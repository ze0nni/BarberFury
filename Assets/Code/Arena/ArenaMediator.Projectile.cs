using System.Collections.Generic;
using Common;
using Arena;
using Model;
using UnityEngine;

namespace Arena {
        public sealed partial class ArenaMediator {
                public Projectile SpawnProjectile(
                        Vector3 position,
                        Quaternion direction,
                        float timeOffset,
                        ProjectileModel model,
                        Identity<Unit> unitId
                ) {
                        var projectile = new Projectile(NewIdentity<Projectile>(), unitId) {
                                SpawnTime = Stage.Time - timeOffset,
                        };
                        projectile.View = Model.InstantiateProjectile(position, direction, model, projectile);
                        
                        Stage.Projectiles.Add(projectile.Id, projectile);
                        if (timeOffset > 0) {
                                UpdateProjectilePosition(timeOffset, projectile);
                        }

                        return projectile;
                }

                private readonly HashSet<Identity<Projectile>> _tmpDieProjectiles = new HashSet<Identity<Projectile>>();
                void UpdateProjectiles(float dt) {
                        _tmpDieProjectiles.Clear();

                        foreach (var p in Stage.Projectiles.Values) {
                                if (Stage.Time - p.SpawnTime > p.View.Model.LifeTime) {
                                        _tmpDieProjectiles.Add(p.Id);

                                        continue;
                                }

                                UpdateProjectilePosition(dt, p);
                                UpdateProjectileTarget(p);
                                if (p.Target.Type != ProjectileTarget.Target.None) {
                                        ApplyProjectileToTarget(p);
                                }
                        }

                        foreach (var id in _tmpDieProjectiles) {
                                if (Stage.Projectiles.TryGetValue(id, out var p)) {
                                        Stage.Projectiles.Remove(id);
                                        GameObject.Destroy(p.View.gameObject);                                        
                                }
                        }
                }

                void UpdateProjectilePosition(float dt, Projectile p) {
                        p.Position0 = p.Position;
                        p.Velocity0 = p.Velocity;

                        p.Velocity += p.View.Model.Gravity * dt;
                        p.Position += 
                                (p.Rotation * Vector3.forward * p.View.Model.Speed) * dt +
                                ((p.Velocity + p.Velocity0) / 2f) * dt;
                }

                void UpdateProjectileTarget(Projectile p) {
                        ref var result = ref p.Target;
                        result = new ProjectileTarget();

                        var distance = (p.Position - p.Position0).magnitude;
                        var ray = new Ray (p.Position0, p.Position - p.Position0);

                        if (Physics.SphereCast(
                                ray,
                                p.View.Model.Radius,
                                out var staticHit,
                                distance,
                                _defaultLayerMask
                        )) {
                                result = new ProjectileTarget {
                                        Type = ProjectileTarget.Target.Static,
                                        Point = staticHit.point,
                                        Distance = staticHit.distance,
                                };
                        }

                        foreach (var u in Stage.Units.Values) {
                                if (u.Id == p.UnitId) {
                                        continue;
                                }

                                if (RayCastUtils.SphereCast(u.View.Collider, ray, p.View.Model.Radius, out var unitHit, distance)) {
                                        if (result.Type == ProjectileTarget.Target.None || unitHit.distance < result.Distance) {
                                                result = new ProjectileTarget {
                                                        Type = ProjectileTarget.Target.Unit,
                                                        Point = unitHit.point,
                                                        Distance = unitHit.distance,
                                                        UnitId = u.Id
                                                };
                                        }
                                }
                        }
                }

                void ApplyProjectileToTarget(Projectile p) {
                        ref var target = ref p.Target;
                        switch (target.Type) {
                                case ProjectileTarget.Target.Static: {
                                        _tmpDieProjectiles.Add(p.Id);
                                        break;
                                }
                                case ProjectileTarget.Target.Unit: {
                                        _tmpDieProjectiles.Add(p.Id);
                                        if (Stage.Units.TryGetValue(target.UnitId, out var unit)) {
                                                var damage = p.View.Model.Damage;
                                                unit.IncomingDamages.Add(new IncomingDamage {
                                                        UnitId = p.UnitId,
                                                        Damage = damage
                                                });
                                        }
                                        break;
                                }
                                default: {
                                        throw new System.ArgumentException(target.Type.ToString());
                                }
                        }                        
                }
        }
}
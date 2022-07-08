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
                        ProjectileModel model
                ) {
                        var projectile = new Projectile(NewIdentity<Projectile>()) {
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
                        }

                        foreach (var id in _tmpDieProjectiles) {
                                if (Stage.Projectiles.TryGetValue(id, out var p)) {
                                        GameObject.Destroy(p.View.gameObject);
                                        Stage.Projectiles.Remove(id);
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
        }
}
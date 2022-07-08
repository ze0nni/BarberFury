using System.Data;
using Arena;
using UnityEngine;

namespace Model {        
        public class WeaponModelBehaviour_Regular: WeaponModelBehaviour {
                public Transform ProjectileRoot;
                public ProjectileModel Projectile;
                public bool Automatic;
                public float FireRate;
                public float SprayCone;
                bool _isLastFired;

                public override void Update(float dt, ArenaMediator mediator, Weapon weapon)
                {
                        var fire = weapon.InputFire;
                        var fireDown = !_isLastFired && fire;
                        var fireUp = _isLastFired && !fire;
                        _isLastFired = fire;

                        int bulletsCount = 0;
                        if (!Automatic && fireDown) {
                                bulletsCount = 1;
                                weapon.LastFireTime = mediator.Stage.Time;
                        } else if (Automatic && fire) {
                                bulletsCount += 1;
                        }

                        if (bulletsCount == 0) {
                                return;
                        }
                        var timeOffsetStep = dt / bulletsCount;
                        var rotation = weapon.Rotation;

                        for (var i = 0; i < bulletsCount; i++) {
                                var sprayX = (float)mediator.Stage.Random.NextDouble() * (SprayCone * 2) - SprayCone;
                                var sprayY = (float)mediator.Stage.Random.NextDouble() * (SprayCone * 2) - SprayCone;

                                mediator.SpawnProjectile(
                                        ProjectileRoot.transform.position,
                                        rotation * Quaternion.EulerAngles(sprayX * Mathf.PI / 180f, sprayY* Mathf.PI / 180f, 0),
                                        timeOffsetStep * i,
                                        Projectile,
                                        weapon.Picker);
                        }
                }
        }
}
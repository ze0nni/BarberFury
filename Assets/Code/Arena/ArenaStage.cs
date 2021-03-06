using Common;
using Arena.View;
using System.Collections.Generic;
using UnityEngine;

namespace Arena {
        public sealed class ArenaStage {                
                public int IdentityCounter;
                public float Time;
                public readonly System.Random Random = new System.Random();
                public readonly Dictionary<Identity<SpawnPoint>, SpawnPoint> SpawnPoints = new Dictionary<Identity<SpawnPoint>, SpawnPoint>();
                public readonly Dictionary<Identity<Unit>, Unit> Units = new Dictionary<Identity<Unit>, Unit>();
                public readonly Dictionary<Identity<Weapon>, Weapon> Weapons = new Dictionary<Identity<Weapon>, Weapon>();
                public readonly Dictionary<Identity<Projectile>, Projectile> Projectiles = new Dictionary<Identity<Projectile>, Projectile>();
        }

        public enum TeamId {
                Team1,
                Team2
        } 

        public sealed class SpawnPoint {
                public readonly Identity<SpawnPoint> Id;
                public readonly Vector3 Position;
                public readonly TeamId TeamId;
                public readonly float StartHealth;

                public SpawnPoint(Identity<SpawnPoint> id, Vector3 position, TeamId teamId, float startHealth) {
                        Id = id;
                        Position = position;
                        TeamId = teamId;
                        StartHealth = startHealth;
                }
        }

        public sealed class Unit {
                public readonly Identity<Unit> Id;

                public UnitInput Input;

                public TeamId Team;

                public Vector3 Position {
                        get => View.transform.position;
                        set => View.transform.position = value;
                }
                public float YawValue;
                public Quaternion Yaw => Quaternion.Euler(0, YawValue, 0);
                public float PitchValue;
                public Quaternion Pitch => Quaternion.Euler(PitchValue, 0, 0);
                public Quaternion Rotation => Yaw * Pitch;

                public Vector3 CameraPosition => Position + Vector3.up * 1.5f;
                public Vector3 LeftHandPosition => CameraPosition + Rotation * new Vector3(-0.35f, -0.25f, 0.5f);
                public Vector3 RightHandPosition => CameraPosition + Rotation * new Vector3(0.35f, -0.25f, 0.5f);

                public UnitInteract Interact;

                public float Health;
                public bool IsAlive => Health > 0;
                public float WetEffect = 0;
                public float BurnEffect = 0;
                public float DamageModfWet = -10f;
                public float DamageModfBurn = 10f;

                public CrosshairTarget CrosshairTarget;
                public Identity<Weapon> LeftHand;
                public Identity<Weapon> RightHand;

                public readonly List<IncomingDamage> IncomingDamages = new List<IncomingDamage>();

                public UnitView View;
                public Unit(Identity<Unit> id) {
                        Id = id;
                }
                
                public bool Raycast(Ray ray, float maxDistance, out RaycastHit hit) {
                        return View.Collider.Raycast(ray, out hit, maxDistance);
                }
        }

        public struct UnitInput {
                public bool MoveForward;
                public bool MoveBack;
                public bool ShiftLeft;
                public bool ShiftRight;                
                
                public float Yaw;
                public float Pitch;

                public bool PickLeft;
                public bool PickRight;
                public bool FireLeft;
                public bool FireRight;

                public bool Reset;
        }

        public struct UnitInteract {
                public enum Target { None, Weapon }                
                public Target Type;
                public float Angle;
                public Identity<Weapon> WeaponId;
        }

        public struct CrosshairTarget {
                public enum Target { None, Static, Unit }
                public Target Type;
                public Vector3 Position;
                public float Distance;
                public Identity<Unit> UnitId;
        }

        public struct IncomingDamage {
                public Identity<Unit> UnitId;
                public float Damage;
                public DamageEffect Effect;
                public float EffectValue;

        }

        public enum DamageEffect {
                Dot,
                Fire,
                Water
        }

        public class Weapon {
                public readonly Identity<Weapon> Id;
                public Vector3 Position {
                        get => View.transform.position;
                        set => View.transform.position = value;
                }

                public Quaternion Rotation {
                        get => View.transform.rotation;
                        set => View.transform.rotation = value;
                }

                public Identity<Unit> Picker;
                public bool InputFire;
                public float LastFireTime;
                public WeaponView View;

                public Weapon(Identity<Weapon> id) {
                        Id = id;
                }
        }

        public class Projectile {
                public readonly Identity<Projectile> Id;
                public readonly Identity<Unit> UnitId;

                public Vector3 Position {
                        get => View.transform.position;
                        set => View.transform.position = value;
                }
                public Vector3 Position0;

                public Quaternion Rotation {
                        get => View.transform.rotation;
                        set => View.transform.rotation = value;
                }

                public Vector3 Velocity;
                public Vector3 Velocity0;
                public float SpawnTime;

                public ProjectileTarget Target;

                public ProjectileView View;

                public Projectile(Identity<Projectile> id, Identity<Unit> unitId) {
                        Id = id;
                        UnitId = unitId;
                }
        }

        public struct ProjectileTarget {
                public enum Target { None, Static, Unit }
                public Target Type;
                public Vector3 Point;
                public float Distance;
                
                public Identity<Unit> UnitId;
        }
}
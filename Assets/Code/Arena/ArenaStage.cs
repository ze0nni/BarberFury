using System.Security.Cryptography.X509Certificates;
using Common;
using Arena.View;
using System.Collections.Generic;
using UnityEngine;

namespace Arena {
        public sealed class ArenaStage {                
                public int IdentityCounter;
                public readonly System.Random Random = new System.Random();
                public readonly Dictionary<Identity<SpawnPoint>, SpawnPoint> SpawnPoints = new Dictionary<Identity<SpawnPoint>, SpawnPoint>();
                public readonly Dictionary<Identity<Unit>, Unit> Units = new Dictionary<Identity<Unit>, Unit>();
                public readonly Dictionary<Identity<Weapon>, Weapon> Weapons = new Dictionary<Identity<Weapon>, Weapon>();
        }

        public enum TeamId {
                Team1,
                Team2
        } 

        public sealed class SpawnPoint {
                public readonly Identity<SpawnPoint> Id;
                public readonly Vector3 Position;
                public readonly TeamId TeamId;

                public SpawnPoint(Identity<SpawnPoint> id, Vector3 position, TeamId teamId) {
                        Id = id;
                        Position = position;
                        TeamId = teamId;
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

                public UnitInteract Interact;

                public float Health;
                public bool IsAlive => Health > 0;

                public Identity<Weapon> LeftHand;
                public Identity<Weapon> RightHand;

                public UnitView View;
                public Unit(Identity<Unit> id) {
                        Id = id;
                }
        }

        public struct UnitInput {
                public bool MoveForward;
                public bool MoveBack;
                public bool ShiftLeft;
                public bool ShiftRight;
                public bool PickLeft;
                public bool PickRight;
                public float Yaw;
                public float Pitch;                
        }

        public struct UnitInteract {
                public enum Target { None, Weapon }                
                public Target Type;
                public float Angle;
                public Identity<Weapon> WeaponId;
        }

        public class Weapon {
                public readonly Identity<Weapon> Id;
                public Vector3 Position {
                        get => View.transform.position;
                        set => View.transform.position = value;
                }

                public Identity<Unit> Picker;

                public WeaponView View;

                public Weapon(Identity<Weapon> id) {
                        Id = id;
                }
        }
}
using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Arena {
        public sealed class ArenaStage {
                public int IdentityCounter;
                public readonly Dictionary<Identity<Unit>, Unit> Units = new Dictionary<Identity<Unit>, Unit>();
        }

        public enum TeamId {
                Team1,
                Team2
        } 

        public sealed class Unit {
                public readonly Identity<Unit> Id;
                public TeamId Team;
                public float Health;
                public float WetFactor;
                public float BurnFactor;

                public Animator Animator;

                public Unit(Identity<Unit> id) {
                        Id = id;
                }
        }

        public struct UnitInput {
                public bool MoveForward;
                public bool MoveBack;
                public bool ShiftLeft;
                public bool ShiftRight;
        }

        public class Weapon {
                public readonly Identity<Weapon> Id;
        }
}
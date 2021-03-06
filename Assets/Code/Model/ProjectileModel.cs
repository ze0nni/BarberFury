using Arena;
using UnityEngine;

namespace Model {
        public class ProjectileModel: MonoBehaviour {
                public float Radius = 1;
                public float LifeTime = 3;
                public float Speed = 10;
                public Vector3 Gravity;

                public float Damage;
                public DamageEffect Effect;
                public float EffectValue;

                void OnDrawGizmos() {
                        Gizmos.DrawWireSphere(transform.position, Radius);
                }
        }
}
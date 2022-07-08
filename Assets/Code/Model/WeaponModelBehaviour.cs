using Arena;
using UnityEngine;

namespace Model {
        public abstract class WeaponModelBehaviour : MonoBehaviour {
                public abstract void Update(float dt, ArenaMediator mediator, Weapon weapon);
        }
}
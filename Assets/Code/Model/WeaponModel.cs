using Arena;
using UnityEngine;

namespace Model {        
        public class WeaponModel : MonoBehaviour {                
                [SerializeField] private WeaponModelBehaviour _behaviour;

                public void Update(float dt, ArenaMediator mediator, Weapon weapon) {
                        _behaviour.Update(dt, mediator, weapon);
                }
        }
}
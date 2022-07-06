using Model;
using UnityEngine;

namespace Arena.Components {
        public class WeaponComponent : MonoBehaviour {
                public WeaponModel Model;
                
         void OnDrawGizmos() {
                        Gizmos.DrawIcon(transform.position, "Weapon.png", true, Color.black);
                }
        }
}
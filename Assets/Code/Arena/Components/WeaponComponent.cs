using UnityEngine;

namespace Arena.Components {
        public class WeaponComponent : MonoBehaviour {
                private void OnDrawGizmos() {
                        Gizmos.DrawIcon(transform.position, "Weapon.png", true, Color.black);
                }
        }
}
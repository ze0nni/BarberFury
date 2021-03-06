using UnityEngine;

namespace Common {
        public class BillboardComponent : MonoBehaviour {
                void Update() {
                        var camera = Camera.main;
                        if (camera == null) {
                                return;
                        }
                        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
                }
        }
}
using UnityEngine;

namespace Common {
        public static class RayCastUtils {
                public static bool SphereCast(Collider collider, Ray ray, float radius, out RaycastHit result, float maxDistance) {
                        //TODO: Тест на касание сферы
                        //Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
                        return collider.Raycast(ray, out result, maxDistance);
                }
        }
}
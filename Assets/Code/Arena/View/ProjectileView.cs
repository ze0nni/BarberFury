using Arena;
using Model;
using UnityEngine;

namespace Arena.View {
        public class ProjectileView: MonoBehaviour {                
                public ProjectileModel Model { get; private set; }
                public Projectile Projectile { get; private set; }
                public void SetModel(ProjectileModel model, Projectile projectile) {
                        Projectile = projectile;
                        Model = GameObject.Instantiate(model, transform);
                }
        }
}
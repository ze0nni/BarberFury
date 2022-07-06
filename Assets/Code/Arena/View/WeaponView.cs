using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Arena.View {                
        public class WeaponView : MonoBehaviour {
                public Rigidbody Rigidbody;
                public GameObject InteractRoot;
                public Text InteractText;
                
                public WeaponModel Model { get; private set; }
                public Weapon Weapon { get; private set; }

                public void SetModel(WeaponModel model, Weapon weapon) {
                        Model = GameObject.Instantiate(model, transform);
                        Weapon = weapon;

                        InteractText.text = model.name;
                }
        }
}
using Arena.View;
using Arena;
using Model;
using UnityEngine;

namespace Model {                
        public class ModelComponent : MonoBehaviour {
                [SerializeField] UnitView _unit;
                [SerializeField] WeaponView _weapon;

                public UnitView InstantiateUnit(Vector3 position) {
                        return GameObject.Instantiate(_unit, position, Quaternion.identity);                        
                }

                public WeaponView InstantiateWeapon(Vector3 position, WeaponModel model, Weapon weapon) {
                        var view = GameObject.Instantiate(_weapon, position, Quaternion.identity);
                        view.SetModel(model, weapon);
                        return view;
                }
        }
}
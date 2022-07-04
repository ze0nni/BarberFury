using Arena.View;
using UnityEngine;

namespace Model {                
        public class ModelComponent : MonoBehaviour {
                [SerializeField] private UnitView _unit;
                [SerializeField] private WeaponView _weapon;

                public UnitView InstantiateUnit(Vector3 position) {
                        return GameObject.Instantiate(_unit, position, Quaternion.identity);                        
                }

                public WeaponView InstantiateWeapon(Vector3 position) {
                        return GameObject.Instantiate(_weapon, position, Quaternion.identity);
                }
        }
}
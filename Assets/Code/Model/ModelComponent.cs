using Arena.View;
using UnityEngine;

namespace Model {                
        public class ModelComponent : MonoBehaviour {
                [SerializeField] private UnitView _unit;

                public UnitView InstantiateUnit(Vector3 position) {
                        return GameObject.Instantiate(_unit, position, Quaternion.identity);
                }
        }
}
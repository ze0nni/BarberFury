using UnityEngine;

namespace Model {                
        public class ModelComponent : MonoBehaviour {
                [SerializeField] private GameObject _unit;

                public GameObject InstantateUnit() {
                        return GameObject.Instantiate(_unit);
                }
        }
}
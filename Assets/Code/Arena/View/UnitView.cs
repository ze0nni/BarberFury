using UnityEngine;
using UnityEngine.UI;

namespace Arena.View {
        public class UnitView : MonoBehaviour {
                public CharacterController Controller;
                public Collider Collider;

                [Header("Info panel")]
                public GameObject InfoRoot;
                
                [Space]
                [SerializeField] Text _healthText;
                
                [Space]
                [SerializeField] Image _effectIcon;
                [SerializeField] Image _effectStatus;
                [SerializeField] Sprite _effectIconWet;
                [SerializeField] Sprite _eEffectIconBurn;

                float _lastHealth = -1;

                public void UpdateInfo(Unit unit) {                        
                        if (_lastHealth != unit.Health) {
                                _lastHealth = unit.Health;
                                _healthText.text = Mathf.Ceil(unit.Health).ToString();
                        }
                }
        }
}
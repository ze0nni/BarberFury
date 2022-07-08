using UnityEngine;
using UnityEngine.UI;

namespace Arena.View {
        public class UnitView : MonoBehaviour {
                public CharacterController Controller;
                public Collider Collider;

                public MeshRenderer MeshRenderer;

                [Header("Info panel")]
                public GameObject InfoRoot;
                
                [Space]
                [SerializeField] Text _healthText;
                
                [Space]
                [SerializeField] Image _effectIcon;
                [SerializeField] Image _effectStatus;
                [SerializeField] Sprite _effectIconWet;
                [SerializeField] Sprite _effectIconBurn;

                float _lastHealth = -1;

                public void UpdateInfo(Unit unit) {                        
                        if (_lastHealth != unit.Health) {
                                _lastHealth = unit.Health;
                                _healthText.text = Mathf.Ceil(unit.Health).ToString();
                        }

                        var effectValue = 0f;
                        Sprite effectIcon = null;
                        var effectColor = Color.white;

                        if (unit.BurnEffect > 0) {
                                effectValue = unit.BurnEffect;
                                effectIcon = _effectIconBurn;
                                effectColor = Color.red;
                        }
                        if (unit.WetEffect > 0) {
                                effectValue = unit.WetEffect;
                                effectIcon = _effectIconWet;
                                effectColor = Color.blue;
                        }

                        if (effectValue == 0) {
                                _effectIcon.gameObject.SetActive(false);
                        } else {
                                _effectIcon.gameObject.SetActive(true);
                                _effectIcon.sprite = effectIcon;
                                _effectIcon.color = effectColor;
                                _effectStatus.transform.localScale = new Vector3(1, effectValue / 100f, 1);
                        }

                        MeshRenderer.material.SetColor("_Color", effectColor);
                }

        }
}
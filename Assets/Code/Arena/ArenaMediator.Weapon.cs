using Common;
using UnityEngine;

namespace Arena {
        public sealed partial class ArenaMediator {
                public Weapon SpawnWeapon(Vector3 position) {
                        var id = NewIdentity<Weapon>();

                        var weapon = new Weapon(id) {
                                
                        };
                        
                        var view = Model.InstantiateWeapon(position);
                        weapon.View = view;

                        Stage.Weapons.Add(id, weapon);

                        return weapon;
                }

                private void UpdateWeapons(float dt) {
                        foreach (var weapon in Stage.Weapons.Values) {
                                weapon.View.gameObject.SetActive(weapon.Picker.IsNull);
                        }
                }

                private void UpdateWeaponsHud(Identity<Weapon> interactId) {
                        foreach (var weapon in Stage.Weapons.Values) {
                                var view = weapon.View;
                                view.InteractRoot.SetActive(interactId == weapon.Id);
                        }
                }
        }       
}
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

                void UpdateWeapons(float dt) {
                        Stage.Units.TryGetValue(_playerId, out var player);

                        foreach (var weapon in Stage.Weapons.Values) {
                                Stage.Units.TryGetValue(weapon.Picker, out var picker);
                                
                                weapon.View.Rigidbody.isKinematic = picker != null;
                                weapon.View.InteractRoot.SetActive(weapon.Picker.IsNull && player?.Interact.WeaponId == weapon.Id);

                                if (picker != null) {
                                        weapon.Position = picker.LeftHand == weapon.Id
                                                ? picker.LeftHandPosition
                                                : picker.RightHandPosition;
                                        weapon.Rotation = picker.Rotation;
                                }
                        }
                }                
        }       
}
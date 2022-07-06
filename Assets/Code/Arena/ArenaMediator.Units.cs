using Common;
using UnityEngine;

namespace Arena {
        public sealed partial class ArenaMediator {
                public Unit SpawnUnit(TeamId team, Vector3 position) {
                        var id = NewIdentity<Unit>();

                        var unit = new Unit(id) {
                                Team = team
                        };

                        var view = Model.InstantiateUnit(position);
                        unit.View = view;

                        Stage.Units.Add(id, unit);

                        return unit;
                }

                void UpdateUnits(float dt) {
                        foreach (var unit in Stage.Units.Values) {
                                UpdateUnitInteract(unit);
                                ApplyUnitMovementInput(dt, unit);
                                UpdateCrosshairTargetPoint(unit);
                                InvalidateUnitWeapon(unit, unit.Input.PickLeft, ref unit.LeftHand);
                                InvalidateUnitWeapon(unit, unit.Input.PickRight, ref unit.RightHand);
                                ApplyUnitFireInput(unit, unit.Input.FireLeft, unit.LeftHand);
                                ApplyUnitFireInput(unit, unit.Input.FireRight, unit.RightHand);
                        }
                }

                void InvalidateUnitWeapon(Unit unit, bool pick, ref Identity<Weapon> id) {
                        if (Stage.Weapons.TryGetValue(id, out var currentWeapon)) {
                                currentWeapon.Picker = unit.Id;
                        } else {
                                id = Identity<Weapon>.Null;
                        }

                        if (!pick) {
                                return;
                        }
                        if (currentWeapon != null) {
                                id = Identity<Weapon>.Null;                                
                                currentWeapon.Picker = Identity<Unit>.Null;
                                currentWeapon.Rotation = unit.Yaw;
                                currentWeapon.Position = unit.CameraPosition + unit.Yaw * Vector3.forward * 0.5f;
                        }

                        if (unit.Interact.WeaponId.IsNull) {
                                return;
                        }

                        currentWeapon = Stage.Weapons[unit.Interact.WeaponId];
                        if (!currentWeapon.Picker.IsNull) {
                                return;
                        }

                        currentWeapon.Picker = unit.Id;
                        id = unit.Interact.WeaponId;
                }

                void ApplyUnitFireInput(Unit unit, bool fire, Identity<Weapon> id) {
                        if (!Stage.Weapons.TryGetValue(id, out var weapon)) {
                                return;
                        }
                        weapon.InputFire = fire;
                }

                void ApplyUnitMovementInput(float dt, Unit unit) {
                        var view = unit.View;
                        var controller = view.Controller;
                        ref var input = ref unit.Input;

                        unit.YawValue += input.Yaw;
                        while (unit.YawValue > 360) unit.YawValue -= 360;
                        while (unit.YawValue < 0) unit.YawValue += 360;
                        unit.PitchValue = Mathf.Clamp(unit.PitchValue + input.Pitch, -90, 90);

                        var moveDirection = Vector3.zero;
                        if (input.MoveForward) {
                                moveDirection += unit.Yaw * Vector3.forward;
                        }
                        if (input.MoveBack) {
                                moveDirection += unit.Yaw * Vector3.back;
                        }
                        if (input.ShiftLeft) {
                                moveDirection += unit.Yaw * Vector3.left;
                        }
                        if (input.ShiftRight) {
                                moveDirection += unit.Yaw * Vector3.right;
                        }

                        view.transform.rotation = unit.Yaw;
                        if (moveDirection.sqrMagnitude > 0) {
                                controller.Move(moveDirection.normalized * 5 * dt);
                        }
                        if (!controller.isGrounded) {
                                controller.Move(Vector3.down * 9.8f * dt);
                        }
                }

                void UpdateUnitInteract(Unit unit) {
                        ref var result = ref unit.Interact;
                        result = new UnitInteract();

                        var position = unit.CameraPosition;
                        var forward = unit.Rotation * Vector3.forward;

                        foreach (var weapon in Stage.Weapons.Values) {
                                if (!weapon.Picker.IsNull) {
                                        continue;
                                }                                
                                if (!GetUnitCanInteractWith(position, forward, weapon.Position, out var angle)) {
                                        continue;
                                }
                                if (result.Type == UnitInteract.Target.None || angle < result.Angle) {
                                        result = new UnitInteract {
                                                Type = UnitInteract.Target.Weapon,
                                                WeaponId = weapon.Id,
                                                Angle = angle
                                        };
                                }
                        }
                }

                bool GetUnitCanInteractWith(Vector3 unit, Vector3 forward, Vector3 target, out float angle) {
                        var horizontalDistance = Vector3.Distance(
                                new Vector3(unit.x, 0, unit.z),
                                new Vector3(target.x, 0, target.z)
                        );
                        if (horizontalDistance > 1.3f) {
                                angle = default;
                                return false;
                        }

                        angle = Vector3.Angle(target - unit, forward);

                        return angle < 30;
                }

                int _defaultLayerMask = LayerMask.GetMask("Default");

                void UpdateCrosshairTargetPoint(Unit unit) {
                        
                        var ray = new Ray(unit.CameraPosition, unit.CameraPosition + unit.Rotation * Vector3.forward * 300f);                        

                        ref var target = ref unit.CrosshairTarget;
                        target = new CrosshairTarget() {
                                Type = CrosshairTarget.Target.None,
                                Position = unit.Rotation * Vector3.forward * 300f,
                                Distance = 300
                        };
                        
                        if (Physics.Raycast(ray, out var staticHit, 300, _defaultLayerMask)) {
                                target = new CrosshairTarget {
                                        Type = CrosshairTarget.Target.Static,
                                        Position = staticHit.point,
                                        Distance = staticHit.distance,
                                };
                        }

                        foreach (var raycastUnit in Stage.Units.Values) {
                                if (raycastUnit == unit) {
                                        continue;
                                }
                                if (raycastUnit.Raycast(ray, 300f, out var unitHit)) {
                                        if (target.Type == CrosshairTarget.Target.None || target.Distance > unitHit.distance) {
                                                target = new CrosshairTarget {
                                                        Type = CrosshairTarget.Target.Unit,
                                                        Position = unitHit.point,
                                                        Distance = unitHit.distance,
                                                        UnitId = raycastUnit.Id
                                                };
                                        }
                                }
                        }
                }
        }       
}
using Common;
using UnityEngine;

namespace Arena {
        public sealed partial class ArenaMediator {
                public Unit SpawnUnit(TeamId team, Vector3 position) {
                        var id = NewIdentity<Unit>();

                        var unit = new Unit(id) {
                                Team = team,
                                Position = position
                        };

                        var view = Model.InstantiateUnit(position);
                        unit.View = view;

                        Stage.Units.Add(id, unit);

                        return unit;
                }

                private void UpdateUnits(float dt) {
                        foreach (var u in Stage.Units.Values) {
                                UpdateUnit(dt, u);
                        }
                }

                private void UpdateUnit(float dt, Unit unit) {                        
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

                        view.transform.position = unit.Position;
                        view.transform.rotation = unit.Yaw;
                        
                        if (moveDirection.sqrMagnitude > 0) {
                                controller.Move(moveDirection.normalized * 5 * dt);
                        }
                        if (!controller.isGrounded) {
                                controller.Move(Vector3.down * 9.8f * dt);
                        }

                        unit.Position = view.transform.position;
                }
        }       
}
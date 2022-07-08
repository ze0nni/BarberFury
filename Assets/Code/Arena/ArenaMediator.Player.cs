using Common;
using UnityEngine;

namespace Arena
{
        public sealed partial class ArenaMediator
        {
                public void SetPlayer(Identity<Unit> id) {
                        _playerId = id;
                }

                void UpdatePlayerInput() {
                        if (!Stage.Units.TryGetValue(_playerId, out var player)) {
                                return;
                        }
                
                        player.Input = new UnitInput {
                                MoveForward = Input.GetKey(KeyCode.W),
                                MoveBack = Input.GetKey(KeyCode.S),
                                ShiftLeft = Input.GetKey(KeyCode.A),
                                ShiftRight = Input.GetKey(KeyCode.D),                                                              
                                Yaw = Input.GetAxis("Mouse X") * 2,
                                Pitch = -Input.GetAxis("Mouse Y") * 2,
                                PickRight = Input.GetKeyDown(KeyCode.E),
                                PickLeft = Input.GetKeyDown(KeyCode.Q),
                                FireLeft = Input.GetMouseButton(0),
                                FireRight = Input.GetMouseButton(1),
                                Reset = Input.GetKeyDown(KeyCode.R)
                        };
                }

                void UpdatePlayerCamera() {
                        if (!Stage.Units.TryGetValue(_playerId, out var player)) {
                                return;
                        }
                        _camera.transform.position = player.CameraPosition;
                        _camera.transform.rotation = player.Rotation;
                }
        }
}
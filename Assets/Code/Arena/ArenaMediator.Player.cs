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
                                Pitch = -Input.GetAxis("Mouse Y") * 2
                        };
                }

                void UpdatePlayerCamera() {
                        if (!Stage.Units.TryGetValue(_playerId, out var player)) {
                                return;
                        }
                        _camera.transform.position = player.Position + Vector3.up * 1.5f;
                        _camera.transform.rotation = player.Rotation;
                }
        }
}
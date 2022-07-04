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
                }
        }
}
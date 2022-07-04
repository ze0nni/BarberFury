using Common;
using Model;
using UnityEngine;

namespace Arena
{
        public sealed partial class ArenaMediator
        {
                public readonly ModelComponent Model;
                public readonly ArenaStage Stage = new ArenaStage();

                private readonly Camera _camera;

                public ArenaMediator(Camera camera, ModelComponent model) {
                        _camera = camera;
                        Model = model;
                }

                private Identity<Unit> _playerId;

                public void Update(float dt) {
                        UpdatePlayerInput();
                        ApplyUnitsInput();
                }

                public void FixedUpdate(float dt) {
                        Physics.Simulate(dt);
                }
                
                public Identity<T> NewIdentity<T>() {
                        return new Identity<T>(++Stage.IdentityCounter);
                }
        }
}
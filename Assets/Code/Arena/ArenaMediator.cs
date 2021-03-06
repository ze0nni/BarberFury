using Common;
using Model;
using UnityEngine;

namespace Arena
{
        public sealed partial class ArenaMediator
        {
                public readonly ModelComponent Model;
                public readonly ArenaStage Stage = new ArenaStage();

                readonly Camera _camera;
                readonly IArenaScript _script;                
                
                Identity<Unit> _playerId;

                public bool TryGetPlayer(out Unit player) => Stage.Units.TryGetValue(_playerId, out player);

                public ArenaMediator(Camera camera, ModelComponent model, IArenaScript script) {
                        _camera = camera;
                        _script = script;

                        Model = model;
                        Init();
                        _script.Init(this);
                }

                public Identity<T> NewIdentity<T>() {
                        return new Identity<T>(++Stage.IdentityCounter);
                }

                public void Update(float dt) {
                        Stage.Time += dt;
                        
                        UpdatePlayerInput();   
                        UpdateUnits(dt);
                        UpdateWeapons(dt);
                        UpdateProjectiles(dt);

                        _script.Update(dt);
                        
                        UpdatePlayerCamera();
                }

                public void FixedUpdate(float dt) {
                        Physics.Simulate(dt);
                }
        }
}
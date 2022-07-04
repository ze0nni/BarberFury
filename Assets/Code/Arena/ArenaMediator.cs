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
                private readonly IArenaScript _script;                
                
                private Identity<Unit> _playerId;

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
                        UpdatePlayerInput();   
                        UpdateUnits(dt);                     

                        _script.Update(dt);

                        UpdatePlayerCamera();
                }
        }
}
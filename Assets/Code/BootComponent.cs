using Arena;
using Model;
using UnityEngine;

public class BootComponent : MonoBehaviour {

        [SerializeField] private Camera _camera;
        [SerializeField] private ModelComponent _model;        

        private ArenaMediator _arena;

        private void OnEnable() {
                Physics.autoSimulation = false;
                
                _arena = new ArenaMediator(_camera, _model);
                var playerId = _arena.SpawnUnit(TeamId.Team1);
                _arena.SetPlayer(playerId);
        }

        private void Update() {
                _arena.Update(Time.deltaTime);
        }

        private void FixedUpdate() {
                _arena.FixedUpdate(Time.fixedDeltaTime);
        }
}
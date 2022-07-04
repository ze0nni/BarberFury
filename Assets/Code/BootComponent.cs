using Arena;
using Model;
using UnityEngine;

public class BootComponent : MonoBehaviour {

        [SerializeField] private Camera _camera;
        [SerializeField] private ModelComponent _model;        

        private ArenaMediator _arena;

        private void OnEnable() {
                Cursor.lockState = CursorLockMode.Locked;
                Physics.autoSimulation = false;
                
                _arena = new ArenaMediator(_camera, _model, new ArenaScripts.TestPolygone());                
        }

        private void Update() {
                _arena.Update(Time.deltaTime);
        }
}
using Arena;
using Model;
using UnityEngine;

public class BootComponent : MonoBehaviour {

        [SerializeField] Camera _camera;
        [SerializeField] ModelComponent _model;        

        ArenaMediator _arena;

        void OnEnable() {
                Cursor.lockState = CursorLockMode.Locked;
                Physics.autoSimulation = false;
                
                _arena = new ArenaMediator(_camera, _model, new ArenaScripts.TestPolygone());                
        }

        void Update() {
                _arena.Update(Time.deltaTime);                
#if !UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.Escape)) {
                        Application.Quit();
                }
#endif
        }

        void FixedUpdate() {
                _arena.FixedUpdate(Time.fixedDeltaTime);
        }
}
using Model;
using UnityEngine;

namespace Arena.Components {        
        public class SpawnPointComponent : MonoBehaviour {

                public TeamId TeamId;
                public bool IsPlayer;

                private void OnDrawGizmos() {
                        Color color;
                        switch (TeamId) {
                                case TeamId.Team1:
                                        color = Color.blue;
                                        break;
                                
                                case TeamId.Team2:
                                        color = Color.red;
                                        break;

                                default:
                                        color = Color.white;
                                        break;
                        }
                        Gizmos.DrawIcon(transform.position, "SpawnPoint.png", true, color);
                }
        }
}
using Arena.Components;
using UnityEngine;

namespace Arena
{
        public sealed partial class ArenaMediator {
                private void Init() {
                        foreach (var spawnPoint in GameObject.FindObjectsOfType<SpawnPointComponent>()) {
                                var id = NewIdentity<SpawnPoint>();
                                Stage.SpawnPoints.Add(id, new SpawnPoint(id, spawnPoint.transform.position, spawnPoint.TeamId));
                        }

                        foreach (var weapon in GameObject.FindObjectsOfType<WeaponComponent>()) {

                        }
                }
        }
}
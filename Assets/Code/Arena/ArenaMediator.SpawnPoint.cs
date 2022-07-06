using System;
using System.Collections.Generic;

namespace Arena {
        public sealed partial class ArenaMediator {
                readonly List<SpawnPoint> _tempSpawnPoints = new List<SpawnPoint>();
                public SpawnPoint SelectSpawnPoint(Func<SpawnPoint, bool> predicate) {
                        _tempSpawnPoints.Clear();
                        foreach (var p in Stage.SpawnPoints.Values) {
                                if (predicate(p)) {
                                        _tempSpawnPoints.Add(p);
                                }
                        }
                        if (_tempSpawnPoints.Count == 0) {
                                return null;
                        }
                        return _tempSpawnPoints[Stage.Random.Next(_tempSpawnPoints.Count)];
                }
        }       
}
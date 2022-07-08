using System.Collections.Generic;
using Arena;
using Common;

namespace ArenaScripts {
        public sealed class TestPolygone : IArenaScript
        {
                ArenaMediator _mediator;
                Dictionary<Identity<SpawnPoint>, Identity<Unit>> _team2 = new Dictionary<Identity<SpawnPoint>, Identity<Unit>>();

                public void Init(ArenaMediator mediator)
                {
                        _mediator = mediator;

                        var playerSpawn = mediator.SelectSpawnPoint(p => p.TeamId == TeamId.Team1);
                        var player = mediator.SpawnUnit(TeamId.Team1, playerSpawn.Position, playerSpawn.StartHealth);
                        mediator.SetPlayer(player.Id);

                        ResetTeam2();
                }

                public void Update(float dt)
                {
                       if (_mediator.TryGetPlayer(out var player) && player.Input.Reset) {
                                ResetTeam2();
                       }
                }

                void ResetTeam2() {
                        foreach (var p in _mediator.Stage.SpawnPoints.Values) {
                                if (p.TeamId != TeamId.Team2) {
                                        continue;
                                }
                                _team2.TryGetValue(p.Id, out var unitId);

                                if (_mediator.Stage.Units.TryGetValue(unitId, out var unit) && unit.IsAlive) {
                                        unit.Health = p.StartHealth;
                                } else {
                                        var newUnit = _mediator.SpawnUnit(TeamId.Team2, p.Position, p.StartHealth);
                                        
                                        _team2[p.Id] = newUnit.Id;
                                }
                        }
                }
        }
}
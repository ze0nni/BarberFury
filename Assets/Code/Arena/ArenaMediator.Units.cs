using Common;

namespace Arena {
        public sealed partial class ArenaMediator {
                public Identity<Unit> SpawnUnit(TeamId team) {
                        var id = NewIdentity<Unit>();

                        var unit = new Unit(id) {
                                Team = team
                        };

                        Stage.Units.Add(id, unit);

                        return id;
                }

                public void ApplyUnitsInput() {

                }
        }       
}
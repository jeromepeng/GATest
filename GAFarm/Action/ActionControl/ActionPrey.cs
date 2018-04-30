using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAFarm.Common.Interface;
using GAFarm.Common.Define;
using GAFarm.Common.Log;

namespace GAFarm.Action.ActionControl
{
    public class ActionPrey : ICreatureAction
    {

        #region Interface Implement
        public MoveResult Move(ICreature creature, GeoInfo startPt, double length, double direction, IMap fieldMap)
        {
            return null;
        }

        public double TurnAndMoveTo(GeoInfo geoInfo, double speed)
        {
            return 0;
        }

        public void Die(ICreature self)
        {
            //UILog.LogToTextBox(self.IsDead.ToString());
            self.IsDead = true;
        }

        public ScanResult[] Scan(GeoInfo centerInfo, double radius, IMap fieldMap)
        {
            return null;
        }

        public void Eat(ICreature creatureToBeEaten)
        {

        }
        #endregion
    }
}

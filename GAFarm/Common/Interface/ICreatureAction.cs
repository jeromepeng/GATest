using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Define;
namespace GAFarm.Common.Interface
{
    public interface ICreatureAction
    {
        MoveResult Move(ICreature creature, GeoInfo startPt, double length, double direction, IMap fieldMap);

        double TurnAndMoveTo(GeoInfo geoInfo, double speed);

        void Die(ICreature self);

        ScanResult[] Scan(GeoInfo centerInfo, double radius, IMap filedMap);

        void Eat(ICreature creatureToBeEaten);

    }
}

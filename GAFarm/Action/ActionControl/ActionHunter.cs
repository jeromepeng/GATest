using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Interface;
using GAFarm.Common.Define;
namespace GAFarm.Action.ActionControl
{
    public class ActionHunter : ICreatureAction
    {

        #region Interface Implement
        public MoveResult Move(ICreature creature, GeoInfo startPt, double length, double direction, IMap fieldMap)
        {
            GeoInfo moveResultGeoInfo = new GeoInfo(new double[startPt.DimensionInfo.Length]);
            moveResultGeoInfo.DimensionInfo[0] = startPt.DimensionInfo[0] + length * Math.Sin(direction);
            moveResultGeoInfo.DimensionInfo[1] = startPt.DimensionInfo[1] + length * Math.Cos(direction);
            MoveResult result = null;
            if (moveResultGeoInfo.DimensionInfo[0] > fieldMap.MinX
                && moveResultGeoInfo.DimensionInfo[0] < fieldMap.Width + fieldMap.MinX
                && moveResultGeoInfo.DimensionInfo[1] > fieldMap.MinY
                && moveResultGeoInfo.DimensionInfo[1] < fieldMap.Height + fieldMap.MinY)
            {
                result = new MoveResult(moveResultGeoInfo);
                creature.CurrentX = result.X;
                creature.CurrentY = result.Y;
            }
            return result;
        }

        public double TurnAndMoveTo(GeoInfo geoInfo, double speed)
        {
            return 0;
        }

        public void Die(ICreature self)
        {
            self.IsDead = true;
        }

        public ScanResult[] Scan(GeoInfo centerInfo, double radius, IMap fieldMap)
        {
            ICreature[] scanResult = fieldMap.FindCreatureByGeoInfo(centerInfo, (int)radius);
            ScanResult[] result = new ScanResult[scanResult.Length];
            for (int i = 0; i < scanResult.Length; i++)
            {
                result[i] = new ScanResult(true, scanResult[i]);
            }
            return result;
        }

        public void Eat(ICreature creatureToBeEaten)
        {
            creatureToBeEaten.BeEaten();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Define;
using GA.Common;
using GAFarm.Common.Interface;

namespace GAFarm.Common.GeoTools
{
    public class Tools
    {
        public static double GetLength(GeoInfo pt1, GeoInfo pt2)
        {
            double resultBeofrePow = 0;
            for (int i = 0; i < pt1.DimensionInfo.Length; i++)
            {
                resultBeofrePow += Math.Pow((pt1.DimensionInfo[i] - pt2.DimensionInfo[i]), 2);
            }
            return Math.Sqrt(resultBeofrePow);
        }

        public static GeoInfo FieldToClient(GeoInfo input, double fieldHeight)
        {
            GeoInfo result = new GeoInfo(new double[input.DimensionInfo.Length]);
            result.DimensionInfo[0] = input.DimensionInfo[0];
            result.DimensionInfo[1] = fieldHeight - input.DimensionInfo[1];
            return result;
        }

        public static void FieldToClient(int x, int y, ref int outX, ref int outY, double fieldHeight)
        {
            outX = x;
            outY = (int)fieldHeight - y;
        }

        public static double GetDirection(GeoInfo pt1, GeoInfo pt2)
        {
            double result = 0;
            if (pt2.DimensionInfo[0] > pt1.DimensionInfo[0])
            {
                result = Math.Acos((pt2.DimensionInfo[1] - pt1.DimensionInfo[1]) / GetLength(pt1, pt2));
            }
            else
            {
                result = 2 * Math.PI - Math.Acos((pt2.DimensionInfo[1] - pt1.DimensionInfo[1]) / GetLength(pt1, pt2));
            }
            return result;
        }

        public static bool CoordinateInBox(int x, int y, int minX, int maxX, int minY, int maxY)
        {
            bool result = false;
            if (x > minX && x < maxX && y > minY && y < maxY)
            {
                result = true;
            }
            return result;
        }

        public static bool CoordinateInBox(double x, double y, int minX, int maxX, int minY, int maxY)
        {
            bool result = false;
            if (x > minX && x < maxX && y > minY && y < maxY)
            {
                result = true;
            }
            return result;
        }

        public static Creature ConvertToGACreature(ICreature creature)
        {
            Creature result = new Creature();
            result.Result = creature.LifeQuality;
            result.Value = creature.GACreatureValues;
            return result;
        }
    }
}

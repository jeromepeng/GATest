using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Interface;
namespace GAFarm.Manager
{
    public class MapManager
    {
        private static List<IMap> allMaps = new List<IMap>();

        public static void AddMap(IMap map)
        {
            allMaps.Add(map);
        }

        public static IMap GetMapFromIndex(int index)
        {
            if (allMaps.Count > 0 && allMaps.Count > index)
            {
                return allMaps[index];
            }
            else
            {
                return null;
            }
        }
    }
}

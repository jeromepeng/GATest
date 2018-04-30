using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Interface;
using GAFarm.Common.Log;

namespace GAFarm.Common.Define
{
    public class FieldMap : IMap, IDisposable
    {
        #region Private Member
        private ICreature[] mapData;

        private int minX;

        private int minY;

        private int width;

        private int height;

        List<ICreature> allCreatures = new List<ICreature>();
        #endregion

        #region
        #endregion

        #region Interface Implement
        public void InitialMap(int x, int y, int width, int height)
        {
            mapData = new ICreature[width * height];
            ClearMap();
            this.width = width;
            this.height = height;
            this.minX = x;
            this.minY = y;
        }

        public void ClearMap()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mapData[i * width + j] = null;
                }
            }
        }

        public void ClearCreatures()
        {
            ClearMap();
            allCreatures.Clear();
        }

        public void AddCreature(ICreature creature)
        {
            DrawCreature(creature);
            allCreatures.Add(creature);
        }

        public void AddCreatures(ICreature[] creatures)
        {
            for (int i = 0; i < creatures.Length; i++)
            {
                AddCreature(creatures[i]);
            }
        }

        public void ReAddCreatures(ICreature[] creatures)
        {
            ClearCreatures();
            AddCreatures(creatures);
        }

        public void DeleteCreature(ICreature creature)
        {
            EraseCreature(creature);
            allCreatures.Remove(creature);
        }

        public void DeleteAllCreature()
        {
            for (int i = 0; i < allCreatures.Count; i++)
            {
                DeleteCreature(allCreatures[i]);
            }
        }

        public ICreature[] FindCreatureByGeoInfo(GeoInfo pt, int size)
        {
            int x = (int)pt.DimensionInfo[0];
            int y = (int)pt.DimensionInfo[1];
            List<ICreature> results = new List<ICreature>();
            int minX = x - size;
            int maxX = x + size;
            int minY = y - size;
            int maxY = y + size;
            for (int i = 0; i < allCreatures.Count; i++)
            {
                if (GeoTools.Tools.CoordinateInBox(allCreatures[i].CurrentX, allCreatures[i].CurrentY, minX, maxX, minY, maxY))
                {
                    results.Add(allCreatures[i]);
                }
            }
            //for (int i = x - size; i < x + size; i++)
            //{
            //    for (int j = y - size; j < y + size; j++)
            //    {
            //        int index = j * width + i;
            //        if (index > 0 && index < mapData.Length && mapData[index] != null)
            //        {
            //            results.Add(mapData[index]);
            //            break;
            //        }
            //    }
            //}
            return results.ToArray();
        }

        public ICreature FindCreatureByName(int id)
        {
            return allCreatures.Where(x => x.ID == id).SingleOrDefault();
        }

        public void RefreshCreatures()
        {
            ClearMap();
            for (int i = 0; i < allCreatures.Count; i++)
            {
                try
                {
                    allCreatures[i].Move();
                    allCreatures[i].ScanStart();
                    DrawCreature(allCreatures[i]);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        public void RefreshCreature(int id)
        {

        }

        public void DrawCreature(ICreature creature)
        {
            mapData[(int)(creature.CurrentY - minY) * this.width + (int)creature.CurrentX - minX] = creature;
        }

        public void EraseCreature(ICreature creature)
        {
            mapData[(int)(creature.CurrentY - minY) * this.width + (int)creature.CurrentX - minX] = null;
        }

        public void Dispose()
        {

        }

        ~FieldMap()
        {
            mapData = null;
            allCreatures.Clear();
        }
        #endregion

        #region Public Member
        public ICreature[] MapData
        {
            get
            {
                return mapData;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public int MinX
        {
            get
            {
                return minX;
            }
        }

        public int MinY
        {
            get
            {
                return minY;
            }
        }

        public ICreature[] AllCreatures
        {
            get
            {
                return allCreatures.ToArray();
            }
        }
        #endregion
    }
}

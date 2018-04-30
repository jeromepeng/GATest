using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Interface;
namespace GAFarm.Common.Define
{
    public class FieldMap : IMap, IDisposable
    {
        #region Private Member
        private ICreature[] mapData;

        private int width;

        private int height;

        List<ICreature> allCreatures = new List<ICreature>();
        #endregion

        #region
        #endregion

        #region Interface Implement
        public void InitialMap(int width, int height)
        {
            mapData = new ICreature[width * height];
            ClearMap();
            this.width = width;
            this.height = height;
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
            List<object> items = new List<object>();
            //items.AddRange(
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
            for (int i = x - size; i < x + size; i++)
            {
                for (int j = y - size; j < y + size; j++)
                {
                    if (mapData[j * width + i] != null)
                    {
                        results.Add(FindCreatureByName(mapData[j * width + i].ID));
                        break;
                    }
                }
            }
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
                allCreatures[i].Move();
                DrawCreature(allCreatures[i]);
            }
        }

        public void RefreshCreature(int id)
        {

        }

        public void DrawCreature(ICreature creature)
        {
            mapData[(int)creature.CurrentY * this.width + (int)creature.CurrentX] = creature;
        }

        public void EraseCreature(ICreature creature)
        {
            mapData[(int)creature.CurrentY * this.width + (int)creature.CurrentX] = null;
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
        #endregion
    }
}

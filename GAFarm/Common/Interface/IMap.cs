using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Define;
namespace GAFarm.Common.Interface
{
    public interface IMap
    {
        ICreature[] MapData { get; }

        int Width { get; }

        int Height { get; }

        void InitialMap(int width, int height);

        void AddCreature(ICreature creature);

        void AddCreatures(ICreature[] creatures);

        void DeleteCreature(ICreature creature);

        void DeleteAllCreature();

        ICreature[] FindCreatureByGeoInfo(GeoInfo pt, int size);

        ICreature FindCreatureByName(int id);

        void RefreshCreatures();

        void RefreshCreature(int id);

        void ClearMap();

        void DrawCreature(ICreature creature);

        void EraseCreature(ICreature creature);
    }
}

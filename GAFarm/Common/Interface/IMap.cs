using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Define;
namespace GAFarm.Common.Interface
{
    public interface IMap
    {
        //ICreature[] MapData { get; }

        int Width { get; }

        int Height { get; }

        int MinX { get; }

        int MinY { get; }

        ICreature[] AliveCreatures { get; }

        ICreature[] AllCreatures { get; }

        void InitialMap(int x, int y, int width, int height);

        void AddCreature(ICreature creature);

        void AddCreatures(ICreature[] creatures);

        void DeleteCreature(ICreature creature);

        void DeleteAllCreature();

        ICreature[] FindCreatureByGeoInfo(GeoInfo pt, int size);

        ICreature FindCreatureByName(int id);

        void RefreshCreatures();

        void RefreshCreature(int id);

        void ClearMap();

        void ClearCreatures();

        //void DrawCreature(ICreature creature);

        //void EraseCreature(ICreature creature);

        void ReAddCreatures(ICreature[] creatures);

        ICreature[] GetCreaturesByType(int type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.Common;
using GAFarm.Common.Define;

namespace GAFarm.Common.Interface
{
    public interface ICreature
    {
        ICreatureAction Action { get; }

        int ID { get; }

        bool IsDead { get; set; }

        double CurrentX { get; set; }

        double CurrentY { get; set; }

        double RestMoveLength { get; set; }

        double Speed { get; }

        void Create(GA.Common.Creature feature, ICreatureAction creatureAction);

        void Move();

        void Stop();

        void Eat(ICreature beEaten);

        void Die();

        void ScanStart();

        void ScanStop();

        void BeEaten();
    }
}

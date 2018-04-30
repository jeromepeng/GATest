using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ActionControl;
using GAFarm.Common.Define;
using GAFarm.Common.Interface;
namespace GAFarm.Common.CreatureObject
{
    public class Hunter : ICreature
    {
        #region Private Member
        private ICreatureAction hunterAction;

        private double scanSize = 20;

        private double bornX;

        private double bornY;

        private double currentX;

        private double currentY;

        private double moveDirStep;

        private double currentDirection;

        private double moveLength;

        private double restMoveLength;

        private double speed = 20;

        private double creatureSize = 20;

        private ActionTimer actionTimerForMove;

        private ActionTimer actionTimerForScan;

        private ScanResult scanResult;

        private bool isDead = false;

        private int id = 0;

        #endregion

        #region Interface Implement
        public ICreatureAction Action
        {
            get
            {
                return hunterAction;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public void Create(GA.Common.Creature feature, ICreatureAction creatureAction)
        {
            currentX = bornX = feature.Value[0];
            currentY = bornY = feature.Value[1];
            moveDirStep = feature.Value[2];
            moveLength = feature.Value[3];
            currentDirection = moveDirStep;
            id = feature.Name.GetHashCode();
            this.hunterAction = creatureAction;
        }

        public void Move()
        {
            MoveAction();
        }

        public void Stop()
        {
        }

        public void Eat(ICreature beEaten)
        {
            beEaten.IsDead = true;
        }

        public void Die()
        {
            isDead = true;
        }

        public void ScanStart()
        {
            ScanAction();
        }

        public void ScanStop()
        {
        }

        public void ScanAction()
        {
            ScanResult[] scanResults = hunterAction.Scan(new GeoInfo(new double[] { currentX, currentY }), scanSize, Manager.MapManager.GetMapFromIndex(0));
            scanResult = scanResults[0];
        }

        public void BeEaten()
        {
            hunterAction.Die();
        }
        #endregion

        #region Public Property
        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
            }
        }


        public double RestMoveLength
        {
            get
            {
                return restMoveLength;
            }
            set
            {
                restMoveLength = value;
            }
        }

        public double Speed
        {
            get
            {
                return speed;
            }
        }

        public double CurrentX
        {
            get
            {
                return currentX;
            }
            set
            {
                currentX = value;
            }
        }

        public double CurrentY
        {
            get
            {
                return currentY;
            }
            set
            {
                currentY = value;
            }
        }
        #endregion

        #region Private Method
        private void MoveAction()
        {
            if (scanResult != null)
            {
                if (!scanResult.TargetCreature.IsDead && GeoTools.Tools.GetLength(new GeoInfo(new double[] { CurrentX, CurrentY }), scanResult.ScanGeoInfo) > creatureSize)
                {
                    restMoveLength = moveLength;
                    hunterAction.Move(this, new GeoInfo(new double[] { currentX, currentY }), speed / 24, currentDirection, Manager.MapManager.GetMapFromIndex(0));
                }
                else
                {
                    Eat(scanResult.TargetCreature);
                }
            }
            else
            {
                if (restMoveLength > 0)
                {
                    hunterAction.Move(this, new GeoInfo(new double[] { currentX, currentY }), speed / 24, currentDirection, Manager.MapManager.GetMapFromIndex(0));
                    restMoveLength -= speed / 24;
                }
                else
                {
                    restMoveLength = moveLength;
                    currentDirection += moveDirStep;
                }

            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ActionControl;
using GAFarm.Common.Define;
using GAFarm.Common.Interface;
using GAFarm.Common.Log;

namespace GAFarm.Common.CreatureObject
{
    public class Hunter : ICreature
    {
        #region Private Member
        private ICreatureAction hunterAction;

        private double scanSize = 10;

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

        private int type = 0;

        private MoveResult lastMoveResult;

        private int lifeQuality = 0;

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

        public int Type
        {
            get
            {
                return this.type;
            }
        }

        public void Create(GA.Common.Creature feature, ICreatureAction creatureAction, int type)
        {
            currentX = bornX = feature.Value[0];
            currentY = bornY = feature.Value[1];
            moveDirStep = feature.Value[2];
            moveLength = feature.Value[3];
            currentDirection = moveDirStep;
            id = feature.Name.GetHashCode();
            this.hunterAction = creatureAction;
            actionTimerForMove = new ActionTimer(42, new ActionControl.TimerAction(Move));
            this.type = type;
            //actionTimerForMove.StartTimer();
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
            actionTimerForMove.StopTimer();
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
            try
            {
                ScanResult[] scanResults = hunterAction.Scan(new GeoInfo(new double[] { currentX, currentY }), scanSize, Manager.MapManager.GetMapFromIndex(0));
                if (scanResults != null && scanResults.Length > 0)
                {
                    for (int i = 0; i < scanResults.Length; i++)
                    {
                        if (scanResults[i].TargetCreature.Type == 1 && !scanResults[i].TargetCreature.IsDead)
                        {
                            hunterAction.Eat(scanResults[i].TargetCreature);
                            lifeQuality++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void BeEaten()
        {
            hunterAction.Die(this);
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

        public int LifeQuality
        {
            get
            {
                return lifeQuality;
            }
        }

        public double[] GACreatureValues
        {
            get
            {
                return new double[] {currentX, currentY, moveDirStep, moveLength};   
            }
        }
        #endregion

        #region Private Method
        private void MoveAction()
        {
            if (lastMoveResult != null)
            {
                if (restMoveLength > 0)
                {
                    lastMoveResult = hunterAction.Move(this, new GeoInfo(new double[] { currentX, currentY }), speed / 24, currentDirection, Manager.MapManager.GetMapFromIndex(0));
                    restMoveLength -= speed / 24;
                }
                else
                {
                    restMoveLength = moveLength;
                    currentDirection += moveDirStep;
                    lastMoveResult = hunterAction.Move(this, new GeoInfo(new double[] { currentX, currentY }), speed / 24, currentDirection, Manager.MapManager.GetMapFromIndex(0));
                }
            }
            else
            {
                currentDirection += moveDirStep;
                lastMoveResult = hunterAction.Move(this, new GeoInfo(new double[] { currentX, currentY }), speed / 24, currentDirection, Manager.MapManager.GetMapFromIndex(0));
            }
            #endregion
        }
    }
}

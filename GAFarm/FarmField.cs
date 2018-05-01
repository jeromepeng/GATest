using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GA.Core;
using GA.Common;
using GAFarm.Common.CreatureObject;
using GAFarm.Common.Define;
using GAFarm.Action.ActionControl;
using GAFarm.Common.GeoTools;
using GAFarm.Manager;
using GAFarm.Common.Log;
using GAFarm.Common.Interface;

namespace GAFarm
{
    public partial class FarmField : Form
    {
        static int creatureNum = 10;
        static int preyNum = 40;
        Hunter[] hunters = new Hunter[creatureNum];
        Hunter[] preies = new Hunter[preyNum];
        ActionControl.ActionTimer mapTimer;
        Pen pen = new Pen(Color.White);
        Pen penPrey = new Pen(Color.Blue);
        Graphics graphic;
        private int width;
        private int height;
        private int minX = 100;
        private int minY = -100;
        private double[] mins;
        private double[] maxs;
        private double[] preyMins;
        private double[] preyMaxs;
        private Creature[] radomFoodCreature;
        private long lastTimeTick;

        public FarmField()
        {
            InitializeComponent();
            int region = 90;
            FieldMap thisMap = new FieldMap();
            width = 200;
            height = 200;
            thisMap.InitialMap(minX, minY, width, height);
            MapManager.AddMap(thisMap);
            mapTimer = new ActionControl.ActionTimer(21, new ActionControl.TimerAction(RefreshFarm));
            graphic = this.CreateGraphics();
            UILog.CreateInstance(rtbLog);
            mins = new double[4] { minX, minY, 0, 100 };
            maxs = new double[4] { this.width + minX, this.height + minY, 2 * Math.PI, 200 };
            preyMins = new double[4] { minX + region, minY + region, 0, 100 };
            preyMaxs = new double[4] { this.width + minX - region, this.height + minY - region, 2 * Math.PI, 200 };
            radomFoodCreature = GACore.InitCreaturesPerValueOneLimit(preyNum, 4, preyMins, preyMaxs);
        }

        private void FarmFiled_Load(object sender, EventArgs e)
        {
            //激活窗体的双缓冲技术,可以注释掉看看是什么效果
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void RefreshFarm()
        {
            MapManager.GetMapFromIndex(0).RefreshCreatures();
            DrawMap();
            int aliveCreatureNum = MapManager.GetMapFromIndex(0).AliveCreatures.Length;
            if (aliveCreatureNum <= creatureNum + preyNum * 0.1)
            {
                try
                {
                    UILog.LogToTextBox("Cost:" + (DateTime.Now.Ticks - lastTimeTick) / 10000 + " ms");
                    Creature[] allGACreatures = new Creature[creatureNum];
                    ICreature[] allHunters = MapManager.GetMapFromIndex(0).GetCreaturesByType(0);
                    for (int i = 0; i < creatureNum; i++)
                    {
                        allGACreatures[i] = Tools.ConvertToGACreature(allHunters[i]);
                    }
                    LiveRule liveRule = new LiveRule();
                    liveRule.OldRate = new double[] { 0.1, 0.2, 0.4, 0.2, 0.1 };
                    liveRule.NewRate = new double[] { 0.4, 0.2, 0.2, 0.1, 0.1 };
                    allGACreatures = allGACreatures.OrderByDescending(i => i.Result).ToArray();
                    Creature[] newGeneration = GA.Core.GACore.GetNextGenerator2(allGACreatures, allGACreatures.Length, liveRule);
                    //Creature[] newGeneration = GACore.MutantPerValueOneLimit(GACore.GetBestCreatures(allGACreatures, 1), 0.3, mins, maxs);
                    List<ICreature> newCreatures = new List<ICreature>();
                    MapManager.GetMapFromIndex(0).ClearCreatures();
                    for (int i = 0; i < newGeneration.Length; i++)
                    {
                        hunters[i] = new Hunter();
                        hunters[i].Create(newGeneration[i], new ActionHunter(), 0);
                        MapManager.GetMapFromIndex(0).AddCreature(hunters[i]);
                    }
                    for (int i = 0; i < preyNum; i++)
                    {
                        preies[i] = new Hunter();
                        preies[i].Create(radomFoodCreature[i], new ActionPrey(), 1);
                        MapManager.GetMapFromIndex(0).AddCreature(preies[i]);
                    }
                    lastTimeTick = DateTime.Now.Ticks;
                }
                catch (Exception ex)
                {
                    UILog.LogToTextBox(ex.ToString());
                }
            }
        }

        private void DrawMap()
        {
            this.Refresh();
        }

        private void CreateToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            Creature[] radomHunterCreature = GACore.InitCreaturesPerValueOneLimit(creatureNum, 4, mins, maxs);
            
            for (int i = 0; i < creatureNum; i++)
            {
                hunters[i] = new Hunter();
                hunters[i].Create(radomHunterCreature[i], new ActionHunter(), 0);
                MapManager.GetMapFromIndex(0).AddCreature(hunters[i]);
            }
            for (int i = 0; i < preyNum; i++)
            {
                preies[i] = new Hunter();
                preies[i].Create(radomFoodCreature[i], new ActionPrey(), 1);
                MapManager.GetMapFromIndex(0).AddCreature(preies[i]);
            }
            DrawMap();
            lastTimeTick = DateTime.Now.Ticks;
            mapTimer.StartTimer();
        }

        private void FieldFormOnClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < hunters.Length; i++)
            {
                if (hunters[i] != null)
                {
                    hunters[i].Die();
                }
            }
            mapTimer.StopTimer();
        }

        private void FarmFieldOnPaint(object sender, PaintEventArgs e)
        {
            graphic = e.Graphics;
            int newX = 0;
            int newY = 0;
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            graphic.DrawImage(bmp, 0, 0);
            Graphics graphicBmp = Graphics.FromImage(bmp);
            foreach (ICreature creature in MapManager.GetMapFromIndex(0).AllCreatures)
            {
                if (!creature.IsDead)
                {
                    switch (creature.Type)
                    {
                        case 0:
                            {
                                Tools.FieldToClient((int)creature.CurrentX, (int)creature.CurrentY, ref newX, ref newY, MapManager.GetMapFromIndex(0).Height);
                                graphicBmp.DrawRectangle(pen, newX - 5, newY - 5, 10, 10);
                                graphic.DrawImage(bmp, 0, 0);
                                break;
                            }
                        case 1:
                            {
                                Tools.FieldToClient((int)creature.CurrentX, (int)creature.CurrentY, ref newX, ref newY, MapManager.GetMapFromIndex(0).Height);
                                graphicBmp.DrawRectangle(penPrey, newX - 3, newY - 3, 6, 6);
                                graphic.DrawImage(bmp, 0, 0);
                                break;
                            }
                    }
                }
            }
            //for (int i = 0; i < MapManager.GetMapFromIndex(0).Height; i++)
            //{
            //    for (int j = 0; j < MapManager.GetMapFromIndex(0).Width; j++)
            //    {
            //        if (MapManager.GetMapFromIndex(0).MapData[i * MapManager.GetMapFromIndex(0).Width + j] != null &&
            //            !MapManager.GetMapFromIndex(0).MapData[i * MapManager.GetMapFromIndex(0).Width + j].IsDead)
            //        {
            //            switch (MapManager.GetMapFromIndex(0).MapData[i * MapManager.GetMapFromIndex(0).Width + j].Type)
            //            {
            //                case 0:
            //                    {
            //                        Tools.FieldToClient(j + minX, i + minY, ref newX, ref newY, MapManager.GetMapFromIndex(0).Height);
            //                        graphicBmp.DrawRectangle(pen, newX - 5, newY - 5, 10, 10);
            //                        graphic.DrawImage(bmp, 0, 0);
            //                        break;
            //                    }
            //                case 1:
            //                    {
            //                        Tools.FieldToClient(j + minX, i + minY, ref newX, ref newY, MapManager.GetMapFromIndex(0).Height);
            //                        graphicBmp.DrawRectangle(penPrey, newX - 3, newY - 3, 6, 6);
            //                        graphic.DrawImage(bmp, 0, 0);
            //                        break;
            //                    }
            //                default:
            //                    {
            //                        break;
            //                    }
            //            }
            //            
            //        }
            //    }
            //}
            bmp.Dispose();
            graphicBmp.Dispose();
        }

        private void KillToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            mapTimer.StopTimer();
        }
    }
}

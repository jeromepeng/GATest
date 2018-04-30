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
namespace GAFarm
{
    public partial class FarmField : Form
    {
        static int creatureNum = 50;
        Hunter[] hunters = new Hunter[creatureNum];
        ActionControl.ActionTimer mapTimer;
        Pen pen = new Pen(Color.White);
        Graphics graphic;
        public FarmField()
        {
            InitializeComponent();
            FieldMap thisMap = new FieldMap();
            thisMap.InitialMap(this.ClientRectangle.Right, this.ClientRectangle.Bottom);
            MapManager.AddMap(thisMap);
            mapTimer = new ActionControl.ActionTimer(42, new ActionControl.TimerAction(RefreshFarm));
            graphic = this.CreateGraphics();
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
        }

        private void DrawMap()
        {
            this.Refresh();
        }

        private void CreateToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            double[] mins = new double[4] { 0, 0, 0, 0 };
            double[] maxs = new double[4] { this.ClientRectangle.Right, this.ClientRectangle.Bottom, 2 * Math.PI, 200 };

            Creature[] radomHunterCreature = GACore.InitCreaturesPerValueOneLimit(creatureNum, 4, mins, maxs);
            Creature[] radomFoodCreature = GACore.InitCreaturesPerValueOneLimit(400, 4, mins, maxs);
            for (int i = 0; i < creatureNum; i++)
            {
                hunters[i] = new Hunter();
                hunters[i].Create(radomHunterCreature[i], new ActionHunter());
                MapManager.GetMapFromIndex(0).AddCreature(hunters[i]);
            }
            DrawMap();
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
            for (int i = 0; i < MapManager.GetMapFromIndex(0).Height; i++)
            {
                for (int j = 0; j < MapManager.GetMapFromIndex(0).Width; j++)
                {
                    if (MapManager.GetMapFromIndex(0).MapData[i * MapManager.GetMapFromIndex(0).Width + j] != 0)
                    {
                        Tools.FieldToClient(j, i, ref newX, ref newY, MapManager.GetMapFromIndex(0).Height);
                        graphicBmp.DrawRectangle(pen, newX - 5, newY - 5, 10, 10);
                        graphic.DrawImage(bmp, 0, 0);
                    }
                }
            }
            bmp.Dispose();
            graphicBmp.Dispose();
        }
    }
}

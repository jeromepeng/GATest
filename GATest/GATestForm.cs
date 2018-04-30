using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GA.Common;

namespace GATest
{
    public partial class GATestForm : Form
    {
        LiveRule liveRule = new LiveRule();
        public delegate void VoidDelegate();

        public GATestForm()
        {
            InitializeComponent();
            liveRule.OldRate = new double[] { 0.1, 0.2, 0.4, 0.2, 0.1 };
            liveRule.NewRate = new double[] { 0.4, 0.2, 0.2, 0.1, 0.1 };
            CBFunction.SelectedIndex = 0;
        }

        private void BtnStartClick(object sender, EventArgs e)
        {
            try
            {
                switch (CBFunction.SelectedIndex)
                {
                    case 0:
                        {
                            string info = GA.Core.GACore.GA(new double[] { System.Convert.ToDouble(tbForSinResult.Text) },
                                200,
                                GA.Common.GAFunctions.Sin,
                                -3.14,
                                3.14,
                                liveRule,
                                new object[] { new double[] { 1, -2, 1 }, 2 },
                                0.001,
                                0.618,
                                1000);
                            UpdateInfo(info);
                            UpdateSinResult(info);
                            break;
                        }
                    case 1:
                        {
                            string info = GetXFromFunctionString(tbForGetXFunction.Text);
                            UpdateInfo(info);
                            UpdateSinResult(info);
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                UpdateInfo(ex.ToString());
            }
        }

        private void UpdateInfo(string info)
        {
            this.Invoke(new VoidDelegate(delegate ()
            {
                this.RTBInfo.Text += info;
            }));
        }

        private void UpdateSinResult(string info)
        {
            this.Invoke(new VoidDelegate(delegate ()
            {
                this.tbForSinX.Text = FindResultFromInfo(info);
            }));
        }

        private void CBFunctionSelectedIndexChanged(object sender, EventArgs e)
        {
            HideAllControl();
            switch (CBFunction.SelectedIndex)
            {
                case 0:
                    {
                        ShowSinControl();
                        break;
                    }
                case 1:
                    {
                        ShowGetXControl();
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void HideAllControl()
        {
            lbForSinEqual.Visible = false;
            lbForSinSin.Visible = false;
            tbForSinResult.Visible = false;
            tbForSinX.Visible = false;
            tbForGetXFunction.Visible = false;
            tbForGetXResult.Visible = false;
            lbForGetXEqual.Visible = false;
        }

        private void ShowSinControl()
        {
            lbForSinEqual.Visible = true;
            lbForSinSin.Visible = true;
            tbForSinResult.Visible = true;
            tbForSinX.Visible = true;
        }

        private void ShowGetXControl()
        {
            tbForGetXFunction.Visible = true;
            tbForGetXResult.Visible = true;
            lbForGetXEqual.Visible = true;
        }

        private string GetXFromFunctionString(string fString)
        {
            fString = fString.Trim();
            int powNumber = 0;
            string[] segs = fString.Split(',');
            powNumber = segs.Length - 1;
            double[] parameters = new double[segs.Length];
            for (int i = 0; i < segs.Length; i++)
            {
                parameters[i] = System.Convert.ToDouble(segs[i]);
            }
            double result = System.Convert.ToDouble(tbForGetXResult.Text);
            return GA.Core.GACore.GA(new double[] { result },
                                200,
                                GA.Common.GAFunctions.GetX,
                                -100,
                                100,
                                liveRule,
                                new object[] { parameters, powNumber },
                                0.001,
                                0.618,
                                1000);
        }

        private string FindResultFromInfo(string info)
        {
            return info.Substring(info.IndexOf("Result:") + 7);
        }
    }
}

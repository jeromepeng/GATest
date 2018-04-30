using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GATest
{
    static class Program
    {
        /*static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                LiveRule liveRule = new LiveRule();
                liveRule.OldRate = new double[] { 0.1, 0.2, 0.4, 0.2, 0.1 };
                liveRule.NewRate = new double[] { 0.4, 0.2, 0.2, 0.1, 0.1 };*/
        //Console.WriteLine("Result = {0}", GA.Core.GACore.GA(new double[] {0}, 200, GA.Common.GAFunctions.GetX, -100, 100, liveRule, new object[] { new double[] { 1, -2, 1 }, 2 }));
        /*Console.WriteLine("Result = {0}", GA.Core.GACore.GA(new double[] { 6, 7 }, 
            2500, 
            GA.Common.GAFunctions.GetXAndY, 
            -100, 
            100, 
            liveRule, 
            new object[] {new double[] {2, 3, 3, -1}, 0 }, 
            0.0001, 
            0.618, 
            1000));*/
        /*Console.WriteLine("Result = {0}", GA.Core.GACore.GA(new double[] { 1, 6, 3 },
            2500,
            GA.Common.GAFunctions.GetXAndYAndZ,
            -100,
            100,
            liveRule,
            new object[] { new double[] { 6, 4, 2, 3, -1, 1, 5, 1, 1 }, 0 },
            0.0001,
            0.618,
            1000));*/
        /*Console.WriteLine("Result = {0}", GA.Core.GACore.GA(new double[] { 4, -2, 0, 2 },
            2500,
            GA.Common.GAFunctions.GetXAndYAndZAndMore,
            -10,
            10,
            liveRule,
            new object[] { new double[] { 
                1, 1, 1, 1, 
                1, 1, -1, -1, 
                1, -1, 1, -1, 
                1, 1, -1, 1 }, 0 },
            0.0001,
            0.618,
            1000));
    }
    Console.ReadKey();
}*/

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GATestForm());
        }
    }
}

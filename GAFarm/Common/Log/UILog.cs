using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAFarm.Common.Log
{
    public class UILog
    {
        private static TextBoxBase textBoxToLog = null;

        private static UILog instance = null;

        private UILog(TextBoxBase textBox)
        {
            textBoxToLog = textBox;
        }

        static public void CreateInstance(TextBoxBase textBox)
        {
            if (instance == null)
            {
                instance = new UILog(textBox);
            }
        }
        static public void LogToTextBox(string logText)
        {
            if (instance != null)
            {
                textBoxToLog.Text += logText + "\n";
            }
        }
    }
}

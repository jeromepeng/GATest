using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Common
{
    public delegate double[] Function(object[] paras, object[] inputs);

    public class Creature
    {
        public string Name
        {
            get
            {
                string result = string.Empty;
                for (int i = 0; i < Value.Length; i++)
                {
                    result += Value[i].ToString();
                    result += "; ";
                }
                result = result.Substring(0, result.Length - 2);
                return result;
            }
        }
        public double[] Value;
        public double Result;
        public override bool Equals(object obj)
        {
            bool result = true;
            if (obj == null)
            {
                result = false;
            }
            if (obj.GetType() != this.GetType())
            {
                result = false;
            }
            Creature input = obj as Creature;
            for (int i = 0; i < Value.Length; i++)
            {
                if (input.Value[i] != this.Value[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }

    public struct LiveRule
    {
        public double[] OldRate;
        public double[] NewRate;
    }

    public class GAFunctions
    {
        public static double[] Sin(object[] paras, object[] inputs)
        {
            return new double[] { Math.Sin((double)((double[])inputs[0])[0]) };
        }

        public static double[] GetX(object[] paras, object[] inputs)
        {
            double result = 0;
            int powNum = (int)paras[0];
            double[] para = paras[1] as double[];
            double input = ((double[])inputs[0])[0];
            if (para.Length == powNum + 1)
            {
                double seg = 1;
                for (int i = powNum; i > -1; i--)
                {
                    if (para[powNum - i] != 0)
                    {
                        seg = 1;
                        for (int j = 0; j < i; j++)
                        {
                            seg *= input;
                        }
                        seg *= para[powNum - i];
                    }
                    else
                    {
                        seg = 0;
                    }
                    result += seg;
                }
            }
            return new double[] { result };
        }

        public static double[] GetXAndY(object[] paras, object[] inputs)
        {
            double result1 = 0;
            double result2 = 0;
            double[] inputParas = paras[1] as double[];
            double[] inputInputs = inputs[0] as double[];
            switch (inputParas.Length)
            {
                case 6:
                    {
                        result1 = (double)inputParas[0] * (double)inputInputs[0] + (double)inputParas[1] * (double)inputInputs[1] - (double)inputParas[2];
                        result2 = (double)inputParas[3] * (double)inputInputs[0] + (double)inputParas[4] * (double)inputInputs[1] - (double)inputParas[5];
                        break;
                    }
                case 4:
                    {
                        result1 = (double)inputParas[0] * (double)inputInputs[0] + (double)inputParas[1] * (double)inputInputs[1];
                        result2 = (double)inputParas[2] * (double)inputInputs[0] + (double)inputParas[3] * (double)inputInputs[1];
                        break;
                    }
            }
            return new double[] { result1, result2 };
        }

        public static double[] GetXAndYAndZAndMore(object[] paras, object[] inputs)
        {
            double[] inputParas = paras[1] as double[];
            double[] inputInputs = inputs[0] as double[];
            double[] results = new double[inputInputs.Length];
            for (int i = 0; i < inputInputs.Length; i++)
            {
                results[i] = 0;
                for (int j = 0; j < inputInputs.Length; j++)
                {
                    results[i] += (double)inputParas[i * inputInputs.Length + j] * inputInputs[j];
                }
            }
            return results;
        }
    }
}

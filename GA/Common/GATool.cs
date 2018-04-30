using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.Common
{
    public class GATool
    {
        public static Creature[][] SplitCreature(Creature[] parents, double[] splitRate)
        {
            Creature[][] spliteResult = new Creature[splitRate.Length][];
            int totalLength = parents.Length;
            for (int i = 0; i < spliteResult.Length; i++)
            {
                int count = (int)(splitRate[i] * totalLength);
                spliteResult[i] = new Creature[count];
                spliteResult[i] = parents.Take(count).ToArray();
                parents = parents.Skip(count).ToArray();
            }
            return spliteResult;
        }

        public static Creature[] MergeCreature(Creature[][] creaturesInput, int count)
        {
            if (count == -1)
            {
                count = 0;
                for (int i = 0; i < creaturesInput.Length; i++)
                {
                    count += creaturesInput[i].Length;
                }
            }
            Creature[] result = new Creature[count];
            int index = 0;
            for (int i = 0; i < creaturesInput.Length; i++)
            {
                System.Buffer.BlockCopy(creaturesInput[i], 0, result, index, creaturesInput[i].Length);
                index += creaturesInput[i].Length;
            }
            return result;
        }
    }
}

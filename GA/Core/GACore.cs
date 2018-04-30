using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.Common;

namespace GA.Core
{
    public class GACore
    {
        #region Public Method
        public static string GA(double[] value, int creatureNum, Function function, double min, double max, LiveRule liveRule, object[] otherParas, double accuracy, double unMutantRate, int maxGenerate)
        {
            Creature[] creatures = InitCreatures(creatureNum, value.Length, min, max);
            creatures = LiveAndOrder(creatures, creatureNum, value, function, otherParas);
            creatures = Mutant(creatures, 0.9, creatureNum, min, max);
            int time = 0;
            long begin = System.DateTime.Now.Ticks;
            double bestResult = 0;
            string result = string.Empty;
            while (!IfStop(creatures, accuracy) && time < maxGenerate)
            {
                creatures = GetNextGenerator2(creatures, creatureNum, liveRule);
                creatures = LiveAndOrder(creatures, creatureNum, value, function, otherParas);
                creatures = Mutant(creatures, unMutantRate, creatureNum, min, max);
                time++;
                result += creatures[0].Result.ToString() + '\n';
                /*if (Math.Abs(bestResult - creatures[0].Result) < 0.00000001)
                {
                    GA(value, creatureNum, function, 
                }*/
                //ShowProgress(time);
            }
            result += "Generator Num:" + time + '\n';
            result += "Time Cost:" + (System.DateTime.Now.Ticks - begin) / 10000 + '\n';
            result += "Result:" + creatures[0].Name + '\n';
            return result;
        }
        #endregion

        #region Private Method
        public static void ShowProgress(int callTime)
        {

            if (callTime % 1000 == 0)
            {
                int index = callTime / 1000;
                switch (index % 3)
                {
                    case 0:
                        {
                            Console.Clear();
                            Console.WriteLine("/");
                            break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("--");
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("\\");
                            break;
                        }
                }
            }
        }

        public static Creature[] InitCreatures(int creatureNum, int valueNum, double min, double max)
        {
            Creature[] creatures = new Creature[creatureNum];
            double span = max - min;
            for (int i = 0; i < creatureNum; i++)
            {
                creatures[i] = new Creature();
                creatures[i].Value = new double[valueNum];
                for (int j = 0; j < valueNum; j++)
                {
                    creatures[i].Value[j] = min + seedGenerator.NextDouble() * span;
                }
            }
            return creatures;
        }

        public static Creature[] InitCreaturesPerValueOneLimit(int creatureNum, int valueNum, double[] min, double[] max)
        {
            Creature[] creatures = new Creature[creatureNum];

            for (int i = 0; i < creatureNum; i++)
            {
                creatures[i] = new Creature();
                creatures[i].Value = new double[valueNum];
                for (int j = 0; j < valueNum; j++)
                {
                    creatures[i].Value[j] = min[j] + seedGenerator.NextDouble() * (max[j] - min[j]);
                }
            }
            return creatures;
        }

        public static bool IfStop(Creature[] creatures, double limit)
        {
            return creatures.First().Result < limit;
        }

        public static Creature[] LiveAndOrder(Creature[] creatures, int creatureNum, double[] value, Function function, object[] otherParas)
        {
            for (int i = 0; i < creatureNum; i++)
            {
                double[] functionResult = function(new object[] { (int)otherParas[1], otherParas[0] as double[] }, new object[] { creatures[i].Value });
                for (int j = 0; j < value.Length; j++)
                {
                    creatures[i].Result += Math.Abs(functionResult[j] - value[j]);
                }
            }
            IOrderedEnumerable<Creature> result = creatures.OrderBy(i => i.Result);
            return result.ToArray();
        }

        /// <summary>
        /// Get the best part of the creatures.
        /// </summary>
        /// <param name="allCreatures"></param>
        /// <param name="threshold">How many left (1 = 100%)</param>
        /// <returns></returns>
        public static Creature[] GetBestCreatures(Creature[] allCreatures, double partial)
        {
            List<Creature> bestCreatures = new List<Creature>();
            Creature[] tempResult = allCreatures.OrderByDescending(i => i.Result).ToArray<Creature>();
            for (int i = 0; i < tempResult.Length * partial; i++)
            {
                bestCreatures.Add(tempResult[i]);
            }
            return bestCreatures.ToArray();
        }

        public static Creature[] Mutant(Creature[] creatures, double mutantRate, int creatureNum, double min, double max)
        {
            double span = max - min;
            for (int i = (int)(creatureNum * mutantRate); i < creatureNum; i++)
            {
                for (int j = 0; j < creatures[i].Value.Length; j++)
                {
                    creatures[i].Value[j] = min + seedGenerator.NextDouble() * span;
                }
            }
            return creatures;
        }

        public static Creature[] MutantPerValueOneLimit(Creature[] creatures, double mutantRate, double[] min, double[] max)
        {
            for (int i = 0; i < min.Length; i++)
            {
                double span = max[i] - min[i];
                for (int j = (int)(creatures.Length * mutantRate); j < creatures.Length; j++)
                {
                    creatures[j].Value[i] = min[i] + seedGenerator.NextDouble() * span;
                }
            }
            return creatures;
        }

        public static Creature[] GetNextGenerator1(Creature[] parents, int creatureNum)
        {
            Creature[] children = new Creature[creatureNum];
            for (int i = 0; i < creatureNum / 2; i++)
            {
                children[i * 2] = new Creature();
                children[i * 2 + 1] = new Creature();
                Creature father = parents[seedGenerator.Next(creatureNum)];
                Creature mother = parents[seedGenerator.Next(creatureNum)]; ;
                while (father == mother)
                {
                    mother = parents[seedGenerator.Next(creatureNum)];
                }
                children[i * 2].Value = new double[father.Value.Length];
                children[i * 2 + 1].Value = new double[father.Value.Length];
                for (int j = 0; j > father.Value.Length; j++)
                {
                    bool flag = father.Value[j] > mother.Value[j];
                    double abs = Math.Abs(father.Value[j] - mother.Value[j]);
                    children[i * 2].Value[j] = flag ? mother.Value[j] + abs * 0.333 : father.Value[j] + abs * 0.333;
                    children[i * 2 + 1].Value[j] = flag ? mother.Value[j] + abs * 0.666 : father.Value[j] + abs * 0.666;
                }
            }
            return children;
        }

        public static Creature[] GetNextGenerator2(Creature[] parents, int creatureNum, LiveRule liveRule)
        {
            Creature[] children = new Creature[creatureNum];
            Creature[][] parentsInSplit = GATool.SplitCreature(parents, liveRule.OldRate);
            int index = 0;
            for (int i = 0; i < liveRule.NewRate.Length; i++)
            {
                int levelCreature = (int)(creatureNum * liveRule.NewRate[i]);
                for (int j = 0; j < levelCreature - 1; j++)
                {
                    children[index + j] = new Creature();
                    /*Creature father = parents[seedGenerator.Next(index, index + levelCreature)];
                    Creature mother = parents[seedGenerator.Next(index, index + levelCreature)];*/
                    Creature father = parents[j];
                    Creature mother = parents[j + 1];
                    while (father == mother)
                    {
                        mother = parents[seedGenerator.Next(index, index + levelCreature)];
                    }
                    children[index + j].Value = new double[father.Value.Length];
                    for (int k = 0; k < father.Value.Length; k++)
                    {
                        double baseValue = father.Value[k] > mother.Value[k] ? mother.Value[k] : father.Value[k];
                        double distance = Math.Abs(father.Value[k] - mother.Value[k]);
                        /*children[index + j].Value[k] = baseValue + Math.Abs(father.Value[k] - mother.Value[k]) * 0.618;*/
                        children[index + j].Value[k] = baseValue - distance + seedGenerator.NextDouble() * 3 * distance;
                    }
                }
                index += levelCreature;
                children[index - 1] = new Creature();
                children[index - 1].Value = new double[parents[index - 1].Value.Length];
                for (int k = 0; k < parents[index - 1].Value.Length; k++)
                {
                    children[index - 1].Value[k] = parents[index - 1].Value[k];
                }
            }
            return children;
        }
        #endregion

        #region Private Member
        static Random seedGenerator = new Random(System.DateTime.Now.Millisecond);
        #endregion
    }
}

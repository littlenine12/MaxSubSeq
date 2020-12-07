using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MaxSubSeq
{
    internal static class Program
    {
        private static readonly double[] Seq = File.ReadAllText("1.seq").Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Convert.ToDouble).ToArray();
        private static readonly Dictionary<double, List<(int, int)>> Dict = new Dictionary<double, List<(int, int)>>();

        private static void Main()
        {
            Calc();
            Print();
        }

        private static void Calc()
        {
            double max = 0, sum = 0;
            int zeroPos = 0, maxPos = 0;
            for (int i = 0; i < Seq.Length; ++i)
            {
                sum += Seq[i];
                if (sum <= 0)
                {
                    Add(max, (zeroPos, maxPos));
                    max = 0;
                    sum = 0;
                    zeroPos = i + 1;
                    maxPos = i;
                }

                if (Seq[i] > 0 && sum > max)
                {
                    max = sum;
                    maxPos = i;
                }
            }
            if (maxPos >= zeroPos) Add(max, (zeroPos, maxPos));
        }


        private static void Add(double value, (int, int) tuple)
        {
            if (Dict.ContainsKey(value)) Dict[value].Add(tuple);
            else Dict.Add(value, new List<(int, int)> { tuple });
        }

        private static void Print()
        {
            double maxvalue = Dict.Keys.Max();
            Console.WriteLine("最大子序列的值为{0},子序列为", maxvalue);
            foreach (var (item1, item2) in Dict[maxvalue])
            {
                if (item1 == item2)
                    Console.WriteLine("[{0}],在位置{1}", maxvalue, item1);
                else
                    Console.WriteLine("[{0}],在位置{1}-{2}", string.Concat(Seq.Skip(item1).Take(1 + item2 - item1).Aggregate("", (current, i) => current + $"{i},").SkipLast(1)), item1, item2);
            }
        }

    }
}

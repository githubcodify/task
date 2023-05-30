using System;
using System.Threading.Tasks;

namespace Practice
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int n = 100000;
            double[] array = new double[n];
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-10, 0);
            }

            int c = 3;
            int segmentLength = (int)Math.Ceiling(n / (double)c);
            Task<double>[] tasks = new Task<double>[c];

            for (int i = 0; i < c; i++)
            {
                int start = i * segmentLength;
                int end = Math.Min((i + 1) * segmentLength, n);
                tasks[i] = new Task<double>(() =>
                {
                    return Sum(array, start, end);
                });
                tasks[i].Start();
            }
            Task.WaitAll(tasks);

            double summary = 0;
            for (int i = 0; i < c; i++)
            {
                summary += tasks[i].Result;
                Console.WriteLine($"Task {i + 1} result is: {tasks[i].Result}");
            }

            Console.WriteLine($"Average is: {summary / n}");
            Console.ReadLine();
        }

        static double Sum(double[] array, int start, int end)
        {
            double sum = 0;
            for (int i = start; i < end; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}

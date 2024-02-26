using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tilfældigheder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //
            // First part of code
            //
            List<int> randomInts = new List<int>();
            List<int> rngInts = new List<int>();


            RandomTest(100, randomInts);

            randomInts.Sort();

            foreach (int i in randomInts)
            {
                Console.WriteLine($"Random number: {i}");
            }


            RandomNumberGeneratorCreateTest(100, rngInts);

            rngInts.Sort();
            foreach (int i in rngInts)
            {
                Console.WriteLine($"RNG number: {i}");
            }


            //
            // Benchmark part of code
            //
            List<int> randomBenchmarkList = new List<int>();
            List<int> rngBenchmarkList = new List<int>();


            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Random Benchmark:\n");

            stopwatch.Start();
            RandomTestBenchmark(randomBenchmarkList);
            stopwatch.Stop();
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();

            Console.WriteLine("Random Number Generator Benchmark:\n");

            stopwatch.Start();
            RNGCreateTestBenchmark(rngBenchmarkList);
            stopwatch.Stop();
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");







            Console.ReadLine();
        }

        static void RandomTest(int amountOfTest, List<int> intList)
        {
            Console.WriteLine("Random Test:\n");
            Random random = new Random();
            for (int i = 0; i < amountOfTest; i++)
            {
                intList.Add(random.Next(0, 1000));
            }
        }

        static void RandomTestBenchmark(List<int> intList)
        {
            Random random = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                intList.Add(random.Next(0, 1000));
            }
        }

        static void RandomNumberGeneratorCreateTest(int amountOfTest, List<int> intList)
        {
            List<byte[]> Test = new List<byte[]>();
            Console.WriteLine("Random Number Generator Test:\n");
            for (int i = 0; i < amountOfTest; i++)
            {
                byte[] randomBytes = new byte[4];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomBytes);
                }
                Test.Add(randomBytes);
                //int randomInt = BitConverter.ToInt32(randomBytes, 0);
                //randomInt %= 1000;
                //if (randomInt < 0)
                //{
                //    randomInt = randomInt * -1;
                //}
                //intList.Add(randomInt);
            }
        }

        static void RNGCreateTestBenchmark(List<int> intList)
        {
            for (int i = 0; i < 1000000; i++)
            {
                byte[] randomBytes = new byte[4];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomBytes);
                }
                int randomInt = BitConverter.ToInt32(randomBytes, 0);
                randomInt %= 1000;
                if (randomInt < 0)
                {
                    randomInt = randomInt * -1;
                }
                intList.Add(randomInt);
            }
        }
    }
}
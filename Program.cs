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


            RandomTest(100, randomInts); // run Random Test Method

            randomInts.Sort(); // sort the numbers so its easier to read
            foreach (int i in randomInts)
            {
                Console.WriteLine($"Random number: {i}"); // writes result
            }


            RandomNumberGeneratorCreateTest(100, rngInts); // run RNGCryptoServiceProvider Test Method

            rngInts.Sort(); // sort the numbers so its easier to read
            foreach (int i in rngInts)
            {
                Console.WriteLine($"RNG number: {i}"); // writes result
            }


            //
            // Benchmark part of code
            //
            Console.WriteLine("\n");


            List<int> randomBenchmarkList = new List<int>();
            List<int> rngBenchmarkList = new List<int>();


            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Random Benchmark:\n");

            stopwatch.Start(); // starts stopwatch
            RandomTestBenchmark(randomBenchmarkList); // run benchmark method
            stopwatch.Stop(); // stops stopwatch
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms"); // writes how much time it used in milliseconds

            stopwatch.Restart(); // Restart stopwatch, so it ready for next test

            Console.WriteLine("Random Number Generator Benchmark:\n");

            stopwatch.Start(); // starts stopwatch
            RNGCreateTestBenchmark(rngBenchmarkList); // run benchmark method
            stopwatch.Stop(); // stops stopwatch
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms"); // writes how much time it used in milliseconds

            //
            // Randomness Code from moodle
            //
            Console.WriteLine("\n");


            // Definer antal test og arraystørrelse
            int numberOfTests = 10;
            int arraySize = 4;

            // RNGCryptoServiceProvider tilfældighedstest
            TestRandomnessWithRNGCryptoServiceProvider(numberOfTests, arraySize);

            // Random tilfældighedstest
            TestRandomnessWithRandom(numberOfTests, arraySize);


            Console.ReadLine(); // to not close
        }

        // Random Test
        static void RandomTest(int amountOfTest, List<int> intList)
        {
            Console.WriteLine("Random Test:\n");
            Random random = new Random();
            for (int i = 0; i < amountOfTest; i++)
            {
                intList.Add(random.Next(0, 1000));
            }
        }

        // Random 1 million Test for benchmark
        static void RandomTestBenchmark(List<int> intList)
        {
            Random random = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                intList.Add(random.Next(0, 1000));
            }
        }

        // RNGCryptoServiceProvider Test
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
            }
        }

        // RNGCryptoServiceProvider 1 million test for benchmark
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

        // Randomness Code from moodle
        private static void TestRandomnessWithRNGCryptoServiceProvider(int numberOfTests, int arraySize)
        {
            Console.WriteLine("RNGCryptoServiceProvider tilfældighedstest:\n");

            for (int i = 0; i < numberOfTests; i++)
            {
                byte[] randomBytes = new byte[arraySize];
                using (var randomNumberGenerator = RandomNumberGenerator.Create())
                {
                    randomNumberGenerator.GetBytes(randomBytes);
                }

                int randomInt = BitConverter.ToInt32(randomBytes, 0);
                Console.WriteLine($"Test {i + 1}: {BitConverter.ToString(randomBytes).Replace("-", "")} => {randomInt}");
            }
        }

        // Randomness Code from moodle
        private static void TestRandomnessWithRandom(int numberOfTests, int arraySize)
        {
            Console.WriteLine("Random tilfældighedstest:\n");

            Random random = new Random();
            for (int i = 0; i < numberOfTests; i++)
            {
                byte[] randomBytes = new byte[arraySize];
                random.NextBytes(randomBytes);

                int randomInt = BitConverter.ToInt32(randomBytes, 0);
                Console.WriteLine($"Test {i + 1}: {BitConverter.ToString(randomBytes).Replace("-", "")} => {randomInt}");
            }
        }
    }
}
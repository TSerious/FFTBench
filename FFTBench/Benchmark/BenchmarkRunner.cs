using System;
using System.Diagnostics;

namespace FFTBench.Benchmark
{
    class BenchmarkRunner
    {
        int innerIterations;

        public BenchmarkRunner(int innerIterations = 50)
        {
            this.innerIterations = innerIterations;
        }

        public BenchmarkResult Run(ITest test, double[] data, int repeat)
        {
            var timer = new Stopwatch();

            test.Initialize(data);

            // Warmup.
            test.FFT(true);

            var result = new BenchmarkResult(data.Length);

            for (int i = 0; i < repeat; i++)
            {
                timer.Restart();

                for (int j = 0; j < innerIterations; j++)
                {
                    test.FFT(true);
                }

                timer.Stop();

                var t = timer.ElapsedTicks;

                result.Minimum = Math.Min(result.Minimum, t);
                result.Maximum = Math.Max(result.Maximum, t);

                result.Total += t;
            }

            result.Minimum = (long)TimeSpan.FromTicks(result.Minimum).TotalMilliseconds;
            result.Maximum = (long)TimeSpan.FromTicks(result.Maximum).TotalMilliseconds;

            result.Total = (long)TimeSpan.FromTicks(result.Total).TotalMilliseconds;
            result.Average = (double)result.Total / (repeat * innerIterations);

            return result;
        }
    }
}


namespace FFTBench.Benchmark
{
    public struct BenchmarkResult
    {
        /// <summary>
        /// The minimum runtime in milliseconds.
        /// </summary>
        public long Minimum { get; set; }

        /// <summary>
        /// The maximum runtime in milliseconds.
        /// </summary>
        public long Maximum { get; set; }

        /// <summary>
        /// The average runtime in milliseconds.
        /// </summary>
        public double Average { get; set; }

        /// <summary>
        /// The average runtime in ticks.
        /// </summary>
        public double AverageTicks { get; set; }

        /// <summary>
        /// The total runtime (with repeats) in milliseconds.
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// The FFT size.
        /// </summary>
        public int Size { get; set; }

        public BenchmarkResult(int size)
        {
            this.Size = size;
            this.Minimum =
            this.Maximum = long.MaxValue;
            this.Average =
            this.AverageTicks = -1;
            this.Total = 0;
        }
    }
}

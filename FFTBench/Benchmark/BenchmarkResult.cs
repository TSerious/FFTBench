
namespace FFTBench.Benchmark
{
    public class BenchmarkResult
    {
        /// <summary>
        /// The minimum runtime in milliseconds.
        /// </summary>
        public long Minimum;

        /// <summary>
        /// The maximum runtime in milliseconds.
        /// </summary>
        public long Maximum;

        /// <summary>
        /// The average runtime in milliseconds.
        /// </summary>
        public double Average;

        /// <summary>
        /// The total runtime (with repeats) in milliseconds.
        /// </summary>
        public long Total;

        /// <summary>
        /// The FFT size.
        /// </summary>
        public int Size;

        public BenchmarkResult(int size)
        {
            this.Size = size;
            this.Minimum = long.MaxValue;
        }
    }
}

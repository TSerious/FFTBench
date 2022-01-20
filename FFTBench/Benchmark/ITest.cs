
namespace FFTBench.Benchmark
{
    interface ITest
    {
        bool Enabled { get; set; }

        /// <summary>
        /// Prepare the real valued data for FFT processing.
        /// </summary>
        /// <param name="data"></param>
        void Initialize(double[] data);

        /// <summary>
        /// Apply FFT to data.
        /// </summary>
        /// <param name="forward">If false, inverse FFT should be applied.</param>
        void FFT(bool forward);

        // Ignore for benchmark (used only for 'FFT Explorer')
        double[] Spectrum(double[] input, bool scale, out double[] backwardResult);
    }
}

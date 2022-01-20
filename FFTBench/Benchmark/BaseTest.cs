using System.Numerics;

namespace FFTBench.Benchmark
{
    public abstract class BaseTest : ITest
    {
        protected Complex[] data;
        protected Complex[] copy;

        public bool Enabled { get; set; }

        public virtual void Initialize(double[] data)
        {
            Helper.ToComplex(data, out this.data);
            this.copy = new Complex[data.Length];
        }

        public abstract void FFT(bool forward);

        public abstract double[] Spectrum(double[] input, bool scale, out double[] backwardResult);
    }
}

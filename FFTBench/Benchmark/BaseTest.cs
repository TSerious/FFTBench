using System.Numerics;

namespace FFTBench.Benchmark
{
    public abstract class BaseTest : ITest
    {
        protected int size;

        protected Complex[] data;
        protected Complex[] copy;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public virtual void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            Helper.ToComplex(data, out this.data);
            this.copy = new Complex[data.Length];

            size = data.Length;//Util.Log2(length);
        }

        public abstract void FFT(bool forward);

        public abstract double[] Spectrum(double[] input, bool scale, out double[] backwardResult);
    }
}

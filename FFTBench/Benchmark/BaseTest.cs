using System.Numerics;

namespace FFTBench.Benchmark
{
    public abstract class BaseTest : ITest
    {
        protected int size;

        protected Complex[] data;
        protected Complex[] copy;

        public bool Enabled { get; set; }
        
        public virtual void Initialize(double[] data)
        {
            int length = data.Length;

            this.data = ToComplex(data);
            this.copy = new Complex[length];

            size = Util.Log2(length);
        }

        public abstract void FFT(bool forward);

        public abstract double[] Spectrum(double[] input, bool scale);

        protected Complex[] ToComplex(double[] data)
        {
            int length = data.Length;

            var result = new Complex[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = new Complex(data[i], 0.0);
            }

            return result;
        }

        protected void ToDouble(Complex[] data, double[] target)
        {
            int length = data.Length;

            for (int i = 0; i < length; i++)
            {
                target[i] = data[i].Real;
            }
        }

        protected double[] ComputeSpectrum(Complex[] fft)
        {
            int length = fft.Length;

            var result = new double[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = fft[i].Magnitude;
            }

            return result;
        }
    }
}

using System;
using System.Numerics;
using MKLNET;

namespace FFTBench.Benchmark
{
    public class TestMKLNET : ITest
    {
        private IntPtr descriptor = new IntPtr();
        Complex[] input;
        Complex[] output;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            Helper.ToComplex(data, out input);
            output = new Complex[data.Length];
        }

        public void FFT(bool forward)
        {
            if (Dfti.ComputeForward(input, output) != 0)
            {
                throw new Exception(this + ": Can't run fft");
            }
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            Helper.ToComplex(input, out Complex[] data1);
            Complex[] data2 = new Complex[input.Length];

            if(Dfti.ComputeForward(data1, data2) != 0)
            {
                throw new Exception(this + ": Can't run fft");
            }

            System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(data2, SignalGenerator.TestArrayFFTresult()));
            var spectrum = Helper.ComputeSpectrum(data2);

            Dfti.ComputeBackward(data2, data1);
            backwardResult = Helper.ToReal(data1);
            Helper.Scale(ref backwardResult, scale);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "MKL.NET";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}

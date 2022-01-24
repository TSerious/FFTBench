using FFTW.NET;
using System.Numerics;
using System;

namespace FFTBench.Benchmark
{
    public class TestFFTW : ITest
    {
        FftwArrayComplex input;
        FftwArrayComplex output;
        FftwPlanC2C plan;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            input = new FftwArrayComplex(data.Length);
            output = new FftwArrayComplex(data.Length);
            Fill(data, ref input);
            plan = FftwPlanC2C.Create(input, output, DftDirection.Forwards);
        }

        public void FFT(bool forward)
        {
            plan.Execute();
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            var data1 = new FftwArrayComplex(input.Length);
            var data2 = new FftwArrayComplex(input.Length);

            using (var plan1 = FftwPlanC2C.Create(data1, data2, DftDirection.Forwards, PlannerFlags.Estimate ))
            using (var plan2 = FftwPlanC2C.Create(data2, data1, DftDirection.Backwards, PlannerFlags.Estimate ))
            {
                Fill(input, ref data1);
                plan1.Execute();
                var spectrum = ComputeSpectrum(data2);
                System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(ComplexToComplex(data2), SignalGenerator.TestArrayFFTresult()));
                plan2.Execute();
                backwardResult = ToReal(data1);
                Helper.Scale(ref backwardResult, scale);

                return spectrum;
            }
        }

        public override string ToString()
        {
            string name = "FFTW";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }

        public static void Fill(double[] realData, ref FftwArrayComplex data)
        {
            if (data.Length != realData.Length)
            {
                throw new NotImplementedException("FFTW: Can't fill arrays of different size.");
            }

            for (int i = 0; i < realData.Length; i++)
            {
                data[i] = new Complex(realData[i], 0.0);
            }
        }

        public static double[] ComputeSpectrum(FftwArrayComplex fft)
        {
            var result = new double[fft.Length];

            for (int i = 0; i < fft.Length; i++)
            {
                result[i] = fft[i].Magnitude;
            }

            return result;
        }

        public static double[] ToReal(FftwArrayComplex data)
        {
            double[] target = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                target[i] = data[i].Real;
            }

            return target;
        }

        public static Complex[] ComplexToComplex(FftwArrayComplex data)
        {
            Complex[] res = new Complex[data.Length];

            for (int i = 0; i<data.Length; i++)
            {
                res[i] = data[i];
            }

            return res;
        }
    }
}

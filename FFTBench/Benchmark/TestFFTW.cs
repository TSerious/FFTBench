using FFTW.NET;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestFFTW : ITest
    {
        FftwArrayComplex input;
        FftwArrayComplex output;
        FftwPlanC2C plan;

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            int length = data.Length >> 1;

            input = new FftwArrayComplex(length);
            output = new FftwArrayComplex(length);

            for (int i = 0; i < length; i++)
            {
                input[i] = new Complex(data[i * 2], data[(i * 2) + 1]);
            }

            plan = FftwPlanC2C.Create(input, output, DftDirection.Forwards);
        }

        public void FFT(bool forward)
        {
            plan.Execute();
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            using (var data1 = new FftwArrayComplex(input.Length))
            using (var data2 = new FftwArrayComplex(input.Length))
            using (var plan1 = FftwPlanC2C.Create(data1, data2, DftDirection.Forwards, PlannerFlags.Estimate ))
            using (var plan2 = FftwPlanC2C.Create(data2, data1, DftDirection.Backwards, PlannerFlags.Estimate))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    data1[i] = new Complex(input[i], 0.0);
                }

                plan1.Execute();

                float[] temp = new float[input.Length << 1];
                for(int i = 0; i < input.Length; i++)
                {
                    temp[(i * 2)] = (float)data2[i].Real;
                    temp[(i * 2) + 1] = (float)data2[i].Imaginary;
                }
                var spectrum = Helper.ComputeSpectrum(temp);

                plan2.Execute();

                for(int i = 0; i < data1.Length; i++)
                {
                    input[i] = data1[i].Real;
                }

                if (scale)
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        input[i] /= input.Length;
                    }
                }

                return spectrum;
            }
        }

        public override string ToString()
        {
            return "FFTW";
        }
    }
}

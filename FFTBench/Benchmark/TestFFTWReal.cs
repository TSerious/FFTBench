using FFTW.NET;

namespace FFTBench.Benchmark
{
    public class TestFFTWReal : ITest
    {
        double[] data;

        PinnedArray<double> input;
        FftwArrayComplex output;

        FftwPlanRC plan;

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            this.data = (double[])data.Clone();

            input = new PinnedArray<double>(data);
            output = new FftwArrayComplex(DFT.GetComplexBufferSize(input.GetSize()));

            plan = FftwPlanRC.Create(input, output, DftDirection.Forwards, PlannerFlags.Estimate);
        }

        public void FFT(bool forward)
        {
            plan.Execute();
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            int length = input.Length;
            
            using (var data1 = new PinnedArray<double>(input))
            using (var data2 = new FftwArrayComplex(DFT.GetComplexBufferSize(data1.GetSize())))
            using (var data3 = new FftwArrayComplex(DFT.GetComplexBufferSize(data1.GetSize())))
            using (var plan1 = FftwPlanRC.Create(data1, data2, DftDirection.Forwards, PlannerFlags.Estimate))
            using (var plan2 = FftwPlanC2C.Create(data2, data3, DftDirection.Backwards, PlannerFlags.Estimate))
            {
                plan1.Execute();

                float[] temp = new float[input.Length];
                for (int i = 0; i < input.Length>>1; i++)
                {
                    temp[(i * 2)] = (float)data2[i].Real;
                    temp[(i * 2) + 1] = (float)data2[i].Imaginary;
                }
                var spectrum = Helper.ComputeSpectrum(temp);

                plan2.Execute();

                for (int i = 0; i < data3.Length; i++)
                {
                    input[i] = data3[i].Real;
                }

                if (scale)
                {
                    for (int i = 0; i < length; i++)
                    {
                        input[i] /= length;
                    }
                }

                return spectrum;
            }
        }

        public override string ToString()
        {
            return "FFTW (real)";
        }
    }
}

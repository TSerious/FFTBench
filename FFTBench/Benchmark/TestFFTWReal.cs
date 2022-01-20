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

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            this.data = (double[])data.Clone();

            input = new PinnedArray<double>(data);
            output = new FftwArrayComplex(DFT.GetComplexBufferSize(input.GetSize()));
            plan = FftwPlanRC.Create(input, output, DftDirection.Forwards, PlannerFlags.Estimate);
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

            var data1 = new PinnedArray<double>(input);
            var data2 = new FftwArrayComplex(DFT.GetComplexBufferSize(data1.GetSize()));

            using (var data3 = new FftwArrayComplex(DFT.GetComplexBufferSize(data1.GetSize())))
            using (var plan1 = FftwPlanRC.Create(data1, data2, DftDirection.Forwards, PlannerFlags.Estimate))
            using (var plan2 = FftwPlanC2C.Create(data2, data3, DftDirection.Backwards, PlannerFlags.Estimate))
            {
                plan1.Execute();
                var spectrum = TestFFTW.ComputeSpectrum(data2);
                plan2.Execute();
                backwardResult = TestFFTW.ToReal(data3);
                Helper.Scale(ref backwardResult, scale);

                return spectrum;
            }
        }

        public override string ToString()
        {
            string name = "FFTW (real)";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}

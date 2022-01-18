using PetterPet.FFTSSharp;

namespace FFTBench.Benchmark
{
    class TestFFTS : ITest
    {
        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            int length = data.Length >> 1;

            FFTSManager.LoadAppropriateDll(FFTSManager.InstructionType.Auto);

            FFTS.Complex(FFTS.Forward, data.Length);

            /*
            input = new FftwArrayComplex(length);
            output = new FftwArrayComplex(length);

            for (int i = 0; i < length; i++)
            {
                input[i] = new Complex(data[i * 2], data[(i * 2) + 1]);
            }

            plan = FftwPlanC2C.Create(input, output, DftDirection.Forwards);
            */
        }

        public void FFT(bool forward)
        {
            //plan.Execute();
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            return input;
        }
    }
}

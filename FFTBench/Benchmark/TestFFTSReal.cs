using PetterPet.FFTSSharp;

namespace FFTBench.Benchmark
{
    class TestFFTSReal : ITest
    {
        private float[] input;
        private float[] output;
        private FFTS plan;

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            FFTSManager.LoadAppropriateDll(FFTSManager.InstructionType.Auto);

            plan = FFTS.Real(FFTS.Forward, data.Length);

            input = ToFloat(data);
            output = new float[plan.outSize];
        }

        public void FFT(bool forward)
        {
            plan.Execute(input, output);
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            FFTSManager.LoadAppropriateDll(FFTSManager.InstructionType.Auto);

            using (var plan1 = FFTS.Real(FFTS.Forward, input.Length))
            using (var plan2 = FFTS.Real(FFTS.Backward, input.Length))
            {
                
                var data1 = ToFloat(input);
                var data2 = new float[plan1.outSize];

                plan1.Execute(data1, data2);

                float[] temp = new float[input.Length << 1];
                for (int i = 0; i < data2.Length; i++)
                {
                    temp[i] = (float)data2[i];
                }
                var spectrum = Helper.ComputeSpectrum(temp);

                plan2.Execute(data2, data1);

                for (int i = 0; i < input.Length; i++)
                {
                    input[i] = data1[i];
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
            return "FFTS (real)";
        }

        private float[] ToFloat(double[] data)
        {
            float[] f = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                f[i] = (float)data[i];
            }

            return f;
        }
    }
}

using PetterPet.FFTSSharp;
using System.Numerics;

namespace FFTBench.Benchmark
{
    class TestFFTSReal : ITest
    {
        private float[] input;
        private float[] output;
        private FFTS plan;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            FFTSManager.LoadAppropriateDll(FFTSManager.InstructionType.Auto);

            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            plan = FFTS.Real(FFTS.Forward, data.Length);
            input = Helper.ConvertToFloat(data);
            output = new float[plan.outSize];
        }

        public void FFT(bool forward)
        {
            plan.Execute(input, output);
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            FFTSManager.LoadAppropriateDll(FFTSManager.InstructionType.Auto);

            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            using (var plan1 = FFTS.Real(FFTS.Forward, input.Length))
            using (var plan2 = FFTS.Real(FFTS.Backward, input.Length))
            {
                var data1 = Helper.ConvertToFloat(input);
                var data2 = new float[plan1.outSize];

                plan1.Execute(data1, data2);
                Helper.ComplexToComplex(Helper.ConvertToDouble(data2), out Complex[] res);
                System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(res, SignalGenerator.TestArrayFFTresult()));
                var spectrum = Helper.ComputeSpectrum(data2);
                plan2.Execute(data2, data1);
                backwardResult = Helper.ConvertToDouble(Helper.ToReal(data1));
                Helper.Scale(ref backwardResult, scale);
                
                return spectrum;
            }
        }

        public override string ToString()
        {
            string name = "FFTS_32(real)";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}

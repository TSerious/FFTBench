using PetterPet.FFTSSharp;
using System.Numerics;

namespace FFTBench.Benchmark
{
    class TestFFTS : ITest
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

            plan = FFTS.Complex(FFTS.Forward, data.Length);
            input = Helper.ToComplexF(data);
            output = new float[plan.outSize];
        }

        public void FFT(bool forward)
        {
            plan.Execute(input, output);
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            FFTSManager.LoadAppropriateDll(FFTSManager.InstructionType.Auto);

            int originalSize = input.Length;
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            using (var plan1 = FFTS.Complex(FFTS.Forward, input.Length))
            using (var plan2 = FFTS.Complex(FFTS.Backward, input.Length))
            {
                var data1 = Helper.ToComplexF(input);
                var data2 = new float[plan1.outSize];

                plan1.Execute(data1, data2);
                Helper.ComplexToComplex(Helper.ConvertToDouble(data2), out Complex[] res);
                System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(res, SignalGenerator.TestArrayFFTresult()));
                var spectrum = Helper.ComputeSpectrum(data2);
                plan2.Execute(data2, data1);
                backwardResult = Helper.ConvertToDouble(Helper.ToReal(data1));
                Helper.UndoStretch(ref backwardResult, originalSize);

                Helper.Scale(ref backwardResult, scale);

                return spectrum;
            }
        }

        public override string ToString()
        {
            string name = "FFTS_32";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}

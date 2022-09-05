using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FFTBench.Benchmark
{
    public abstract class TestMathNetBase : BaseTest
    {
        public bool StretchInput { get; set; }

        public bool SingleThreaded { get; set; } = true;

        public override void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            if (forward)
            {
                Fourier.Forward(copy, FourierOptions.Default);
            }
            else
            {
                Fourier.Inverse(copy, FourierOptions.Default);
            }
        }

        public override double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            this.Initialize(true);

            Helper.ToComplex(input, out Complex[] data);
            Fourier.Forward(data, FourierOptions.AsymmetricScaling);
            Debug.WriteLine(this + " Error = " + Helper.CalculateError(data, SignalGenerator.TestArrayFFTresult()));
            var spectrum = Helper.ComputeSpectrum(data);
            Fourier.Inverse(data, FourierOptions.Default);
            backwardResult = Helper.ToReal(data);
            Helper.Scale(ref backwardResult, scale);

            return spectrum;
        }

        protected void InitializeThreads()
        {
            if (this.SingleThreaded)
            {
                Control.UseSingleThread();
            }
            else
            {
                Control.UseMultiThreading();
            }
        }

        protected void Initialize(bool managed)
        {
            this.InitializeThreads();
            
            if (managed)
            {
                Control.UseManaged();
            }
            else
            {
                Control.UseNativeMKL();
            }

            // Gives identical results
            //Control.UseManagedReference();
        }
    }
}

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
    public abstract class TestMathNetBaseReal32: TestMathNetBase
    {
        float[] input;
        float[] output;
        int length;

        public override void Initialize(double[] data)
        {
            length = data.Length;
            this.input = Helper.ConvertToFloat(data);
            this.output = new float[TestMKLReal32.GetOutPutLength(data.Length, false, true)];
            Array.Copy(input, output, input.Length);
            input = output;
            output = new float[input.Length];
        }

        public override void FFT(bool forward)
        {
            input.CopyTo(output, 0);

            if (forward)
            {
                Fourier.ForwardReal(output, length, FourierOptions.Default);
            }
            else
            {
                Fourier.InverseReal(output, length, FourierOptions.Default);
            }
        }

        
    }
}

using System;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public static class Helper
    {
        public static float[] ConvertToFloat(double[] input)
        {
            var result = new float[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = (float)input[i];
            }

            return result;
        }

        public static double[] ConvertToDouble(float[] input)
        {
            var result = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i];
            }

            return result;
        }

        public static void CopyToDouble(float[] input, double[] traget)
        {
            int length = input.Length;

            for (int i = 0; i < length; i++)
            {
                traget[i] = (double)input[i];
            }
        }


        public static double[] ComputeSpectrum(float[] fft)
        {
            int length = fft.Length / 2;

            var result = new double[length];

            for (int i = 0; i < length; i++)
            {
                float x = fft[2 * i];
                float y = fft[2 * i + 1];

                result[i] = Math.Sqrt(x * x + y * y);
            }

            return result;
        }

        public static double[] ComputeSpectrum(Complex[] fft)
        {
            var result = new double[fft.Length];

            for (int i = 0; i < fft.Length; i++)
            {
                result[i] = fft[i].Magnitude;
            }

            return result;
        }

        public static double[] ComputeSpectrum(double[] fft)
        {
            int length = fft.Length >> 1;

            var result = new double[length];

            for (int i = 0; i < length; i++)
            {
                double x = fft[2 * i];
                double y = fft[2 * i + 1];

                result[i] = Math.Sqrt(x * x + y * y);
            }

            return result;
        }

        public static void ToComplex(double[] data, out Complex[] result)
        {
            result = new Complex[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new Complex(data[i], 0.0);
            }
        }

        public static void ComplexToComplex(double[] data, out Complex[] result)
        {
            result = new Complex[data.Length>>1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Complex(
                    data[i<<1],
                    data[(i<<1)+1]);
            }
        }

        public static double[] ToComplex(Complex[] data)
        {
            var result = new double[data.Length << 1];

            for (int i = 0; i < data.Length; i++)
            {
                result[(i << 1)] = (float)data[i].Real;
                result[(i << 1) + 1] = (float)data[i].Imaginary;
            }

            return result;
        }

        public static double[] ToComplex(double[] data)
        {
            var result = new double[data.Length << 1];

            for (int i = 0; i < data.Length; i++)
            {
                result[i << 1] = data[i];
            }

            return result;
        }

        public static float[] ToComplex(float[] data)
        {
            var result = new float[data.Length << 1];

            for (int i = 0; i < data.Length; i++)
            {
                result[2 * i] = data[i];
            }

            return result;
        }

        public static float[] ToComplexF(double[] data)
        {
            float[] result = new float[data.Length << 1];

            for (int i = 0; i < data.Length; i++)
            {
                result[i * 2] = (float)data[i];
            }

            return result;
        }

        public static double[] ToReal(Complex[] data)
        {
            double[] target = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                target[i] = data[i].Real;
            }

            return target;
        }

        public static double[] ToReal(double[] data)
        {
            double[] target = new double[data.Length >> 1];

            for (int i = 0; i < target.Length; i++)
            {
                target[i] = data[i << 1];
            }

            return target;
        }

        public static float[] ToReal(float[] data)
        {
            float[] target = new float[data.Length >> 1];

            for (int i = 0; i < target.Length; i++)
            {
                target[i] = data[i << 1];
            }

            return target;
        }

        public static int GetNextPowerOf2(int n)
        {
            int count = 0;

            // First n in the below 
            // condition is for the
            // case where n is 0
            if (n > 0 && (n & (n - 1)) == 0)
                return n;

            while (n != 0)
            {
                n >>= 1;
                count += 1;
            }

            return 1 << count;
        }

        public static double[] Stretch(double[] data, int length)
        {
            if (length == data.Length)
            {
                return data;
            }

            if (length < data.Length)
            {
                Array.Resize(ref data, length);
                return data;
            }

            double[] temp = new double[length];

            int end = length - data.Length;
            // fill the first part of the inputArray with the first value of the input data (to get a length of power of 2 and stretch in input data)
            for (int i = 0; i < end; i++)
            {
                temp[i << 1] = data[0];
            }
            // write the input data to the end of the inputArray
            for (int i = end; i < length; i++)
            {
                temp[i] = data[i - end];
            }

            return temp;
        }

        public static void StretchToNextPowerOf2(ref double[] data)
        {
            int pow2 = GetNextPowerOf2(data.Length);

            if (pow2 != data.Length)
            {
                data = Helper.Stretch(data, pow2);
            }
        }

        public static void UndoStretch(ref double[] data, int unstretchedSize)
        {
            if (data.Length <= unstretchedSize)
            {
                return;
            }

            double[] temp = new double[unstretchedSize];
            Array.Copy(data, data.Length - unstretchedSize, temp, 0, unstretchedSize);
            data = temp;
        }

        public static void Scale(ref double[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] /= data.Length;
            }
        }

        public static void Scale(ref double[] data, bool scale)
        {
            if (!scale)
            {
                return;
            }

            for (int i = 0; i < data.Length; i++)
            {
                data[i] /= data.Length;
            }
        }

        public static Complex[] Multiply(Complex[] data, double factor)
        {
            Complex[] result = new Complex[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i] * factor;
            }

            return result;
        }

        public static double CalculateError(Complex[] result, Complex[] target)
        {
            int length = Math.Min(result.Length, target.Length);

            double error = 0;
            for (int i = 0; i < length; i++)
            {
                error += Complex.Subtract(result[i], target[i]).Magnitude;
            }

            return error;
        }
    }
}

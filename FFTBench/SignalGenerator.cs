
namespace FFTBench
{
    using MathNet.Numerics;
    using System;
    using System.Numerics;

    class SignalGenerator
    {
        public static double[] Sine(int size = 1024)
        {
            const int N = 64; // sampling rate

            return Generate.Sinusoidal(size, N, 1.0, 20.0);
        }

        public static double[] Sine2(int size = 1024)
        {
            const int N = 64; // sampling rate

            var a = Generate.Sinusoidal(size, N, 1.0, 20.0);
            var b = Generate.Sinusoidal(size, N, 2.0, 10.0);

            for (int i = 0; i < size; i++)
            {
                a[i] += b[i];
            }

            return a;
        }

        public static double[] Sine3(int size = 1024)
        {
            const int N = 64; // sampling rate

            var a = Generate.Sinusoidal(size, N, 1.0, 20.0);
            var b = Generate.Sinusoidal(size, N, 2.0, 10.0);
            var c = Generate.Sinusoidal(size, N, 4.0,  5.0);

            for (int i = 0; i < size; i++)
            {
                a[i] += (b[i] + c[i]);
            }

            return a;
        }

        public static double[] Square(int size = 1024)
        {
            const int N = 32;

            return Generate.Square(size, N, N, -20.0, 20.0);
        }

        public static double[] Sawtooth(int size = 1024)
        {
            const int N = 32;

            return Generate.Sawtooth(size, N, -20.0, 20.0);
        }

        public static double[] Triangle(int size = 1024)
        {
            const int N = 32;

            return Generate.Triangle(size, N, N, -20.0, 20.0);
        }

        public static double[] Bump(int size = 1024)
        {
            double[] data = new double[size];
            int start = size >> 2;
            double step = Math.PI / (size >> 1);

            for (int i = 0; i < (size >> 1); i++)
            {
                data[i+start] = Math.Sin(step * i);
            }

            return data;
        }

        public static double[] TestArray(int size)
        {
            double[] result = new double[16];
            double step = Math.PI / 16;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Sin(step * i);
            }

            return result;
        }

        public static Complex[] TestArrayFFTresult()
        {
            return new Complex[]
            {
                new Complex(10.15317038760886 , 0),
                new Complex(-3.42830608933527 , 2.220446049250313e-16),
                new Complex(-0.7128448985744655 , 3.0531133177191805e-16),
                new Complex(-0.3261824431007063 , 4.440892098500626e-16),
                new Complex(-0.19891236737965823 , 0),
                new Complex(-0.14308382743893477 , 4.440892098500626e-16),
                new Complex(-0.11558222617172476 , 3.0531133177191805e-16),
                new Complex(-0.1024276401250892 , 2.220446049250313e-16),
                new Complex(-0.09849140335716466 , 0),
                new Complex(-0.1024276401250892 , 2.220446049250313e-16),
                new Complex(-0.11558222617172476 , 3.0531133177191805e-16),
                new Complex(-0.14308382743893477 , 4.440892098500626e-16),
                new Complex(-0.19891236737965823 , 0),
                new Complex(-0.3261824431007063 , 4.440892098500626e-16),
                new Complex(-0.7128448985744655 , 3.0531133177191805e-16),
                new Complex(-3.42830608933527 , 2.220446049250313e-16)
            };
        }

        private static double[] ApplyWindow(double[] signal)
        {
            int n = signal.Length;

            var window = Window.Hamming(n);

            for (int i = 0; i < n; i++)
            {
                signal[i] *= window[i];
            }

            return signal;
        }
    }
}

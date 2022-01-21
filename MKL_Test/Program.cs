using mkl;
using System;
using System.Numerics;

namespace MKL_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This will run a short test of the MKL library.");

            var data = GenerateData(512);
            RunTest(data);

            Console.ReadKey();
        }

        static double[] GenerateData(int length)
        {
            double[] data = new double[length];

            double step = Math.PI * 2 / (length >> 1);
            for(int i = 0; i<length; i++)
            {
                data[i] = Math.Sin(step * i);
            }

            return data;
        }

        static bool RunTest(double[] input)
        {
            IntPtr desc = new IntPtr();
            int res = DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.DOUBLE,
                DFTI.COMPLEX,
                1,
                input.Length);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't initialize");
                return false;
            }

            res = DFTI.DftiSetValue(desc, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't set placement.");
                return false;
            }

            res = DFTI.DftiCommitDescriptor(desc);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't commit descriptor.");
                return false;
            }

            Complex[] data1 = mkl.Util.ToComplex(input);
            Complex[] data2 = new Complex[input.Length];

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't run fft.");
                return false;
            }

            res = DFTI.DftiComputeBackward(desc, data2, data1);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't run inverse fft.");
                return false;
            }

            DFTI.DftiFreeDescriptor(ref desc);

            Console.WriteLine("Test executed successfully.");
            return true;
        }
    }
}

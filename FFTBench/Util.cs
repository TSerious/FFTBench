using FFTBench.Benchmark;
using System;
using System.Collections.Generic;

namespace FFTBench
{
    class Util
    {
        public static List<ITest> LoadTests()
        {
            var tests = new List<ITest>
            {
                new TestAccord() { Enabled = true },
                new TestAccord() { Enabled = true, StretchInput = true },
                new TestAForge() { Enabled = true },
                new TestMathNet() { Enabled = true },
                new TestMathNet() { Enabled = true, SingleThreaded = false },
                new TestMathNet() { Enabled = true, StretchInput = true },
                new TestMathNetReal32() { Enabled = true },
                new TestMathNetReal32() { Enabled = true, SingleThreaded = false },
                new TestMathNetReal32() { Enabled = true, StretchInput = true },
                new TestMathNetPlusMkl() { Enabled = true },
                new TestMathNetPlusMkl() { Enabled = true, SingleThreaded = false },
                new TestMathNetPlusMkl() { Enabled = true, StretchInput = true },
                new TestMathNetPlusMklReal32() { Enabled = true },
                new TestMathNetPlusMklReal32() { Enabled = true, SingleThreaded = false },
                new TestMathNetPlusMklReal32() { Enabled = true, StretchInput = true },
                new TestNAudio() { Enabled = true },
                new TestDSPLib() { Enabled = true },
                new TestLomont() { Enabled = true },
                new TestLomontReal() { Enabled = true },
                new TestExocortex() { Enabled = true },
                new TestExocortexReal32() { Enabled = true },
                new TestFFTS() { Enabled = true },
                new TestFFTS() { Enabled = true, StretchInput = true },
                new TestFFTSReal() { Enabled = true },
                new TestFFTSReal() { Enabled = true, StretchInput = true },
                new TestFFTW() { Enabled = true },
                new TestFFTW() { Enabled = true, StretchInput = true },
                new TestFFTWReal() { Enabled = true },
                new TestFFTWReal() { Enabled = true, StretchInput = true },
                new TestMKLReal() { Enabled = true },
                new TestMKLReal() { Enabled = true, StretchInput = true },
                new TestMKLRealInplace() { Enabled = true },
                new TestMKLReal32() { Enabled = true },
                new TestMKLReal32() { Enabled = true, StretchInput = true },
                new TestMKLReal32() { Enabled = true, StretchInput = true, UsePackedFormat = true },
                new TestMKLReal32() { Enabled = true, StretchInput = true, UsePackedFormat = true, UseComplexStorage = true },
                new TestMKL() { Enabled = true },
                new TestMKL() { Enabled = true, StretchInput = true },
                new TestMKL32() { Enabled = true },
                new TestMKL32() { Enabled = true, StretchInput = true },
                new TestMKLNET() { Enabled = true, StretchInput = false },
                new TestMKLNET() { Enabled = true, StretchInput = true },
            };
            //tests.Add(new TestLomontReal32() { Enabled = true });
            //tests.Add(new TestKissFFT() { Enabled = true });

            return tests;
        }

        public static bool PowerOf2(int value)
        {
            return (value & (value - 1)) == 0;
        }

        public static int Log2(int value)
        {
            if (value == 0) throw new InvalidOperationException();
            if (value == 1) return 0;
            int result = 0;
            while (value > 1)
            {
                value >>= 1;
                result++;
            }
            return result;
        }

        public static int Pow(int b, int exp)
        {
            if (exp < 0) throw new InvalidOperationException();

            int result = 1;

            for (int i = 0; i < exp; i++)
            {
                result *= b;
            }

            return result;
        }
    }
}

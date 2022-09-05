﻿using FFTBench.Benchmark;
using System;
using System.Collections.Generic;
using System.IO;

namespace FFTBench
{
    class Util
    {
        public static List<ITest> LoadTests()
        {
            var tests = new List<ITest>();

            tests.Add(new TestAccord() { Enabled = false });
            tests.Add(new TestAccord() { Enabled = false, StretchInput = true });
            tests.Add(new TestAForge() { Enabled = false });
            tests.Add(new TestMathNet() { Enabled = true });
            tests.Add(new TestMathNet() { Enabled = true, StretchInput = true });
            tests.Add(new TestMathNetPlusMkl() { Enabled = true });
            tests.Add(new TestMathNetPlusMkl() { Enabled = true, StretchInput = true });
            tests.Add(new TestNAudio() { Enabled = false });
            tests.Add(new TestDSPLib() { Enabled = false });
            tests.Add(new TestLomont() { Enabled = false });
            tests.Add(new TestLomontReal() { Enabled = false });
            tests.Add(new TestExocortex() { Enabled = false });
            tests.Add(new TestExocortexReal32() { Enabled = false });
            tests.Add(new TestFFTS() { Enabled = false });
            tests.Add(new TestFFTS() { Enabled = false, StretchInput = true });
            tests.Add(new TestFFTSReal() { Enabled = false });
            tests.Add(new TestFFTSReal() { Enabled = false, StretchInput = true });
            tests.Add(new TestFFTW() { Enabled = false });
            tests.Add(new TestFFTW() { Enabled = false, StretchInput = true });
            tests.Add(new TestFFTWReal() { Enabled = false });
            tests.Add(new TestFFTWReal() { Enabled = false, StretchInput = true });
            tests.Add(new TestMKLReal() { Enabled = false });
            tests.Add(new TestMKLReal() { Enabled = false, StretchInput = true });
            tests.Add(new TestMKLRealInplace() { Enabled = false });
            tests.Add(new TestMKLReal32() { Enabled = false });
            tests.Add(new TestMKLReal32() { Enabled = false, StretchInput = true });
            tests.Add(new TestMKLReal32() { Enabled = false, StretchInput = true, UsePackedFormat = true });
            tests.Add(new TestMKLReal32() { Enabled = false, StretchInput = true, UsePackedFormat = true, UseComplexStorage = true });
            tests.Add(new TestMKL() { Enabled = false });
            tests.Add(new TestMKL() { Enabled = false, StretchInput = true });
            tests.Add(new TestMKL32() { Enabled = false });
            tests.Add(new TestMKL32() { Enabled = false, StretchInput = true });
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

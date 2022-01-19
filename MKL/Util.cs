using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace mkl
{
    public static class Util
    {
        public static Complex[] PackedRealToComplex(double[] packed)
        {
            Complex[] complex = new Complex[packed.Length];

            for (int k = 1; k <= packed.Length; k++)
            {
                if (k == 1)
                {
                    complex[k - 1] = new Complex(packed[k - 1], 0);
                }
                else if (k - 1 == packed.Length - k + 1)
                {
                    complex[k - 1] = new Complex(packed[(2 * (k - 1)) - 1], 0);
                }
                else if (k <= packed.Length / 2 + 1)
                {
                    complex[k - 1] = new Complex(
                        packed[(2 * (k - 1) + 0) - 1],
                        packed[(2 * (k - 1) + 1) - 1]
                        );
                }
                else
                {
                    complex[k - 1] = new Complex(
                        packed[(2 * (packed.Length - k + 1) + 0) - 1],
                        -1 * packed[(2 * (packed.Length - k + 1) + 1) - 1]
                        );
                }
            }

            return complex;
        }

        public static Complex[] PackedRealToComplex(float[] packed)
        {
            Complex[] complex = new Complex[packed.Length];

            for (int k = 1; k <= packed.Length; k++)
            {
                if (k == 1)
                {
                    complex[k - 1] = new Complex(packed[k - 1], 0);
                }
                else if (k - 1 == packed.Length - k + 1)
                {
                    complex[k - 1] = new Complex(packed[(2 * (k - 1)) - 1], 0);
                }
                else if (k <= packed.Length / 2 + 1)
                {
                    complex[k - 1] = new Complex(
                        packed[(2 * (k - 1) + 0) - 1],
                        packed[(2 * (k - 1) + 1) - 1]
                        );
                }
                else
                {
                    complex[k - 1] = new Complex(
                        packed[(2 * (packed.Length - k + 1) + 0) - 1],
                        -1 * packed[(2 * (packed.Length - k + 1) + 1) - 1]
                        );
                }
            }

            return complex;
        }
    }
}

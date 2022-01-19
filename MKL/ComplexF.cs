using System;

namespace MKL
{
    public struct ComplexF
    {

        // --------------SECTION: Private Data members ----------- //

        private float m_real;
        private float m_imaginary;

        // --------------SECTION: Public Properties -------------- //

        public float Real
        {
            get
            {
                return m_real;
            }
        }

        public float Imaginary
        {
            get
            {
                return m_imaginary;
            }
        }

        // --------------SECTION: Attributes -------------- //

        public static readonly ComplexF Zero = new ComplexF((float)0.0, (float)0.0);
        public static readonly ComplexF One = new ComplexF((float)1.0, (float)0.0);
        public static readonly ComplexF ImaginaryOne = new ComplexF((float)0.0, (float)1.0);

        // --------------SECTION: Constructors and factory methods -------------- //

        public ComplexF(float real, float imaginary)  /* Constructor to create a complex number with rectangular co-ordinates  */
        {
            this.m_real = real;
            this.m_imaginary = imaginary;
        }

        public static ComplexF FromPolarCoordinates(double magnitude, double phase) /* Factory method to take polar inputs and create a Complex object */
        {
            return new ComplexF((float)(magnitude * Math.Cos(phase)), (float)(magnitude * Math.Sin(phase)));
        }

        public static ComplexF Negate(ComplexF value)
        {
            return -value;
        }

        public static ComplexF Add(ComplexF left, ComplexF right)
        {
            return left + right;
        }

        public static ComplexF Subtract(ComplexF left, ComplexF right)
        {
            return left - right;
        }

        public static ComplexF Multiply(ComplexF left, ComplexF right)
        {
            return left * right;
        }

        public static ComplexF Divide(ComplexF dividend, ComplexF divisor)
        {
            return dividend / divisor;
        }

        // --------------SECTION: Arithmetic Operator(unary) Overloading -------------- //
        public static ComplexF operator -(ComplexF value)  /* Unary negation of a complex number */
        {

            return (new ComplexF((-value.m_real), (-value.m_imaginary)));
        }

        // --------------SECTION: Arithmetic Operator(binary) Overloading -------------- //       
        public static ComplexF operator +(ComplexF left, ComplexF right)
        {
            return (new ComplexF((left.m_real + right.m_real), (left.m_imaginary + right.m_imaginary)));

        }

        public static ComplexF operator -(ComplexF left, ComplexF right)
        {
            return (new ComplexF((left.m_real - right.m_real), (left.m_imaginary - right.m_imaginary)));
        }

        public static ComplexF operator *(ComplexF left, ComplexF right)
        {
            // Multiplication:  (a + bi)(c + di) = (ac -bd) + (bc + ad)i
            float result_Realpart = (left.m_real * right.m_real) - (left.m_imaginary * right.m_imaginary);
            float result_Imaginarypart = (left.m_imaginary * right.m_real) + (left.m_real * right.m_imaginary);
            return (new ComplexF(result_Realpart, result_Imaginarypart));
        }

        public static ComplexF operator /(ComplexF left, ComplexF right)
        {
            // Division : Smith's formula.
            float a = left.m_real;
            float b = left.m_imaginary;
            float c = right.m_real;
            float d = right.m_imaginary;

            if (Math.Abs(d) < Math.Abs(c))
            {
                double doc = d / c;
                return new ComplexF((float)((a + b * doc) / (c + d * doc)), (float)((b - a * doc) / (c + d * doc)));
            }
            else
            {
                double cod = c / d;
                return new ComplexF((float)((b + a * cod) / (d + c * cod)), (float)((-a + b * cod) / (d + c * cod)));
            }
        }
    }
}
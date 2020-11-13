using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Math;

namespace DataProtection.Engine.Models
{
    public class GeneralizedBigIntegerEvklid
    {
        public EvklidBigIntegerModel V { get; set; }
        public EvklidBigIntegerModel T { get; set; }
        public EvklidBigIntegerModel U { get; set; }
    }

    public class EvklidBigIntegerModel
    {
        public BigInteger A { get; set; }
        public BigInteger B { get; set; }
        public BigInteger R { get; set; }

        public EvklidBigIntegerModel()
        {
        }

        public EvklidBigIntegerModel(BigInteger a, BigInteger b, BigInteger r)
        {
            A = a;
            B = b;
            R = r;
        }
    }
}

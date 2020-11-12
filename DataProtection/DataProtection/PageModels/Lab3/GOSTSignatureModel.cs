using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab3
{
    public class GOSTSignatureModel
    {
        public BigInteger p { get; set; }
        public BigInteger q { get; set; }
        public int b { get; set; }
        public BigInteger a { get; set; }
        public BigInteger x { get; set; }
        public BigInteger y { get; set; }
        public BigInteger h { get; set; }
        public BigInteger h_revers { get; set; }
        public BigInteger k { get; set; }
        public BigInteger r { get; set; }
        public BigInteger s { get; set; }
        public BigInteger h_check { get; set; }
        public BigInteger u1 { get; set; }
        public BigInteger u2 { get; set; }
        public BigInteger v { get; set; }

    }
}

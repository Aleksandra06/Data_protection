using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab3
{
    public class GOSTSignatureModel
    {
        public System.Numerics.BigInteger p { get; set; }
        public System.Numerics.BigInteger q { get; set; }
        public int b { get; set; }
        public System.Numerics.BigInteger a { get; set; }
        public System.Numerics.BigInteger x { get; set; }
        public System.Numerics.BigInteger y { get; set; }
        public System.Numerics.BigInteger h { get; set; }
        public System.Numerics.BigInteger h_revers { get; set; }
        public System.Numerics.BigInteger k { get; set; }
        public System.Numerics.BigInteger r { get; set; }
        public System.Numerics.BigInteger s { get; set; }
        public System.Numerics.BigInteger h_check { get; set; }
        public System.Numerics.BigInteger u1 { get; set; }
        public System.Numerics.BigInteger u2 { get; set; }
        public System.Numerics.BigInteger v { get; set; }

    }
}

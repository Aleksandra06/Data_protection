using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Org.BouncyCastle.Math;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab3
{
    public class ElGamalSignatureModel
    {
        public BigInteger p { get; set; }
        public BigInteger q { get; set; }
        public BigInteger x { get; set; }
        public BigInteger y { get; set; }
        public BigInteger g { get; set; }
        public BigInteger r { get; set; }
        public BigInteger k { get; set; }
        public BigInteger k_revers { get; set; }
        public BigInteger u { get; set; }
        public BigInteger s { get; set; }
        public BigInteger p_prev { get; set; }
        public BigInteger h { get; set; }

    }
}

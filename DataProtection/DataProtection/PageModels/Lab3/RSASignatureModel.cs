using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace DataProtection.PageModels.Lab3
{
    public class RSASignatureModel
    {
        public BigInteger p { get; set; }
        public BigInteger q { get; set; }
        public BigInteger n { get; set; }
        public BigInteger phi { get; set; }
        public BigInteger d { get; set; }
        public BigInteger c { get; set; }
        public BigInteger y { get; set; }
        public BigInteger s { get; set; }
        public BigInteger w { get; set; }
    }
}

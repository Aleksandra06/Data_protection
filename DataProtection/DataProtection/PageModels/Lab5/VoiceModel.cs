using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace DataProtection.PageModels.Lab5
{
    public class VoiceModel
    {
        public int bitLength { get; set; }
        public BigInteger n { get; set; }
        public BigInteger h { get; set; }
        public BigInteger _h { get; set; }
        public BigInteger r { get; set; }
        public BigInteger s { get; set; }
        public BigInteger _s { get; set; }
        public BigInteger[] ticket { get; set; }
        public string name { get; set; }

    }
}

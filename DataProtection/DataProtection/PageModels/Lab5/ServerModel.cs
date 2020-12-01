using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace DataProtection.PageModels.Lab5
{
    public class ServerModel
    {
        public int bitLength { get; set; }
        public BigInteger p { get; set; }
        public BigInteger q { get; set; }
        public BigInteger n { get; set; }
        public BigInteger f { get; set; }
        public BigInteger c { get; set; }
        public BigInteger d { get; set; }
    }
}

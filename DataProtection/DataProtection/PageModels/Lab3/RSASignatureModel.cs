using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab3
{
    public class RSASignatureModel
    {
        public long p { get; set; }
        public long q { get; set; }
        public long n { get; set; }
        public long phi { get; set; }
        public long d { get; set; }
        public long c { get; set; }
        public long y { get; set; }
        public long s { get; set; }
        public long w { get; set; }
    }
}

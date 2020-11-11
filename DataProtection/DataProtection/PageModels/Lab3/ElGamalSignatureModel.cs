using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab3
{
    public class ElGamalSignatureModel
    {
        public long p { get; set; }
        public long q { get; set; }
        public long x { get; set; }
        public long y { get; set; }
        public long g { get; set; }
        public long r { get; set; }
        public long k { get; set; }
        public long k_revers { get; set; }
        public long u { get; set; }
        public long s { get; set; }
        public long p_prev { get; set; }
        public long h { get; set; }

    }
}

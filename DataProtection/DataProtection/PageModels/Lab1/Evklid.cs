using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab1
{
    public class Evklid
    {
        public long A { get; set; }
        public long B { get; set; }
        public long R { get; set; }

        public Evklid()
        {
        }
        public Evklid(long a, long b, long r)
        {
            A = a;
            B = b;
            R = r;
        }
        public override string ToString()
        {
            return A + "\n" + B + "\n" + R;
        }
    }
}

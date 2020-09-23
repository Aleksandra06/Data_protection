using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection.Engine.Models
{
    public class GeneralizedEvklid
    {
        public EvklidModel V { get; set; }
        public EvklidModel T { get; set; }
        public EvklidModel U { get; set; }
    }

    public class EvklidModel
    {
        public long A { get; set; }
        public long B { get; set; }
        public long R { get; set; }

        public EvklidModel()
        {
        }

        public EvklidModel(long a, long b, long r)
        {
            A = a;
            B = b;
            R = r;
        }
    }
}

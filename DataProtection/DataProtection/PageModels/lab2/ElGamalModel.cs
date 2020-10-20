using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.PageModels.lab2
{
    public struct Message
    {
        public long r { get; set; }
        public long e { get; set; }
    };
    public class ElGamalModel
    {
        public long p { get; set; }
        public long q { get; set; }
        public long g { get; set; }
        public long Da { get; set; }
        public long Ca { get; set; }
        public Message[] message { get; set; }

    }
}

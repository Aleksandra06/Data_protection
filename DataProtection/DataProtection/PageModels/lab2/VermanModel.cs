using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.PageModels.lab2
{
    public struct VermanMessage
    {
        public byte k;
        public byte e;
        public byte m;
    }
    public class VermanModel
    {
        public VermanMessage[] Message { get; set; }
    }
}

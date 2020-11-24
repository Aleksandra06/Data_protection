using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace DataProtection.PageModels.Lab4
{
    public class PockerModel
    {
        [Range(2, 25, ErrorMessage = "Слишком мало или слишком много игроков")]
        public int CountPeople { get; set; }
        public Dictionary<int, string> cards { get; set; }
        public BigInteger p { get; set; }
        public BigInteger p_prev { get; set; }
        public List<BigInteger> deck { get; set; }
        public LinkedList<Player> players { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.PageModels.Lab4
{
    public class PockerModel
    {
        [Range(2, 25, ErrorMessage = "Слишком мало или слишком много игроков")]
        public int CountPeople { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using DataProtection.Pages.Lab3;
using Microsoft.AspNetCore.Components;

namespace DataProtection.Pages.Lab3
{
    public partial class GOSTSignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        public GOSTSignatureModel gost = new GOSTSignatureModel();
        Random random = new Random();


        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {
            gost.p = generateP();
        }

        private System.Numerics.BigInteger generateP()
        {
            System.Numerics.BigInteger currentQ;
            //int b = random.Next(700, 800);
            //do {
                
            //} while ();
            throw new NotImplementedException();
        }
    }
}

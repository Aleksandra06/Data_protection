using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using DataProtection.Pages.Lab3;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Math;

namespace DataProtection.Pages.Lab3
{
    public partial class GOSTSignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        public GOSTSignatureModel gost = new GOSTSignatureModel();
        Random random = new Random();
        //private const int b = Math 767;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {
            SHA256 sha256 = SHA256.Create();

        }
    }
}

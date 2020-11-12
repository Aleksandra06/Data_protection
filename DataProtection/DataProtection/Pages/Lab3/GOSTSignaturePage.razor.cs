using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {

        }
    }
}

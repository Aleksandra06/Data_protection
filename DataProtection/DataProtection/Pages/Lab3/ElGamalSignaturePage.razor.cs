using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using Microsoft.AspNetCore.Components;

namespace DataProtection.Pages.Lab3
{
    public partial class ElGamalSignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        ElGamalSignatureModel elGamal = new ElGamalSignatureModel();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {

        }
    }
}

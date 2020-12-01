using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace DataProtection.Pages.Lab5
{
    public class VoiceViewModel : ComponentBase
    {
        protected int Voise { get; set; } = 1;
        protected string Text = "";
        protected void Vote()
        {
            if (Voise == 0)
            {
                Text = "Введите свой голос!";
                return;
            }
        }
    }
}

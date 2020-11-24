using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProtection.PageModels.Lab4;
using Microsoft.AspNetCore.Components;

namespace DataProtection.Pages.Lab4
{
    public class PokerViewModel : ComponentBase
    {
        protected PockerModel Model { get; set; } = new PockerModel();

    }
}

﻿
using Microsoft.JSInterop;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Home
    {
        protected override async Task OnInitializedAsync()
        {
            _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Home");
        }






    }
}


using Microsoft.JSInterop;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Home
    {
        protected override void OnInitialized()
        {
            _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Home");
        }






    }
}

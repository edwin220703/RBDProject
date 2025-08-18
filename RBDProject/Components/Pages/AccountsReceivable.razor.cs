using ConsoleApp1;
using Microsoft.JSInterop;
using RBDProject.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace RBDProject.Components.Pages
{
    partial class AccountsReceivable
    {
        private List<RbdCuentasPorCobrar> _listCuentasPorCobrar { get; set; } = new List<RbdCuentasPorCobrar>();

        private IList<RbdCuentasPorCobrar> _selectCuentaPorCobrar { get; set; } = new List<RbdCuentasPorCobrar>();

        //API
        private string _httpServidor = "Servidor";
        private string _httpApi = "api/CuentasPorCobrar";
        //private string _httpClientes = "api/RBDClientes";


        protected override async Task OnInitializedAsync()
        {
            await GetCxc();
            var a =_jSRuntime.InvokeVoidAsync("CambiarTitle", "Cuentas Por Cobrar");
            StateHasChanged();
        }

        public async Task GetCxc()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCuentasPorCobrar>>(result);

                        if(result2 != null)
                        {
                            _listCuentasPorCobrar = result2;
                        }
                    }
                }
            }
        }


        public void ImprimirReporte(int value)
        {
            Reporte reporte = new Reporte();

            switch (value)
            {
                case 1:
                    {
                        reporte.CuentasPorCobrar(_listCuentasPorCobrar);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectCuentaPorCobrar.Count == 1)
                        {
                            reporte.CuentasPorCobrar(new List<RbdCuentasPorCobrar> { _selectCuentaPorCobrar[0] });
                        }

                        if (_selectCuentaPorCobrar.Count > 1)
                        {
                            reporte.CuentasPorCobrar(_selectCuentaPorCobrar.ToList());
                        }
                    }
                    ; break;
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Controllers;
using RBDProject.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Position
    {
        List<RbdCargo> cargos { get; set; } = new List<RbdCargo>();
        IList<RbdCargo> _selectedCargos { get; set; } = new List<RbdCargo>();
        List<RbdEstado> _listEstados { get; set; } = new List<RbdEstado>();

        //MODAL
        private RbdCargo model { get; set; } = new RbdCargo();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDCargos";
        private string httpApiEstado = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
            GetStatus();
             Get();

        }

        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdCargo rbdCargo, string e)
        {
            model = rbdCargo;
            utilitymodal = e;
        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCargo>>(result);

                        if (result2 == null)
                            cargos = new List<RbdCargo>();
                        else
                            cargos = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetStatus()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpApiEstado))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 == null)
                            _listEstados = new List<RbdEstado>();
                        else
                            _listEstados = result2;
                    }
                }
            }
        }

        public async Task Add(RbdCargo cargo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(cargo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadidco", $"Se agrego {cargo.NomCar} correctemente");
                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdCargo cargo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{cargo.CodCar}", JsonSerializer.Serialize(cargo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {cargo.NomCar} correctemente");
                        await Get();
                    }
                }
            }
        }

        public async Task Remove(RbdCargo cargo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{cargo.CodCar}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {cargo.NomCar} correctemente");
                        await Get();
                    }
                }
            }
        }
    }
}


using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Controllers;
using RBDProject.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Client
    {
        List<RbdCliente> clientes { get; set; } = null;
        IList<RbdCliente> _selectedCliente { get; set; } = new List<RbdCliente>();
        List<RbdCiudade> _listCiudades { get; set; } = new List<RbdCiudade>();
        List<RbdCalle> _listCalle { get; set; } = new List<RbdCalle>();

        //MODAL
        private RbdCliente model { get; set; } = new RbdCliente();
        private string utilitymodal { get; set; } = string.Empty;

        //Opcion buscar
        private string textobuscar { get; set; } = string.Empty;
        private int valor { get; set; } = 1;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDClientes";
        private string httpApiCiudades = "api/RBDCiudades";
        private string httpApiCalles = "api/RBDCalles";

        protected override async Task OnInitializedAsync()
        {
            GetOthers();
            Get();
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });


        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdCliente cliente, string e)
        {
            model = cliente;
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

                        var result2 = JsonSerializer.Deserialize<List<RbdCliente>>(result);

                        if (result2 == null)
                            clientes = new List<RbdCliente>();
                        else
                            clientes = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetOthers()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpApiCiudades))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCiudade>>(result);

                        if (result2 == null)
                            _listCiudades = new List<RbdCiudade>();
                        else
                            _listCiudades = result2;
                    }
                }

                using (var content = await client.GetAsync(httpApiCalles))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCalle>>(result);

                        if (result2 == null)
                            _listCalle = new List<RbdCalle>();
                        else
                            _listCalle = result2;

                    }
                }
            }

            StateHasChanged();
        }

        public async Task Add(RbdCliente cliente)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(cliente)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {cliente.NomCli} correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"Error al añadir {cliente.NomCli} correctamente");
                    }
                }
            }
        }

        public async Task Update(RbdCliente cliente)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{cliente.CodCli}", JsonSerializer.Serialize(cliente)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {cliente.NomCli} correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"Error al actualizar {cliente.NomCli} correctamente");
                    }
                }
            }
        }

        public async Task Remove(RbdCliente cliente)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{cliente.CodCli}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminado", $"Se elimino {cliente.NomCli} correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"Error al eliminar {cliente.NomCli} correctamente");
                    }
                }
            }
        }
    }
}

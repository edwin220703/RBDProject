
using ClosedXML.Excel;
using ConsoleApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Models;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RBDProject.Components.Pages
{
    partial class Article
    {
        private string utilitymodal { get; set; } = string.Empty;
        private List<RbdArticulo> _lisarticles { get; set; } = new List<RbdArticulo>();
        private IList<RbdArticulo> _selectedArticle { get; set; } = new List<RbdArticulo>();

        private RbdArticulo model { get; set; } = new RbdArticulo();
        private List<RbdListaDePrecio> _modalListPrecio { get; set; } = new List<RbdListaDePrecio>();

        private List<RbdEstado> _listEstados { get; set; } = new List<RbdEstado>();
        private List<RbdGrupo> _listGrupos { get; set; } = new List<RbdGrupo>();
        //API
        private readonly string httpServidor = "Servidor";
        private readonly string httpApi = "api/RBDArticulos";
        private readonly string httpEstados = "api/RBDEstados";
        private readonly string httpGrupos = "api/RBDGrupos";

        protected override async Task OnInitializedAsync()
        {
            await GetByOthers();
            await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Articulo");
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
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

                        var result2 = JsonSerializer.Deserialize<List<RbdArticulo>>(result);

                        if (result2 == null)
                        {
                            _lisarticles = new List<RbdArticulo>();
                            ShowNotification(NotificationSeverity.Info, "Contenido", "No existen articulos en la base de datos");
                        }
                        else
                        {
                            _lisarticles = result2;
                        }

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task GetByOthers()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpEstados))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 == null)
                        {
                            _listEstados = new List<RbdEstado>();
                        }
                        else
                        {
                            _listEstados = result2.Take(3).ToList();
                        }
                    }
                }

                using (var content = await client.GetAsync(httpGrupos))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdGrupo>>(result);

                        if (result2 == null)
                            _listGrupos = new List<RbdGrupo>();
                        else
                            _listGrupos = result2;

                    }
                }

                StateHasChanged();
            }
        }


        public async Task Add(RbdArticulo articulo, List<RbdListaDePrecio> precio)
        {
            int id2;
            articulo.CodArt = 0;

            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var id = await content.Content.ReadAsStringAsync();

                        id2 = JsonSerializer.Deserialize<int>(id);

                        if (precio != null && precio.Count != 0)
                        {
                            foreach (var item in precio)
                            {
                                item.CodArt = id2;

                                await client.PostAsJsonAsync(httpApi + "/ListaPrecios", JsonSerializer.Serialize(item));

                            }
                        }

                        ShowNotification(NotificationSeverity.Success, "Articulo", $"Se añadio correctamente el articulo {articulo.NomArt}");
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"El producto no fue ingresado correctamente");
                    }
                }

                await Get();
            }
        }

        public async Task Update(RbdArticulo articulo, List<RbdListaDePrecio> precios)
        {
            foreach (var p in precios)
            {
                p.CodArt = articulo.CodArt;
            }

            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{articulo.CodArt}", JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"{articulo.NomArt} se actualizo correctamente");
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"El producto no fue actualizado correctamente");
                    }
                }

                using (var content = await client.PutAsJsonAsync(httpApi + $"/ListaPrecios/{articulo.CodArt}", JsonSerializer.Serialize(precios)))
                {
                    if (content.IsSuccessStatusCode)
                    {

                    }
                }

                await Get();
            }
        }

        public async Task Remove(RbdArticulo articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{articulo.CodArt}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"{articulo.NomArt} se elimino correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"El producto no fue eliminado correctamente");
                    }
                }
            }
        }

        public void SendTypeModal(RbdArticulo rbdArticulo, string e)
        {
            if (rbdArticulo.IdArt == null)
            {
                string? pre = _configure.GetValue<string>("Configuracion:Codigo-Articulo");
                string result;

                if (_lisarticles.Count != 0)
                {
                    result = $"{pre}{_lisarticles.Max(a => a.CodArt)}";

                }
                else
                {
                    result = pre + "1";
                }

                rbdArticulo.IdArt = result;
            }

            model = rbdArticulo;
            utilitymodal = e;

            if (rbdArticulo.RbdListaDePrecios != null)
            {
                _modalListPrecio = rbdArticulo.RbdListaDePrecios.ToList();
            }
            else
            {
                _modalListPrecio = new List<RbdListaDePrecio>();
            }
        }

        public void ImprimirReporte(int value)
        {
            Reporte reporte = new Reporte();

            switch (value)
            {
                case 1:
                    {
                        reporte.Articulos(_lisarticles);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedArticle.Count == 1)
                        {
                            reporte.Articulos(new List<RbdArticulo> { _selectedArticle[0] });
                        }

                        if (_selectedArticle.Count > 1)
                        {
                            reporte.Articulos(_selectedArticle.ToList());
                        }
                    }
                    ; break;
            }
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Articulos");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("Descripcion"),
                new DataColumn("Existencias"),
                new DataColumn("Fecha de Creacion"),
                new DataColumn("Grupo"),
                new DataColumn("Estado"),
                new DataColumn("Precio 1"),
                new DataColumn("Precio 2"),
                new DataColumn("Precio 3"),
                new DataColumn("Precio 4"),
            });

            switch (value)
            {
                case 1:
                    {


                        foreach (var a in _lisarticles)
                        {
                            //ASIGNANDO PRECIOS
                            double[] e = new double[4] { 0, 0, 0, 0 };
                            int i = 0;

                            if (a.RbdListaDePrecios != null)
                                foreach (var p in a.RbdListaDePrecios)
                                {
                                    e[i] = p.Precio;
                                    i++;
                                }
                            //TERMINANDO DE ASIGNAR

                            dt.Rows.Add(a.CodGrup, a.NomArt, a.DesArt, a.ExistArt, a.FecArt, a.CodGrupNavigation.NomGrup, a.CodEstNavigation.NomEst, e[0], e[1], e[2], e[3]);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedArticle.ToList())
                        {
                            //ASIGNANDO PRECIOS
                            double[] e = new double[4] { 0, 0, 0, 0 };
                            int i = 0;

                            if (a.RbdListaDePrecios != null)
                                foreach (var p in a.RbdListaDePrecios)
                                {
                                    e[i] = p.Precio;
                                    i++;
                                }
                            //TERMINANDO DE ASIGNAR

                            dt.Rows.Add(a.CodGrup, a.NomArt, a.DesArt, a.ExistArt, a.FecArt, a.CodGrupNavigation.NomGrup, a.CodEstNavigation.NomEst, e[0], e[1], e[2], e[3]);
                        }
                    }
                    ; break;
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                var mbyte = new byte[0];

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    mbyte = stream.ToArray();
                    //(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Nombre");
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Articulos - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}

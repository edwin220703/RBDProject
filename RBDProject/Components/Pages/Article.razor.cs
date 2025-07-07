
using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Article
    {
        private List<RbdArticulo> _lisarticles { get; set; } = null;
        private IList<RbdArticulo> _selectedArticle { get; set; } = new List<RbdArticulo>();
        private RbdArticulo model { get; set; } = new RbdArticulo();
        private string utilitymodal { get; set; } = string.Empty;
        private List<RbdEstado> _listEstados { get; set; } = null;
        private List<RbdGrupo> _listGrupos { get; set; } = null;

        //API
        private readonly string httpServidor = "Servidor";
        private readonly string httpApi = "api/RBDArticulos";
        private readonly string httpEstados = "api/RBDEstados";
        private readonly string httpGrupos = "api/RBDGrupos";

        protected override async Task OnInitializedAsync()
        {
            Get();
            GetByOthers();
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });


        public async Task Search(int id, string value)
        {

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
                            _lisarticles = new List<RbdArticulo>();
                        else
                            _lisarticles = result2;

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
                            _listEstados = new List<RbdEstado>();
                        else
                            _listEstados = result2;
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
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var id = await content.Content.ReadAsStringAsync();

                        var id2 = JsonSerializer.Deserialize<int>(id);

                        foreach (var item in precio)
                        {
                            item.CodArt = id2;

                            await client.PostAsJsonAsync(httpApi + "/ListaPrecios", JsonSerializer.Serialize(item));

                        }

                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdArticulo articulo, List<RbdListaDePrecio> precios)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{articulo.CodArt}", JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        await client.PutAsJsonAsync(httpApi + $"/ListaPrecio/{articulo.CodArt}", JsonSerializer.Serialize(precios));
                        Get();
                    }
                }
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
                        Get();
                    }
                }
            }
        }

        public void SendTypeModal(RbdArticulo rbdArticulo, string e)
        {
            model = rbdArticulo;
            utilitymodal = e;
        }
    }
}


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
            GetByEstados();
            GetByGrupos();
        }

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

        public async Task GetByEstados()
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

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task GetByGrupos()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
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

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Add(RbdArticulo articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                    }
                }
            }
        }

        public async Task Update(RbdArticulo articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{articulo.CodArt}", JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
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

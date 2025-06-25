
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Article
    {
        private List<RbdArticulo> _lisarticles { get; set; } = null;
        private RbdArticulo model { get; set; } = new RbdArticulo();
        private string utilitymodal {  get; set; } = string.Empty;

        //API
        private readonly string httpServidor = "Servidor";
        private readonly string httpApi = "api/RBDArticulos";

        protected override async Task OnInitializedAsync()
        {
            Get();
        }

        public async Task Search(int id, string value)
        {

        }

        public async Task Get()
        {
            using(HttpClient client = _http.CreateClient(httpServidor))
            {
                using(var content = await client.GetAsync(httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdArticulo>>(result);

                        if(result2 == null)
                            _lisarticles = new List<RbdArticulo>();
                        else
                            _lisarticles = result2;

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
                        StateHasChanged();
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
                        StateHasChanged();
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
                        StateHasChanged();
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

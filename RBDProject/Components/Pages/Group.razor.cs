using RBDProject.Controllers;
using RBDProject.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Group
    {
        //LISTAS PRINCIPALES
        List<RbdGrupo> grupos { get; set; } = null;
        List<RbdEstado> estados { get; set; } = new List<RbdEstado>();

        //MODAL
        private RbdGrupo model { get; set; } = new RbdGrupo();
        private string utilitymodal { get; set; } = string.Empty;

        //Opcion buscar
        private string textobuscar { get; set; } = string.Empty;
        private int valor { get; set; } = 1;

        //URL PETICIONES
        private string httpServer = "Servidor";
        private string httpApi = "api/RBDGrupos";
        private string httpEstados = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
            SendStatusModal();
            Get();
        }

        public void SendTypeModal(RbdGrupo rbdGrupo, string e)
        {
            model = rbdGrupo;
            utilitymodal = e;
        }

        public async Task SendStatusModal()
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                //TRAYENDO Estados
                using (var content = await client.GetAsync(httpEstados))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 == null)
                            estados = new List<RbdEstado>();
                        else
                            estados = result2;

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                //TRAYENDO GRUPOS
                using (var content = await client.GetAsync(httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdGrupo>>(result);

                        if (result2 == null)
                            grupos = new List<RbdGrupo>();
                        else
                            grupos = result2;

                        StateHasChanged();
                    }
                }
            }
        }


        public async Task Add(RbdGrupo grupo)
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(grupo));

                if (content.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Update(RbdGrupo grupo)
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                var content = await client.PutAsJsonAsync(httpApi + $"/{grupo.CodGrup}", JsonSerializer.Serialize(grupo));

                if (content.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Remove(RbdGrupo grupo)
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                var content = await client.DeleteAsync(httpApi + $"/{grupo.CodGrup}");

                if (content.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Search(int a, string value)
        {

        }
    }
}

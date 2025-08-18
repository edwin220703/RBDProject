using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.AuthFolder
{
    public class UserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RbdEmpleado _empleado;

        public UserServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _empleado = new RbdEmpleado();
        }

        public async Task<RbdEmpleado> GetUser(string name, string password)
        {
            using (var client = _httpClientFactory.CreateClient("Servidor"))
            {
                using (var content = await client.GetAsync($"api/RBDEmpleados/{name}/{password}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var em = JsonSerializer.Deserialize<RbdEmpleado>(result);

                        if (em != null)
                            return em;
                        else
                            return new RbdEmpleado();
                    }
                    else
                    {
                        return new RbdEmpleado();
                    }
                }
            }
        }
    }
}

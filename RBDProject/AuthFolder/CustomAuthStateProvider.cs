using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RBDProject.Models;

namespace RBDProject.AuthFolder
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly UserServices _usuario;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider(UserServices usuaro, IHttpContextAccessor httpContextAccessor)
        {
            _usuario = usuaro;
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            try
            {
                if (_httpContextAccessor.HttpContext!.Request.Cookies.ContainsKey(BlazorConstants.AuthCookieName))
                {
                    var token = _httpContextAccessor.HttpContext.Request.Cookies[BlazorConstants.AuthCookieName];
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                    var claims = new List<Claim>();
                    foreach (var claim in jsonToken!.Claims)
                    {
                        claims.Add(new Claim(claim.Type, claim.Value)); 
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, "jwt");
                    var user = new ClaimsPrincipal(claimsIdentity);
                    return Task.FromResult(new AuthenticationState(user));
                }
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _usuario.GetUser(username, password);

            string rol = user.CodCarNavigation == null ? "user" : user.CodCarNavigation.NomCar;

            if (user != null && user.NomClav == password)
            {
                var claimsIdentity = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.Name, user.NomUs),
                        new Claim(ClaimTypes.Role, rol)
                    ]);

                var token = new JwtSecurityToken(
                    issuer: "https://test-issuer.com",
                    audience: Guid.NewGuid().ToString(),
                    claims: claimsIdentity.Claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                    SecurityAlgorithms.HmacSha256)
                    );


                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenstring;
            }

            throw new Exception("Usuario o contraseña incorrecto");
        }
    }
}

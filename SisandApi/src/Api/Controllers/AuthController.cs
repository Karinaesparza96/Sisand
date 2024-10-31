using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Api.Configurations;
using Api.Dtos;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("conta/")]
    public class AuthController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        INotificador notificador) : BaseController(notificador)
    {
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, registerUser.Password!);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                var token = GerarJwt();

                return CustomResponse(HttpStatusCode.OK, new { token = token });
            }

            Notificar("Falha ao registrar o usuário.");
            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await signInManager.PasswordSignInAsync(loginUser.Email!, loginUser.Password!, false, true);

            if (result.Succeeded)
            {
                var token = GerarJwt();
                return CustomResponse(HttpStatusCode.OK, new { token = token });
            }

            Notificar("Usuário ou senha incorretos");
            return CustomResponse();
        }

        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Value.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.Value.Emissor,
                Audience = jwtSettings.Value.Audiencia,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.Value.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encondedToken = tokenHandler.WriteToken(token);
            return encondedToken;
        }
    }
}

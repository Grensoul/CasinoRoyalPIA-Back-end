using Casino_Royal_PIA_Back_end.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasController(UserManager<IdentityUser> userManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAuthDTO>> Register(CredencialesUserDTO credencialesUserDTO)
        {
            var user = new IdentityUser { UserName = credencialesUserDTO.Email,
                Email = credencialesUserDTO.Email };
            var result = await userManager.CreateAsync(user, credencialesUserDTO.Password);

            if (result.Succeeded)
            {
                return await CreateToken(credencialesUserDTO);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")] //checando login pq no funciona
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAuthDTO>> Login(CredencialesUserDTO credencialesUserDTO)
        {
            var result = await signInManager.PasswordSignInAsync(credencialesUserDTO.Email,
                credencialesUserDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await CreateToken(credencialesUserDTO);
            }
            else
            {
                return BadRequest("No se ha podido logear");
            }
        }

        private async Task<RespuestaAuthDTO> CreateToken(CredencialesUserDTO credencialesUserDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUserDTO.Email),
            };

            var user = await userManager.FindByEmailAsync(credencialesUserDTO.Email);

            var claimsDb = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDb);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(7);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAuthDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

        [HttpPost("DarAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GiveAdmin(EditarAdminDTO editarAdminDTO)
        {
            var user = await userManager.FindByEmailAsync(editarAdminDTO.Email);
            await userManager.AddClaimAsync(user, new Claim("admin", "1"));
            return NoContent();
        }

        [HttpPost("RevocarAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> RemoveAdmin(EditarAdminDTO editarAdminDTO)
        {
            var user = await userManager.FindByEmailAsync(editarAdminDTO.Email);
            await userManager.RemoveClaimAsync(user, new Claim("admin", "1"));
            return NoContent();
        }
    }
}

using Blazor_eCommerce_Project.API.Helper;
using Blazor_eCommerce_Project.Common;
using Blazor_eCommerce_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _singinManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AccountController(SignInManager<IdentityUser> singinManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,ITokenService tokenService)
        {
            _singinManager = singinManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SingUp([FromBody] UserRequestDTO userRequesDto)
        {
            if (userRequesDto is null || !ModelState.IsValid)
                return BadRequest();

            var user = new IdentityUser
            {
                UserName = userRequesDto.Email,
                Email = userRequesDto.Email,
                PhoneNumber = userRequesDto.PhoneNo,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, userRequesDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(m => m.Description);
                return BadRequest(new Result<IEnumerable<string>>(false, ResultConstant.IdNotNull, errors));

            }


            //var roleResult = await _userManager.AddToRoleAsync(user, ResultConstant.Role_Customer);
            //if (!roleResult.Succeeded)
            //{
            //    var errors = result.Errors.Select(m => m.Description);
            //    return BadRequest(new Result<IEnumerable<string>>(false, ResultConstant.IdNotNull, errors));

            //}

            return StatusCode(201);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SingIn([FromBody] SingInDTO singInDTO)
        {
            var result = await _singinManager.PasswordSignInAsync(singInDTO.UserName, singInDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(singInDTO.UserName);
                if (user is null)
                
                    return Unauthorized(new Result<IActionResult>(false, ResultConstant.InvalidAuthentication));

                var returnData = new UserDTO
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    Email=user.Email,
                    PhoneNo=user.PhoneNumber,
                    Token=_tokenService.CreateToken(user)
                };
                return Ok(new Result<UserDTO>(true,ResultConstant.TokenResponseMessage,returnData));
            }
            else
            {
                return Unauthorized(new Result<IActionResult>(false,ResultConstant.InvalidAuthentication));
            }
        }

    }
}

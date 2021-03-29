using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TechDemo.Core.Infrastructure.Services;

namespace TechDemoReact.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateUserVm model)
        {
            var id = await _userService.CreateAsync(model);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateUserVm model)
        {
            await _userService.UpdateAsync(model);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

    }
}
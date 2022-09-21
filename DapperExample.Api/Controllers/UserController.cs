using DapperExample.DataAccess.Repositories;
using DapperExample.Models.DTOs;
using DapperExample.Models.Entites;
using Microsoft.AspNetCore.Mvc;


namespace DapperExample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion

        #region Ctor

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Method [s]

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            await _userRepository.Post(signUpDto);

            return Ok();
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateDto updateDto)
        {
            await _userRepository.Update(id, updateDto);

            return Ok();
        }


        [HttpGet]
        [Route("GetUserRoles/{id:int}")]
        public async Task<IActionResult> GetUserRoles(int id)
        {
            return Ok(await _userRepository.GetUserAndUserRoles(id));
        }

        #endregion

    }
}

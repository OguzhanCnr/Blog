using Blog.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository userRepository;
        public UsersController()
        {
            userRepository = new UsersRepository();
        }

        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Users Get(int id)
        {
            return userRepository.GetById(id);
        }

        [HttpPost]
        public void Post(Users user)
        {
            if (ModelState.IsValid)
                userRepository.Add(user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Users user)
        {
            user.UserId = id;
            if (ModelState.IsValid)
                userRepository.Update(user);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userRepository.Delete(id);
        }
    }
}

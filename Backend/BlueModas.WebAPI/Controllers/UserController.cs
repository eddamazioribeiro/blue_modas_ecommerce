using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Domain;
using BlueModas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlueModas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repo;

        public UserController(UserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                user.CreatedAt = DateTime.Now;
                
                _repo.Add(user);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(user);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode
                (
                    StatusCodes.Status500InternalServerError,
                    "Erro ao inserir as informações do banco de dados\n"
                    + ex.InnerException
                );
            }

            return BadRequest();
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> Update(int userId, User user)
        {
            try
            {
                var userAux = (User)await _repo.GetUserByIdAsync(userId);
                
                if(userAux == null)
                {
                    return NotFound();
                }
                
                userAux.Username = user.Username;
                userAux.Email = user.Email;
                userAux.Phone = user.Phone;
                userAux.Password = user.Password;
                userAux.UpdatedAt = user.UpdatedAt;

                _repo.Update(userAux);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(userAux);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao atualizar os dados do Usuário\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete(int addressId)
        {
            try
            {
                var addressAux = await _repo.GetUserByIdAsync(addressId);

                if(addressAux == null)
                {
                    return NotFound();
                }
                
                _repo.Delete(addressAux);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Enderço excluído com sucesso");
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao excluir o endereço\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetAddressById(int userId)
        {
            try
            {
                var result = await _repo.GetUserByIdAsync(userId);
                
                if(result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }              
        }                                              
    }
}

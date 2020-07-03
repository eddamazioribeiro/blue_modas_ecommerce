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
    public class AddressController : ControllerBase
    {
        private readonly AddressRepository _repo;

        public AddressController(AddressRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{UserId}")]
        public async Task<IActionResult> Create(int userId, Address address)
        {
            try
            {
                address.UserId = userId;
                
                _repo.Add(address);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok(address);
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

        [HttpPut("{AddressId}")]
        public async Task<IActionResult> Update(int addressId, Address address)
        {
            try
            {
                var addressAux = (Address)await _repo.GetAddressByIdAsync(addressId);
                
                if(addressAux == null)
                {
                    return NotFound();
                }
                
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;
                addressAux.Street = address.Street;

                _repo.Update(addressAux);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(addressAux);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao atualizar o endereço\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpDelete("{AddressId}")]
        public async Task<IActionResult> Delete(int addressId)
        {
            try
            {
                var addressAux = await _repo.GetAddressByIdAsync(addressId);

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

        [HttpGet("{AddressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            try
            {
                var result = await _repo.GetAddressByIdAsync(addressId);
                
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

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserMainAddress(int userId)
        {
            try
            {
                var result = await _repo.GetUserMainAddressAsync(userId);
                
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

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserAddresses(int userId)
        {
            try
            {
                var result = await _repo.GetUserAddressesAsync(userId);
                
                if(result.Count() == 0)
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

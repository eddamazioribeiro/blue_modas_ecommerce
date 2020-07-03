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
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repo;

        public OrderController(OrderRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            try
            {
                order.CreatedAt = DateTime.Now;

                _repo.Add(order);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/order/{order.Id}", order);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode
                (
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException
                );
            }

            return BadRequest();
        }

        [HttpPut("{OrderId}")]
        public async Task<IActionResult> Update(int orderId, Order order)
        {
            try
            {
                var orderAux = (Order)await _repo.GetByIdAsync(orderId);
                
                if(orderAux == null)
                {
                    return NotFound();
                }
                
                orderAux.UserId = order.UserId;
                orderAux.Payment = order.Payment;
                orderAux.Status = order.Status;
                orderAux.ShippingAddressId = order.ShippingAddressId;
                orderAux.UpdatedAt = DateTime.Now;
                // orderAux.Items = order.Items;

                _repo.Update(orderAux);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(orderAux);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao atualizar o pedido\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpDelete("{OrderId}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            try
            {
                var orderAux = await _repo.GetByIdAsync(orderId);

                if(orderAux == null)
                {
                    return NotFound();
                }
                
                _repo.Delete(orderAux);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Pedido excluído com sucesso");
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao excluir o pedido\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            try
            {
                var result = await _repo.GetByIdAsync(orderId);
                
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrdersAsync(int userId)
        {
            try
            {
                var result = await _repo.GetUserOrdersAsync(userId);
                
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

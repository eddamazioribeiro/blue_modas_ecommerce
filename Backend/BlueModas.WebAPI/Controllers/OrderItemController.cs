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
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemRepository _repo;

        public OrderItemController(OrderItemRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem orderItem)
        {
            try
            {
                _repo.Add(orderItem);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
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

        [HttpPut]
        public async Task<IActionResult> Update(OrderItem item)
        {
            try
            {
                var itemAux = (OrderItem)await _repo.GetOrderItemAsync(item.OrderId, item.ProductId);
                
                if(itemAux == null)
                {
                    return NotFound();
                }
                
                itemAux.Price = item.Price;
                itemAux.Quantity = item.Quantity;

                _repo.Update(itemAux);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(itemAux);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao atualizar o item do pedido\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(OrderItem item)
        {
            try
            {
                var itemAux = await _repo.GetOrderItemAsync(item.OrderId, item.ProductId);

                if(itemAux == null)
                {
                    return NotFound();
                }
                
                _repo.Delete(itemAux);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Item do pedido excluído com sucesso");
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao excluir o item do pedido\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            try
            {
                var result = await _repo.GetOrderItemsAsync(orderId);
                
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

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
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repo;

        public ProductController(ProductRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                product.IncludedAt = DateTime.Now;

                _repo.Add(product);

                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/product/{product.Id}", product);
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

        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Update(int productId, Product product)
        {
            try
            {
                var productAux = (Product)await _repo.GetByIdAsync(productId);
                
                if(productAux == null)
                {
                    return NotFound();
                }
                
                productAux.Name = product.Name;
                productAux.Price = product.Price;
                productAux.Quantity = product.Quantity;
                productAux.ImageURL = product.ImageURL;
                productAux.UpdatedAt = DateTime.Now;

                _repo.Update(productAux);
                
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(productAux);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao atualizar o produto\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var productAux = await _repo.GetByIdAsync(productId);

                if(productAux == null)
                {
                    return NotFound("Product not found");
                }
                
                _repo.Delete(productAux);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok("Produto excluído com sucesso");
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao excluir o produto\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            try
            {
                var result = await _repo.GetByIdAsync(productId);
                
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repo.GetAllProducts();
                
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

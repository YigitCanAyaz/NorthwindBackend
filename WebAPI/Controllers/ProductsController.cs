using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Attribute
    public class ProductsController : ControllerBase
    {
        // Loosely coupled
        // defaultu private 
        // _ => naming convention
        // JS'te constructor'da verirsek aşağıdan da erişebiliriz
        // IoC Container -- Inversion of Control
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")] // Get Request (Nasıl çağrılacağını yazıyoruz)
        public IActionResult GetAll()
        {
            // Swagger => hazır dökümantasyon sağlar
            // Dependency chain (birbirine ihtiyaç duymak)
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result); // 200
            }
            return BadRequest(result); // 400
        }
        
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")] // Post Request
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

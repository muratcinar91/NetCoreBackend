using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryDal;

        public CategoriesController(ICategoryService categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            
            var result = _categoryDal.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int categoryId)
        {
            var result = _categoryDal.GetById(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Category category)
        {
            var result = _categoryDal.Add(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(Category category)
        {
            var result = _categoryDal.Update(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Category category)
        {
            var result = _categoryDal.Delete(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
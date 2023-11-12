using EcommerceAPI.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAPI.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IDataHelper<Category> idata;

        public CategoryController(IDataHelper<Category> _Idata)
        {
            idata = _Idata;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<object> Get()
        {
            
            return idata.GetAll();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IEnumerable<object> Get(int id)
        {
            var re = idata.GetById(id);
            return (re);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public void Post([FromBody] Category value)
        {
            idata.Add(value);
           
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public void Put(int id, [FromBody] Category value)
        {
            idata.Update(id, value);
           
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public void Delete(int id)
        {
            idata.Delete(id);
        }
    }
}

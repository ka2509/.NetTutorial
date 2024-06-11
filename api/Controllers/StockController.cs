using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {
            // read about deferred execution
            // So the query is not executed when it is defined or when its methods are called. So at this point, we need an ToList() to actually get the data out.
            var stocks = _context.Stock.ToList();
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) {
            var stock = _context.Stock.Find(id);
            if(stock == null) {
                return NotFound();
            }
            return Ok(stock);
        }
        
    }
}
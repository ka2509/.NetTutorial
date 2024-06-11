using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
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
            // Code line below is creating a list of 'Stock' entites then apply the ToStockDto() method to every index of that list. So we have a list of StockDto 
            var stocks = _context.Stock.ToList()
                .Select(s => s.ToStockDto());
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.StockD;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepo = stockRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            // read about deferred execution
            // So the query is not executed when it is defined or when its methods are called. So at this point, we need an ToList() to actually get the data out.
            // Code line below is creating a list of 'Stock' entites then apply the ToStockDto() method to every index of that list. So we have a list of StockDto 
            var stocks = await _stockRepo.GetAllAsync();

            var stocksDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocksDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null) {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if(stockModel == null) {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null) {
                return NotFound();
            }
            return NoContent();
        }
    }
}
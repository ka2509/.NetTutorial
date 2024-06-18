using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio() {
            var username = User.FindFirstValue(ClaimTypes.GivenName);
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol) {
            var username = User.FindFirstValue(ClaimTypes.GivenName);
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);
            if(stock == null) return BadRequest("Stock Not Found");
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            if(userPortfolio.Any(e => e.Symbol == symbol)) return BadRequest("Cannot add same stock");
            var portfolioModel = new Portfolio {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };
            return Ok(_portfolioRepo.CreateAsync(portfolioModel));
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol) {
            var username = User.FindFirstValue(ClaimTypes.GivenName);
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol == symbol);
            if(filteredStock.Count() == 1) {
                await _portfolioRepo.DeleteAsync(appUser, symbol);
            }
            else {
                return BadRequest("Stock not exist");
            }
            return Ok();
        }
    }
}
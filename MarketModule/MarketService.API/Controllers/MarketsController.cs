using MarketService.API.Dtos;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace MarketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MarketRepository _marketRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemsController(MarketRepository marketRepository, IPublishEndpoint publishEndpoint)
        {
            _marketRepository = marketRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _marketRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> Create(MarketCreateDto dto)
        {
            var result = _marketRepository.Add(new Data.Entities.Market { InventoryId = dto.InventoryId, ItemId = dto.ItemId, PlayerId = dto.PlayerId, Price = dto.Price });

            await _publishEndpoint.Publish<MarketCreated>(new { dto.InventoryId, dto.ItemId, Count = 1 });

            return Created("", result);
        }
    }
}

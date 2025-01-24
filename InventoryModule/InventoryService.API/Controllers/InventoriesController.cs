using InventoryService.API.Dtos;
using InventoryService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly InventoryRepository _inventoryRepository;

        public InventoriesController(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _inventoryRepository.Get();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _inventoryRepository.GetById(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _inventoryRepository.Remove(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InventoryCreateDto dto)
        {
            var result = await _inventoryRepository.Create(new Data.Entities.Inventory { PlayerId = dto.PlayerId, Name = dto.Name });

            return Created("", result);
        }

    }
}

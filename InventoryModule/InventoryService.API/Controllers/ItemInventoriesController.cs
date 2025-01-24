using InventoryService.API.Dtos;
using InventoryService.Data.Entities;
using InventoryService.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInventoriesController : ControllerBase
    {
        private readonly ItemInventoryRepository _itemInventoryRepository;

        public ItemInventoriesController(ItemInventoryRepository itemInventoryRepository)
        {
            _itemInventoryRepository = itemInventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _itemInventoryRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemInventoryCreateDto dto)
        {
            var result = await _itemInventoryRepository.Create(new ItemInventory { InventoryId = dto.InventoryId, ItemId = dto.ItemId, Count = dto.Count });

            return Created("", result);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(ItemInventoryUpdateDto dto)
        {
            var updatedEntity = await _itemInventoryRepository.GetItemInventory(dto.ItemId, dto.InventoryId);

            if (updatedEntity != null)
            {
                updatedEntity.Count = updatedEntity.Count - dto.Count;
                await _itemInventoryRepository.Update(updatedEntity);

                return NoContent();
            }

            return NotFound();
        }
    }
}

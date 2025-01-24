namespace MarketService.API.Dtos
{
    public record MarketCreateDto(string ItemId, string InventoryId, decimal Price, string PlayerId);

}

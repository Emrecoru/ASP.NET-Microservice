using InventoryService.Data.Repositories;
using MassTransit;
using Shared.Interfaces;

namespace InventoryService.API.Consumers
{
    public class MarketCreatedConsumer : IConsumer<MarketCreated>
    {

        public async Task Consume(ConsumeContext<MarketCreated> context)
        {
            var message = context.Message;
            var itemInventoryRepository = new ItemInventoryRepository();

            var itemInventory = await itemInventoryRepository.GetItemInventory(message.ItemId, message.InventoryId);

            if (itemInventory != null)
            {
                itemInventory.Count = itemInventory.Count - message.Count;
                await itemInventoryRepository.Update(itemInventory);
            }
        }
    }
}

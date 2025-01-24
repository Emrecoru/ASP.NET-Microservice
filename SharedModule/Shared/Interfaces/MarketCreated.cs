using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    //string ItemId, string InventoryId, decimal Price, string PlayerId
    //string InventoryId, string ItemId, int Count
    public interface MarketCreated
    {
        public string InventoryId { get; set; }

        public string ItemId { get; set; }

        public int Count { get; set; }
    }
}

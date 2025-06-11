using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Trade
    {
        public int TradeId { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int StockId { get; set; }
        public Stock Stock { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal TradePrice { get; set; }

        public DateTime TradeDate { get; set; } = DateTime.UtcNow;
    }
}

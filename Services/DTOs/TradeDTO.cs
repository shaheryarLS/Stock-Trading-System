using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class TradeDto
    {
        public int TradeId { get; set; }
        public string UserName { get; set; }
        public string StockSymbol { get; set; }
        public string StockCompany { get; set; }
        public int Quantity { get; set; }
        public decimal TradePrice { get; set; }
        public DateTime TradeDate { get; set; }
    }

    public class CreateTradeDto
    {
        public string UserId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public decimal TradePrice { get; set; }
    }
}

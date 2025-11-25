namespace Share.Trading.Domain.Entities.Models
{
    public class SharesDetails
    {
        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price per share.
        /// </summary>
        public decimal PricePerShare { get; set; }

        /// <summary>
        /// Gets or sets the quantity of shares.
        /// </summary>
        public int Quantity { get; set; }
    }
}

namespace Share.Trading.Domain.Entities.Models
{
    public class Portfolio
    {
        /// <summary>
        /// Portfolio Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cash Balance
        /// </summary>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// List of Shares in the Portfolio
        public List<SharesDetails> Shares { get; set; }
    }
}

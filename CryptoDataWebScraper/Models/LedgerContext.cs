using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CryptoDataWebScraper.Models
{
    public class LedgerContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<SelenLedger> SelenLedgers { get; set; }
    }
}
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public sealed class QRCodeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private string _connectionStr = string.Empty;

        public QRCodeDbContext(DbContextOptions<QRCodeDbContext> options) : base(options) { }

        public QRCodeDbContext(string connectionStr)
        {
            _connectionStr = connectionStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionStr))
            {
                //var dbExts = optionsBuilder.Options.FindExtension<MySQLOptionsExtension>();
                //if (dbExts == null)
                //    optionsBuilder.UseMySQL(_connectionStr);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DadosQRCodeConfiguration());
        }
    }
}

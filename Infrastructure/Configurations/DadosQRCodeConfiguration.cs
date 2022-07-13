using Microsoft.EntityFrameworkCore;
using Model;
namespace Infrastructure.Configurations
{
    public class DadosQRCodeConfiguration : IEntityTypeConfiguration<DadosQRCode>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DadosQRCode> builder)
        {
            builder.ToTable("dadosqrcode");
            builder.HasKey(e => e.Id);
        }
        
    }
}
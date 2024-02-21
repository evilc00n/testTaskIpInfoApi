using IpInfo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace IpInfo.Dal.Configuration
{
    public class IpInfoConfiguration : IEntityTypeConfiguration<IpInfoEntity>
    {
        /// <summary>
        /// Конфигурация принимается в ApplicationDbContext
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<IpInfoEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.IpAddress).IsRequired();
            builder.Property(x => x.InfoData).IsRequired().HasColumnType("json");
        }
    }
}

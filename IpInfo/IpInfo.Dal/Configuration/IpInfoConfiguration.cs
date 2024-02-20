using IpInfo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace IpInfo.Dal.Configuration
{
    public class IpInfoConfiguration : IEntityTypeConfiguration<IpInfoEntity>
    {
        public void Configure(EntityTypeBuilder<IpInfoEntity> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.IpAddress).IsRequired();
            builder.Property(x => x.InfoData).IsRequired();


        }
    }
}

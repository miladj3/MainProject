

using DomainClasses.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DomainClasses.Configuraion
{
    public class ImageConfig : EntityTypeConfiguration<ProductImage>
    {
        public ImageConfig()
        {
            Property(a => a.Path)
                .IsOptional().IsMaxLength();
        }
    }
}

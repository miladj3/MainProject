using DomainClasses.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DomainClasses.Configuraion
{
    public class CommentConfig:EntityTypeConfiguration<Comment>
    {
        public CommentConfig()
        {
            this.HasRequired(x => x.Product)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .WillCascadeOnDelete(false);

            this.HasMany(x => x.LikedUser)
                .WithMany(x => x.LikedComments)
                .Map(x =>
                {
                    x.ToTable("LikeUsersComment");
                    x.MapLeftKey("CommentId");
                    x.MapRightKey("UserId");
                });

            this.Property(x => x.Content)
                .IsMaxLength();

            this.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}

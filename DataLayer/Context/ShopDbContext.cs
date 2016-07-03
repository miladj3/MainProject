using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Configuraion;
using System.Data.Entity.Core.Objects;
using EFSecondLevelCache;

namespace DataLayer.Context
{
    public class ShopDbContext : DbContext, IUnitOfWork
    {
        #region Constructure
        public ShopDbContext() : base("ShopDbContext")
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = true;
        }
        #endregion
        
        #region DbSet
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SiteOption> SiteOptions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Value> SpecificationValues { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<ProductImage> Images { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<DomainClasses.Entities.Attribute> Attributes { get; set; }
        public DbSet<SlideShow> SlideShows { get; set; }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentConfig());
            modelBuilder.Configurations.Add(new SlideShowConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new ShoppingCartConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new OrderDetailConfig());
            modelBuilder.Configurations.Add(new ImageConfig());
            modelBuilder.Configurations.Add(new AttributeConfig());
            modelBuilder.Configurations.Add(new FolderConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new ValueConfig());
            modelBuilder.Configurations.Add(new ContactConfig());
            modelBuilder.Configurations.Add(new PageConfig());
            modelBuilder.Configurations.Add(new SiteOptionConfig());
            modelBuilder.Properties().Where(x => x.Name == "Id").Configure(x => x.IsKey().HasColumnName(x.ClrPropertyInfo.DeclaringType.Name + "Id"));

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region UnitOfWork
        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class =>
            base.Set<TEntity>();

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class =>
            Database.SqlQuery<T>(sql, parameters).ToList();


        public void ForceDatabaseInitialize()
        {
            Database.Initialize(true);
        }

        public override int SaveChanges() => SaveAllChanges();


        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            var changedEntityNames = GetChangedEntityNames();
            var result = base.SaveChanges();
            if (invalidateCacheDependencies)
            {
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            }
            return result;
        }

        private string[] GetChangedEntityNames() =>
            ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        public override Task<int> SaveChangesAsync() =>
            SaveAllChangesAsync();


        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {
            var changedEntityNames = GetChangedEntityNames();
            var result = base.SaveChangesAsync();
            if (invalidateCacheDependencies)
            {
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            }
            return result;
        }

        public void AutoDetectChangesEnabled(bool flag = true)
        {
            Configuration.AutoDetectChangesEnabled = flag;
        }
        #endregion
    }
}

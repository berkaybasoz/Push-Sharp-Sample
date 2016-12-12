using MobileNotification.DAL.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MobileNotification.DAL.Context
{
    public  class PushContext : DbContext
    {
        public PushContext()
            : base("name=PushContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<MusteriBildirimMobil> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            //modelBuilder.Entity<Feed>()
            //            .HasRequired<App>(s => s.Application)
            //            .WithMany(s => s.Feeds)
            //            .HasForeignKey(s => s.AppRefId);

            //modelBuilder.Entity<App>()
            //         .HasMany<Feed>(s => s.Feeds)
            //         .WithRequired(s => s.Application)
            //         .HasForeignKey(s => s.AppRefID); 
            base.OnModelCreating(modelBuilder);
        }
    }
}
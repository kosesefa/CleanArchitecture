using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context
{
    public sealed class AppDbContext : IdentityDbContext<User,IdentityRole,string>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        //Model Builder üzerinden yaptığımız bu assembly işlemi ile artık kaç tane entity class'ımıza configuration ayarı yaparsak yapalım,hepsi DbContext'e gönderilecek.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();
            modelBuilder.Ignore<IdentityRole<string>>();
        }
            


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Burada yaptığımız işlem ile ise Entity isimli base entity'imizin içinde bulunan tarihleri otomatik olarak işlem tarihiyle eşleştirmiş olduk.
            var entires = ChangeTracker.Entries<Entity>();
            foreach (var entry in entires)
            {
                if (entry.State == EntityState.Added)
                    entry.Property(p => p.CreatedDate)
                        .CurrentValue = DateTime.Now;
                if(entry.State == EntityState.Modified)
                    entry.Property(p=>p.UpdatedDate)
                        .CurrentValue = DateTime.Now;
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }

    
   
}

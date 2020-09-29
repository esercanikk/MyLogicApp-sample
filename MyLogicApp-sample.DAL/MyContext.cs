using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyLogicApp_sample.ENTITY.IdentityModels;
using MyLogicApp_sample.ENTITY.Models;
using MyLogicApp_sample.ENTITY.Models.WorkFlow;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyLogicApp_sample.DAL
{
    public class MyContext : IdentityDbContext<User>
    {
        public MyContext() : base("name=MyCon")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Baglayici>()
                .HasRequired(x => x.OncekiAdim)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Baglayici>()
                .HasRequired(x => x.SonrakiAdim)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            var userId = HttpContext.Current?.User == null ? "" : HttpContext.Current.User.Identity.GetUserId();

            var selectedEntityList = ChangeTracker.Entries()
                .Where(x => x.Entity is AuditBase && x.State == EntityState.Added);

            foreach (var entity in selectedEntityList)
            {
                ((AuditBase)(entity.Entity)).CreatedUser = ((AuditBase)(entity.Entity)).CreatedUser ?? userId;

                if (((AuditBase)(entity.Entity)).CreatedDate == new DateTime(1, 1, 1))
                    ((AuditBase)(entity.Entity)).CreatedDate = DateTime.Now;
            }
            selectedEntityList = ChangeTracker.Entries()
                .Where(x => x.Entity is AuditBase && x.State == EntityState.Modified);

            foreach (var entity in selectedEntityList)
            {
                ((AuditBase)(entity.Entity)).UpdatedUser = userId;
                ((AuditBase)(entity.Entity)).UpdatedDate = DateTime.Now;
            }

            return base.SaveChanges();
        }

        public virtual DbSet<Sema> Semalar { get; set; }
        public virtual DbSet<Adim> Adimlar { get; set; }
        public virtual DbSet<Baglayici> Baglayicilar { get; set; }
        public virtual DbSet<IsAtama> IsAtamalar { get; set; }
    }
}

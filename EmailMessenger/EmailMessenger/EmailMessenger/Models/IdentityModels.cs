using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EmEntity;
using System.ComponentModel.DataAnnotations;

namespace EmailMessenger.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class EM_ServerConnect:ServerConnect 
    {
        public EM_ServerConnect(string connectionName,
                                string serverHostName,
                                string serverUserName, 
                                string serverPassword,
                                string databaseName, 
                                string tableName)
        {
                      
            ConnectionName = connectionName;
            ServerHostNameIP = serverHostName;           
            ServerUserName = serverUserName;
            ServerPassword = serverPassword;
            DatabaseName = databaseName;
            TableName = tableName;
     
        }
        [StringLength(50)]
        public override string ConnectionName { get; set; } 
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<EM_ServerConnect> EM_ServerConnect { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        //Identity 1 table name change
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EM_ServerConnect>().ToTable("EM_ServerConnect");

            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

          //  modelBuilder.Entity<IdentityUser>().ToTable("User"); //add this for Identity 2
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
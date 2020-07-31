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
        public EM_ServerConnect() { }
        public EM_ServerConnect(string connectionName,
                                string serverHostName,
                                string serverUserName, 
                                string serverPassword,
                                string databaseName, 
                                string tableName,
                                string dbProvider)
        {
                      
            ConnectionName = connectionName;
            ServerHostNameIP = serverHostName;           
            ServerUserName = serverUserName;
            ServerPassword = serverPassword;
            DatabaseName = databaseName;
            TableName = tableName;
            ServerType = dbProvider;
     
        }
        [StringLength(50)]
        public override string ConnectionName { get; set; } 
    }

    
    public class SqlDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<EM_ServerConnect> EM_ServerConnect { get; set; }
        public SqlDbContext()
            : base("DefaultConnection1", throwIfV1Schema: false)
        {
        }


        //Identity 1 table name change
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EM_ServerConnect>().ToTable("ServerConnect");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Attachment>().ToTable("Attachment");
            modelBuilder.Entity<MailTracker>().ToTable("MailTracker");
            modelBuilder.Entity<MailTemplate>().ToTable("MailTemplate");
            modelBuilder.Entity<MailTemplateText>().ToTable("MailTemplateText");

            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

          //  modelBuilder.Entity<IdentityUser>().ToTable("User"); //add this for Identity 2
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");

            //             create table matchRequirementsToTests(
            // requirements varchar(200),
            // testcase varchar(200),
            //primary key(requirements, testcase),
            //foreign key (requirements) references Requirements(id),
            //foreign key(test case) references Test_cases(id))
        }
        public static SqlDbContext Create()
        {
            return new SqlDbContext();
        }
    }
}
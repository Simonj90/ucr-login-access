using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace login_access.Models
{
    public class UserSubmitModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool CoolDB { get; set; }
        [Required]
        public bool AwesomeDB { get; set; }
    }

    
    public class UserSubmitDBContext : DbContext
    {
        public UserSubmitDBContext() : base("DefaultConnection") { }
        public DbSet<UserSubmitModel> UserSubmits { get; set; }
    }
}
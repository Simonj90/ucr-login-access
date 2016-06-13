using System.Data.Entity;

namespace login_access.Models
{
    public class UserSubmitModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string FreeText { get; set; }
        public bool CoolDB { get; set; }
        public bool AwesomeDB { get; set; }
        public bool FinishedSubmit { get; set; }
        public string ReturnText { get; set; }
    }

    
    public class UserSubmitDBContext : DbContext
    {
        public UserSubmitDBContext() : base("DefaultConnection") { }

        public DbSet<UserSubmitModel> UserSubmits { get; set; }
    }
}
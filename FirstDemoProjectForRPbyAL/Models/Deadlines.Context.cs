namespace FirstDemoProjectForRPbyAL.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    
    public partial class DeadlinesEntities : DbContext
    {
        public DeadlinesEntities() : base("name=DeadlinesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}

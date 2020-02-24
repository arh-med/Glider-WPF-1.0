using Glider_WPF_1._0.Migrations;
using MySql.Data.Entity;
using System.Data.Entity;

namespace Glider_WPF_1._0.Sql
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GliderDataContext : DbContext // класс установленый с пакетом System.Data.Entity для работы с базой данных
    {   
        private static GliderDataContext _instance; // статическое поле 
        public static GliderDataContext Instance // данное свойство дайт доступ к безе данный с инициализацие только один раз 
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new GliderDataContext();
                }

                return _instance;
            } 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

        public GliderDataContext() : base()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GliderDataContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Login);
            modelBuilder.Entity<Request>().HasKey(u => u.Id);
        }
    }
}

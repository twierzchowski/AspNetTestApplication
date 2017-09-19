using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AspNetTestApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var dbContext = new TestContext(connectionString))
            {
                dbContext.Database.Initialize(true);
            }
        }
    }

    public class Test
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class TestContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public TestContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DbContext>());
        }
    }


    public class DbInitializer : DropCreateDatabaseIfModelChanges<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            context.Tests.AddRange(new List<Test>{new Test{id = 1, name="test"}, new Test{id=2, name="ok"}});
            context.SaveChanges();
        }
    }
}

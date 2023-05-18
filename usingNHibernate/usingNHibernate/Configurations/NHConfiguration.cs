using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System.Reflection;
using usingNHibernate.Mappings.FM;

namespace usingNHibernate.Configurations
{
    public class NHConfiguration
    {
        private readonly Configuration configuration;

        public NHConfiguration()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NHDemoDbGr1;Integrated Security=True;";

            configuration = new Configuration();
            configuration.DataBaseIntegration(config =>
            {
                config.ConnectionString = connectionString;
                config.Driver<SqlClientDriver>();
                config.Dialect<MsSql2012Dialect>();
            })
                .AddMapping(getMappings());

            configuration.AddAssembly(Assembly.GetExecutingAssembly());


        }

        private HbmMapping getMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<AddressMapping>();
            //mapper.AddMapping<BenefitMapping>();
            //mapper.AddMapping<LeaveMapping>();
            //mapper.AddMapping<FoodTicketMapping>();
            mapper.AddMapping<EmployeeMapping>();

            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public ISessionFactory GetSessionFactory()
        {
            return configuration.BuildSessionFactory();

        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Gringotts.Persistence
{
    public class NHibernateInMemoryTestFixtureBase
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;

        public static void InitalizeSessionFactory()
        {
            if (sessionFactory != null)
                return;

            var properties = new Dictionary<string, string>();
            properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            properties.Add("connection.connection_string", "Data Source=:memory:;Version=3;New=True;");
            properties.Add("connection.release_mode", "on_close");
            properties.Add("show_sql", "true");

            configuration = new Configuration();
            configuration.Properties = properties;
        	configuration.AddAssembly("Gringotts");

            configuration.BuildMapping();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public ISession CreateSession()
        {
            ISession openSession = sessionFactory.OpenSession();
            IDbConnection connection = openSession.Connection;
            new SchemaExport(configuration).Execute(false, true, false, true, connection, null);
            return openSession;
        }

    }
}

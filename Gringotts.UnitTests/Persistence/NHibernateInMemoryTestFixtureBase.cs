using System.Collections.Generic;
using System.Data;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Gringotts.Persistence
{
    public class NHibernateInMemoryTestFixtureBase
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;
        protected ISession session;

        public static void InitalizeSessionFactory()
        {
            if (sessionFactory != null)
                return;

            var properties = new Dictionary<string, string>();
            properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver,NHibernate");
            properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            properties.Add("connection.connection_string", "Data Source=./../../../database/gringotts.db;Version=3;New=True;");
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
            return openSession;
        }

        [SetUp]
        public void SetUp(){
            session = CreateSession();
            session.BeginTransaction();
        }

        [TearDown]
        public void TearDown(){
            session.Transaction.Rollback();
            session.Dispose();
        }
    }
}

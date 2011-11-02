using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Inventory.Utilities
{
    class Program
    {
        static Configuration m_NHConfiguration;
        static ISessionFactory m_SessionFactory;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            m_NHConfiguration = new ConfigurationBuilder().Build();
            m_SessionFactory = m_NHConfiguration.BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            Console.WriteLine("Choose Option:");
            Console.WriteLine("  1. Export Schema");

            ConsoleKey option = Console.ReadKey().Key;
            switch (option)
            {
                case ConsoleKey.D1:
                    CreateSchema();
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }

        static void CreateSchema()
        {
            SchemaExport export = new SchemaExport(m_NHConfiguration);
            export.SetDelimiter(";");
            export.SetOutputFile(@"db.sql").Execute(false, false, false);
            Console.WriteLine("Schema Exported");
        }
    }
}

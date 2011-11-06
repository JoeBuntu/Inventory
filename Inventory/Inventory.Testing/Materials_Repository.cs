using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using Inventory.Core;
using Inventory.Core.Data;
using Inventory.Core.Entities;

namespace Inventory.Testing
{
    [TestFixture]
    public class Materials_Repository
    {
        static Configuration m_NHConfiguration;
        static ISessionFactory m_SessionFactory;
        MaterialsRepository m_Repository;
       
        public Materials_Repository()
        {
            log4net.Config.XmlConfigurator.Configure();
            m_NHConfiguration = new ConfigurationBuilder().Build();
            m_SessionFactory = m_NHConfiguration.BuildSessionFactory();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            m_Repository = new MaterialsRepository(m_SessionFactory);

            //setup test data ...
            using (ISession session = m_SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //clean any existing records
                    foreach (Material m in session.Query<Material>())
                    {
                        session.Delete(m);
                    }
                    transaction.Commit();
                }
                using (ITransaction transaction = session.BeginTransaction())
                {
                    //insert some test data
                    for (int i = 0; i < 10; i++)
                    {
                        Material material = new Material();
                        material.PartNumber = "Part" + i.ToString();
                        material.Description = "This is part " + i.ToString();
                        material.Type = MaterialType.Component;
                        material.PiecesPerCase = 5;
                        material.EachesPerPiece = 5;

                        session.Save(material);
                    }
                    transaction.Commit();
                }             
            }
        }

        [Test]
        public void Can_Insert_Material()
        {
            int id;
            using (ISession session = m_SessionFactory.OpenSession())
            {
                using (ITransaction trxn = session.BeginTransaction())
                {
                    //Arrange: given a new material
                    Material m = new Material() { PartNumber = "Part11", Type = MaterialType.Product };

                    //Act: when we insert the part into database...
                    id = (int)m_Repository.Add(m);
                }
            }

            //Assert: returned id is not a default value
            Assert.AreNotEqual(id, default(int));
        }

        [Test]
        public void Can_Select_Material_By_ID()
        {
            //Arrange: Given a part number is in database and we have the id
            int id;
            using (ISession session = m_SessionFactory.OpenSession())
            {
                using (ITransaction trxn = session.BeginTransaction())
                {
                    Material m = new Material() { PartNumber = "Part12", Type = MaterialType.Component };
                    id = (int)session.Save(m);
                    trxn.Commit();
                }
            }

            //Act: when we fetch the material by id...
            Material m1 = m_Repository.Get(id);

            //Assert: the material is not null and has the correct part number
            Assert.NotNull(m1);
            Assert.AreEqual("Part12", m1.PartNumber);
        }

        [Test]
        public void Can_Select_Materials_By_Range()
        {
            //Arrange: given we have a start index and a count
            int start = 0;
            int count = 5;

            //Act: when we fetch materials using the range
            Material[] results = m_Repository.Get(start, count).ToArray();

            //Assert the result set count is 5 and the first part number is 'Part1'
            Assert.AreEqual(5, results.Length);
            Assert.AreEqual("Part0", results[0].PartNumber);
        }

        [Test]
        public void Can_Not_Add_Duplicate_PartNumbers()
        {
            //Arrange: given we have two materials with the same part number
            Material m1 = new Material() { PartNumber = "Part13", Description = "", Type = MaterialType.Component };
            Material m2 = new Material() { PartNumber = "Part13", Description = "", Type = MaterialType.Component };

            //Act: when we try to save both
            m_Repository.Add(m1);

            //Assert: the repository should throw and exception for duplicate part number
            NUnit.Framework.Assert.Catch<NHibernate.ADOException>(() => m_Repository.Add(m2));
        }

        [Test]
        public void Can_Not_Modify_PartNumber()
        {
            //Given: we have a material that has been persisted to the database
            Material m1 = new Material() { PartNumber = "Part14", Description = "", Type = MaterialType.Component };

            m_Repository.Add(m1);

            //Act: when the part number is changed...
            m1.PartNumber = "Part15";

            //Assert: the repository should throw and exception for altering a non-mutable natural id
            Assert.Catch<NHibernate.HibernateException>(() => m_Repository.Update(m1));
        }

        [Test]
        public void Can_Delete_Materials()
        {
            //Arrange: we add a material to the repository
            Material m1 = new Material() { PartNumber = "Part15", Description = "", Type = MaterialType.Component };
            m_Repository.Add(m1);
            
            //Act: when we delete the material from the repository...
            m_Repository.Delete(m1);
            
            //Assert: the repository should no longer contain the material
            Material m2 = m_Repository.Get(m1.Id);
            Assert.IsNull(m2);
        }

        [Test]
        public void Throws_Exception_On_StaleObject()
        {
            //Arrange: given we get an instance of a material
            Material m1 = m_Repository.Get(0, 1)[0];
            m1.PiecesPerCase += 1;

            //Act: if the material is updated by another user...
            using (MySqlConnection cnx = new MySqlConnection(@"Server=localhost;Database=inv_testing;Uid=root;Pwd=386Jamie;"))
            {
                using (MySqlCommand sqlCmd = new MySqlCommand("update mtrl set mtrl_vrsn = 3 where mtrl_id = @id", cnx))
                {
                    cnx.Open();
                    sqlCmd.Parameters.AddWithValue("@id", m1.Id);
                    sqlCmd.ExecuteNonQuery();
                }
            }

            //Assert: an exception should be thrown when we save the update to the repository
            Assert.Catch<StaleObjectStateException>(() => m_Repository.Update(m1));
        }
    }
}

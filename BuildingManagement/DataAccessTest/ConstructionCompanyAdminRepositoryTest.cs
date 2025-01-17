﻿using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class ConstructionCompanyAdminRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateConstructionCompanyAdminTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestCreateConstructionCompanyAdmin"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                ConstructionCompanyAdmin expected = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    Password = "password123",
                    Name = "Construction Company Admin"
                };

                repository.CreateConstructionCompanyAdmin(expected);
                context.SaveChanges();

                ConstructionCompanyAdmin storedAdmin = context.ConstructionCompanyAdmins.FirstOrDefault(a => a.Id == expected.Id);
                Assert.IsNotNull(storedAdmin);
                Assert.AreEqual(expected.Email, storedAdmin.Email);
            }
        }

        [TestMethod]
        public void EmailExistsInConstructionCompanyAdminsTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestEmailExistsInConstructionCompanyAdmins"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    Password = "password123",
                    Name = "Construction Company Admin"
                };

                context.ConstructionCompanyAdmins.Add(admin);
                context.SaveChanges();

                bool exists = repository.EmailExistsInConstructionCompanyAdmins(admin.Email);
                Assert.IsTrue(exists);

                bool notExists = repository.EmailExistsInConstructionCompanyAdmins("nonexistent@example.com");
                Assert.IsFalse(notExists);
            }
        }

        [TestMethod]
        public void GetConstructionCompanyAdminByIdTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetConstructionCompanyAdminById"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                Guid adminId = Guid.NewGuid();
                ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
                {
                    Id = adminId,
                    Email = "admin@example.com",
                    Password = "password123",
                    ConstructionCompany = new ConstructionCompany
                    {
                        ConstructionCompanyId = Guid.NewGuid(),
                        Name = "Construction Company"
                    },
                    Name = "Construction Company Admin"
                };

                context.ConstructionCompanyAdmins.Add(admin);
                context.SaveChanges();

                ConstructionCompanyAdmin retrievedAdmin = repository.GetConstructionCompanyAdminById(adminId);

                Assert.IsNotNull(retrievedAdmin);
                Assert.AreEqual(admin.Email, retrievedAdmin.Email);
                Assert.IsNotNull(retrievedAdmin.ConstructionCompany);
                Assert.AreEqual(admin.ConstructionCompany.Name, retrievedAdmin.ConstructionCompany.Name);
            }
        }

        [TestMethod]
        public void Get_Succes()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetConstructionCompanyAdminById2"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                Guid adminId = Guid.NewGuid();
                ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
                {
                    Id = adminId,
                    Email = "email",
                    Name = "name",
                    Password = "password"
                };

                context.ConstructionCompanyAdmins.Add(admin);
                context.SaveChanges();

                Guid result = repository.Get(adminId);

                Assert.AreEqual(adminId, result);
            }
        }

        [TestMethod]
        public void GetByEmailAndPassword()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetConstructionCompanyAdminByEmailAndPassword"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "email",
                    Name = "name",
                    Password = "password"
                };

                context.ConstructionCompanyAdmins.Add(admin);
                context.SaveChanges();

                ConstructionCompanyAdmin result = repository.GetByEmailAndPassword(admin.Email, admin.Password);

                Assert.IsNotNull(result);
                Assert.AreEqual(admin.Email, result.Email);
                Assert.AreEqual(admin.Password, result.Password);
            }
        }
    }
}

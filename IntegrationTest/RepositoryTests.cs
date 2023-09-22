using Application.User.Queries.LoginUser;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationTest
{
    [TestFixture]
    public class RepositoryTests
    {
        IConfiguration _fakeConfig;
        CancellationToken _cancellationToken;
        public RepositoryTests()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "Sqlite")]).Returns("Data Source=Database.db");

            var config = new Mock<IConfiguration>();
            config.Setup(s => s.GetSection(It.Is<string>(a => a == "ConnectionStrings"))).Returns(mockConfSection.Object);

            _fakeConfig = config.Object;
            _cancellationToken = new CancellationToken();
            
            CleanDB();
        }

        [Test]
        public async Task ShouldPopulateIdWhenCreateAnUser()
        {
            //Arrange
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var x = builder.GetSection("Jwt:Key").Value!;

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LoginUserMapper()); 
            });
            var mapper = mockMapper.CreateMapper();

            var sutCreation = new UserRepository(builder);
            var sutLogin = new LoginUserQueryHandler(sutCreation, mapper, builder);

            var userToSave = new User
            {
                Name = "IntegrationTest",
                Email = "Int@test.test",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123")
            };

            LoginUserQuery loginUserQuery = new LoginUserQuery
            {
                Email = "Int@test.test",
                Password = "Test123"
            };

            //Act
            var addResult = await sutCreation.AddAsync(userToSave);
            var loginResult = await sutLogin.Handle(loginUserQuery, _cancellationToken);

            //Assert
            Assert.That(addResult, Is.Not.EqualTo(0));
            Assert.That(loginResult, Is.Not.Null);
            Assert.That(loginResult?.Token, Is.Not.Empty);
        }

        private async void CleanDB()
        {
            var userRepo = new BaseRepository<User>(_fakeConfig);
            await userRepo.DeleteAllAsync();
        }
    }
}

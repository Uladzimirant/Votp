using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;
using Votp;
using Votp.Contracts.Services;
using Votp.Controllers.Admin;
using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Models.Request;
using Votp.Services.Realizations;

namespace Tests.Votp
{
    public class TokenControllerTest
    {
        private TokensController CreateTokenController(IVotpDbContext votpDbContext)
        {
            var lfact = LoggerFactory.Create(e => { });
            var map = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile())).CreateMapper();


            var dbservice = new DBTokenService(lfact.CreateLogger<DBTokenService>(), map, votpDbContext);
            ITokenService tokenService = dbservice;
            IUserService userService = dbservice;

            TokensController tokensController
                = new TokensController(lfact.CreateLogger<TokensController>(), map, tokenService, userService);
            return tokensController;
        }

        private void DefaultFillDB(VotpDbContext context)
        {
            context.Database.EnsureDeleted();
            User[] user =
{
                    new User{ Id = 1, Login = "U1" },
                    new User{ Id = 2, Login = "U2" },
                    new User{ Id = 3, Login = "U3" }
                };
            Token[] tokens =
            {
                    new Token() { Id = 1, User = user[0], Value = "1", RegistrationTime = DateTime.Now },
                    new Token() { Id = 2, User = user[1], Value = "2", RegistrationTime = DateTime.Now },
                    new Token() { Id = 3, User = user[2], Value = "3", RegistrationTime = DateTime.Now }
                };
            context.AddRange(tokens);
            context.SaveChanges();
        }


        [Theory]
        [InlineData("4", "U1")]
        [InlineData("4", "U2")]
        public void CreateToken_Correct(string token, string username)
        {
            //arrange
            var options = new DbContextOptionsBuilder<VotpDbContext>().UseInMemoryDatabase(databaseName: "Votp").Options;
            using (var context = new VotpDbContext(options, false))
            {
                DefaultFillDB(context);
                TokensController controller = CreateTokenController(context);

                //act
                controller.Create(new TokenIDto() { Value = token, UserName = username }).Wait();

                //assert
                Assert.Contains(context.Tokens, e => e.Value == token);
            }
        }

        [Theory]
        [InlineData("4", "U4")]
        public void CreateToken_Incorrect(string token, string username)
        {
            //arrange
            var options = new DbContextOptionsBuilder<VotpDbContext>().UseInMemoryDatabase(databaseName: "Votp").Options;
            using (var context = new VotpDbContext(options, false))
            {
                DefaultFillDB(context);
                TokensController controller = CreateTokenController(context);
                //act
                //assert
                Assert.Throws<AggregateException>(() =>
                {
                    controller.Create(new TokenIDto() { Value = token, UserName = username }).Wait();
                });
            }
        }

    }
}
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
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
        private static List<Token> GenList()
        {
            User[] user =
{
                    new User{ Id = 1, Login = "U1" },
                    new User{ Id = 2, Login = "U2" },
                    new User{ Id = 3, Login = "U3" }
                };
            return new List<Token>()
            {
                    new Token() { Id = 1, User = user[0], Value = "1", RegistrationTime = DateTime.Now },
                    new Token() { Id = 2, User = user[1], Value = "2", RegistrationTime = DateTime.Now },
                    new Token() { Id = 3, User = user[2], Value = "3", RegistrationTime = DateTime.Now }
            };
        }

        private TokensController CreateTokenController(out List<Token> innerList)
        {
            var lfact = LoggerFactory.Create(e => { });
            var map = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile())).CreateMapper();

            //var dbservice = new DBTokenService(lfact.CreateLogger<DBTokenService>(), map, votpDbContext);
            var mockToken = new Mock<ITokenService>();
            var list = GenList();
            mockToken.Setup(a => a.CreateToken(It.IsAny<Token>())).Callback<Token>((t)=>list.Add(t));
            mockToken.Setup(a => a.GetTokens()).ReturnsAsync(list);
            ITokenService tokenService = mockToken.Object;
            IUserService userService = Mock.Of<IUserService>();


            TokensController tokensController
                = new TokensController(lfact.CreateLogger<TokensController>(), map, tokenService, userService);
            innerList = list;
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
            TokensController controller = CreateTokenController(out var list);

            //act
            controller.Create(new TokenIDto() { Value = token, UserName = username }).Wait();

            //assert
            Assert.Contains(list, e => e.Value == token);
        }

    }
}
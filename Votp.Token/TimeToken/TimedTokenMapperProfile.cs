using AutoMapper;
using Votp.DS.Database.Entities;
using Votp.DS.TToken;
using Votp.Web.TokenBase.Model;
using Votp.Web.TToken.Models;

namespace Votp.Web.TToken
{
    public class TimedTokenMapperProfile : Profile
    {
        public TimedTokenMapperProfile()
        {
            //CreateMap<Token, TokenODto>()
            //    .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Login))
            //    .ForMember(d => d.Token, o => o.MapFrom(s => s.Value));
            CreateMap<TokenIDto, Token>().ForMember(d => d.User, o => o.MapFrom(s=> new User() { Login = s.UserName }));
            CreateMap<TimeTokenIDto, TimeToken>().IncludeBase<TokenIDto, Token>();
        }
    }
}

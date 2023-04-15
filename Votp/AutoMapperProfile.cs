using AutoMapper;
using Votp.DS.Entities;
using Votp.Models.Request;
using Votp.Models.Response;

namespace Votp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Token, TokenODto>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Login))
                .ForMember(d => d.Token, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.TokenType, o => o.MapFrom(s => s.GetType().ToString()));
            CreateMap<User, UserODto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Login));
            CreateMap<TokenIDto, Token>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Value));

            CreateMap<ResolverInfo, UserResolverODto>();
            CreateMap<UserResolverIDto, ResolverInfo>();
        }
    }
}

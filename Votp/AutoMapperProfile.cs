using AutoMapper;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Entities;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.UserResolver.InnerDatabase;
using Votp.UserResolver.Ldap;

namespace Votp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Token, TokenODto>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.TokenType, o => o.MapFrom(s => s.GetType().Name));
            CreateMap<User, UserODto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Login));
            CreateMap<TokenIDto, Token>();

            CreateMap<ResolverInfo, UserResolverODto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.GetType().Name));
            CreateMap<DatabaseUserResolverInfo, UserResolverODto>()
                .IncludeBase<ResolverInfo, UserResolverODto>()
                .ForMember(d=>d.AdditionalDataString,o=>o.MapFrom(s=>"Users From Inner Database"));
            CreateMap<LdapUserResolverInfo, UserResolverODto>()
                .IncludeBase<ResolverInfo, UserResolverODto>()
                .ForMember(d => d.AdditionalDataString, o => o.MapFrom(
                    s => $"Server={s.Host}{(s.Port != null ? $":{s.Port}" : "")}"
                    ));

            CreateMap<UserResolverDatabaseIDto, DatabaseUserResolverInfo>();
            CreateMap<UserResolverLdapIDto, LdapUserResolverInfo>().
                ForMember(d => d.AttributesJson, o => o.MapFrom(s => s.Attributes));
        }
    }
}

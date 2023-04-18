using AutoMapper;
using OtpNet;
using Votp.DS.Entities;
using Votp.Tokens.Abstractions.Models;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Time.Models;
using Votp.Tokens.Totp.Entities;
using Votp.Tokens.Totp.Models;

namespace Votp.Tokens
{
    public class TokensMapperProfile : Profile
    {
        public TokensMapperProfile()
        {
            //CreateMap<Token, TokenODto>()
            //    .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Login))
            //    .ForMember(d => d.Token, o => o.MapFrom(s => s.Value));
            CreateMap<TokenIDto, Token>().ForMember(d => d.User, o => o.MapFrom(s => new User() { Login = s.UserName }));
            CreateMap<Token, TokenODto>().ForMember(d => d.User, o => o.MapFrom(s => s.User.Login));
            
            CreateMap<TimeTokenIDto, TimeToken>().IncludeBase<TokenIDto, Token>();
            CreateMap<TotpTokenIDto, TotpToken>().IncludeBase<TokenIDto, Token>();
            CreateMap<TimeToken, TokenODto>().IncludeBase<Token, TokenODto>();
            CreateMap<TotpToken, TotpTokenODto>().IncludeBase<Token, TokenODto>()
                .ForMember(d => d.KeyBase32, o => o.MapFrom(s=> Base32Encoding.ToString(s.Key)));
        }
    }
}

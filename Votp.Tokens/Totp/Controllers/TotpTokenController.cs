﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OtpNet;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Contracts.Services;
using Votp.Exceptions;
using Votp.Tokens.Abstractions.Controllers;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Time.Models;
using Votp.Tokens.Totp.Entities;
using Votp.Tokens.Totp.Models;

namespace Votp.Tokens.Totp.Controllers
{
    [Route("Tokens/[action]/[controller]")]
    public class TotpTokenController : TokenControllerBase
    {
        private readonly IByteGeneratorService _generator;
        public TotpTokenController(ILogger<TokenControllerBase> logger, IMapper mapper, ITokenService tokenService, IByteGeneratorService generator) : base(logger, mapper, tokenService)
        {
            _generator = generator;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TotpTokenIDto dto)
        {
            try
            {
                var totpToken = Mapper.Map<TotpToken>(dto);
                totpToken.Key = _generator.Generate(20);
                await AddToken(totpToken);
                return RedirectToAction("Details", new { id = (await TokenService.GetTokens()).Single(t => t.Name == dto.Name).Id });
            }
            catch (ExpectedException ex) { return View("ErrorMessage", ex.Message); }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //var rawToken = (await TokenService.GetTokens()).Single(e => e.Id == id);
            //if (rawToken == null) { return NoToken(id); }
            //var token = rawToken as TotpToken;
            //if (token == null) { return WrongToken(rawToken.GetType(), typeof(TotpToken)); }

            var (token, action) = await TryGetTokenByIdAs<TotpToken>(id);
            if (token == null) { return action ?? throw new NullReferenceException("TryGetTokenByIdAs returned second null in action which not supposed to happen"); }

            var odto = Mapper.Map<TotpTokenODto>(token);
            odto.QRImageBase64 = StringToBase64QREncoder.Transform($"otpauth://totp/votp:{token.User.Login}?secret={odto.KeyBase32}&issuer=votp");

            return View(odto);
        }
    }
}

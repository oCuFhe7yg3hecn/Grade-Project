﻿using AutoMapper;
using GradeProject.AuthService.Models.Clients;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure.Clients
{
    public class ImplicitClientMapStrategy : IClientMapStrategy
    {
        private readonly IMapper _mapper;

        public ImplicitClientMapStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Client Map(ClientInsertModel insertModel)
        {
            var client = _mapper.Map<Client>(insertModel);
            client.AllowOfflineAccess = false;
            client.AllowedGrantTypes = GrantTypes.Implicit;
            client.AllowAccessTokensViaBrowser = true;
            client.RedirectUris = insertModel.RedirectUris.Split(",").Select(x => x.Trim()).ToList();
            client.PostLogoutRedirectUris = insertModel.PostLogoutUris.Split(",").Select(x => x.Trim()).ToList();
            client.LogoUri = client.LogoUri ?? "/images/oauth-app-default.svg";

            return client;
        }
    }
}

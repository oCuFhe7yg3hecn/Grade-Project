using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.AuthService.Infrastructure;
using GradeProject.AuthService.Models.Clients;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Models;
using System.IO;
using IdentityServer4;

namespace GradeProject.AuthService.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientSvc;
        private readonly IMapper _mapper;
        private readonly IApiManagmentService _apiMng;
        private readonly IFilesSaveService _filesSvc;

        public ClientsController(
            IClientService clientSvc,
            IMapper mapper,
            IFilesSaveService filesSvc,
            IApiManagmentService apiMng)
        {
            _clientSvc = clientSvc;
            _mapper = mapper;
            _apiMng = apiMng;
            _filesSvc = filesSvc;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ClientInsertModel clientDto)
        {
            var fileName = $"images/Clients/{clientDto.ClientId}{Path.GetExtension(clientDto.ClientImage.FileName)}";
            await _filesSvc.SaveFile(fileName, clientDto.ClientImage);
            clientDto.ClientImageUrl = fileName;

            var client = new Client()
            {
                ClientId = clientDto.ClientId,
                ClientName = clientDto.ClientName,
                ClientUri = clientDto.ClientUri,
                LogoUri = clientDto.ClientImageUrl,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowOfflineAccess = true,
                RequireConsent = true,
                AllowAccessTokensViaBrowser = true,
            };

            client.RedirectUris = clientDto.RedirectUris.Split(" ");
            client.PostLogoutRedirectUris = clientDto.PostLogoutUris.Split(" ");
            client.AllowedScopes = new List<string>()
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            };

            _clientSvc.AddClient(client);

            return RedirectToAction(nameof(HomeController.Index));
        }

        //var modelClient = _mapper.Map<IdentityServer4.Models.Client>(client);
        //modelClient.RedirectUris = client.RedirectUris.Split(",").ToList();
        //modelClient.PostLogoutRedirectUris = client.PostLogoutUris.Split(",").ToList();


        //_apiMng.RegisterApi(client);

        //_clientSvc.AddClient(modelClient);
    }
}
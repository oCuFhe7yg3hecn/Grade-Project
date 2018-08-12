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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using GradeProject.AuthService.Models.Account;
using Newtonsoft.Json;
using GradeProject.AuthService.Infrastructure.Clients;

namespace GradeProject.AuthService.Controllers
{
    [Authorize]
    //[Authorize(Policy = "DevelopersOnly")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientSvc;
        private readonly IMapper _mapper;
        private readonly IApiManagmentService _apiMng;
        private readonly IFilesSaveService _filesSvc;
        private Guid _userId;

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
        public async Task<IActionResult> Index()
        {
            _userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
            var clients = await _clientSvc.GetUserClients(_userId);
            return View(clients);
        }


        [HttpGet]
        [Authorize]
        public IActionResult NewClient(string pass = "none")
        {
            ViewData["password"] = pass;
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add(string type = "oauth-client")
        {
            return View(new ClientInsertModel() { Type = type });
        }

        //Add granual authorization
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ClientInsertModel clientDto)
        {
            _userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);

            if (clientDto.Type.Equals("oauth-client") && clientDto.ClientLogo != null)
            {
                var fileName = $"images/Clients/{Guid.NewGuid()}{Path.GetExtension(clientDto.ClientLogo.FileName)}";
                await _filesSvc.SaveFile(fileName, clientDto.ClientLogo);
                clientDto.LogoUri = fileName;
            }

            var pwd = _clientSvc.AddClient(clientDto, _userId);

            return RedirectToAction(nameof(NewClient), new { pass = pwd });
        }
    }
}
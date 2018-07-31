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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add(string type="oauth-client")
        {
            return View(new ClientInsertModel() { Type = type });
        }


        //Add granual authorization
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ClientInsertModel clientDto)
        {
            if (clientDto.Type.Equals("oauth-client"))
            {
                var fileName = $"images/Clients/{Guid.NewGuid()}{Path.GetExtension(clientDto.ClientLogo.FileName)}";
                await _filesSvc.SaveFile(fileName, clientDto.ClientLogo);
                clientDto.LogoUri = fileName;
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub").Value;

            _clientSvc.AddClient(clientDto, Guid.Parse(userId));

            return RedirectToAction(nameof(Index));
        }
    }
}
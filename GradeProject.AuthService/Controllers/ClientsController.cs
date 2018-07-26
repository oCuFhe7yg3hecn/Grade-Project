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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new ClientInsertModel() { Type = "application" });
        }


        //Add granual authorization
        [HttpPost]
        public async Task<IActionResult> Add(ClientInsertModel clientDto)
        {
            //Якщо тут по якійсь причині кліжнт не буде зареєстрований, тоді це фото має видалятися.
            //Потім дороблю
            if (clientDto.Type.Equals("oauth-client"))
            {
                var fileName = $"images/Clients/{Guid.NewGuid()}{Path.GetExtension(clientDto.ClientLogo.FileName)}";
                await _filesSvc.SaveFile(fileName, clientDto.ClientLogo);
                clientDto.LogoUri = fileName;
            }

            _clientSvc.AddClient(clientDto);

            return RedirectToAction(nameof(Index));
        }
    }
}
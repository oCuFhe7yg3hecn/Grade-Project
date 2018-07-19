using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.AuthService.Infrastructure;
using GradeProject.AuthService.Models.Clients;
using Microsoft.AspNetCore.Mvc;

namespace GradeProject.AuthService.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientSvc;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientSvc, IMapper mapper)
        {
            _clientSvc = clientSvc;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ClientInsertModel client)
        {
            var modelClient = _mapper.Map<IdentityServer4.Models.Client>(client);
            modelClient.RedirectUris = client.RedirectUris.Split(",").ToList();
            modelClient.PostLogoutRedirectUris = client.PostLogoutUris.Split(",").ToList();

            _clientSvc.AddClient(modelClient);

            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
using Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Application.WebApi.ApplicationApi
{
    public class AccountController : ApiController
    {
        private IClientService _clientService;

        public AccountController(IClientService clientService)
        {
            _clientService = clientService;
        }


    }
}

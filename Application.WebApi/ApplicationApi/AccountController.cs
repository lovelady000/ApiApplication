using Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Application.WebApi.ApplicationApi
{   
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IClientService _clientService;

        public AccountController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            int count = _clientService.GetAll().Count();
            return request.CreateResponse(HttpStatusCode.OK, count);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCore_AAD_B2C.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost("cookie")]
        public HttpResponseMessage Post([FromBody]CustomRequest request)
        {

            var response = new CustomResponse()
            {
                encryptedToken = request.token + "encypted",
                valid = true
            };
            string jsonRes = JsonConvert.SerializeObject(response);

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            //create and set cookie in response
            var cookie = new CookieHeaderValue("sessionId", "123456");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.Host.Host;
            cookie.Path = "/";

            //resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            resp.Content = new StringContent("[OK]", System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            option.Domain = Request.Host.Host;
            option.Path = "/";
            option.HttpOnly = true;
            option.Secure = true;
            option.SameSite = SameSiteMode.Strict;

            var response = new CustomResponse()
            {
                encryptedToken = "encypted",
                valid = true
            };

            Response.Cookies.Append("sessionId", "123456", option);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

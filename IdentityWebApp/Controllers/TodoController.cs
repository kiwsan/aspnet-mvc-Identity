using System.Web.Http;

namespace IdentityWebApp.Controllers
{
    public class TodoController : ApiController
    {
        // GET: api/Todo
        [Authorize]
        public IHttpActionResult Get()
        {

            var userName = User.Identity.Name;

            return Ok($"Who is me?: I'm {userName}");
        }

        // GET: api/Todo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Todo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Todo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Todo/5
        public void Delete(int id)
        {
        }
    }
}

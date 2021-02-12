using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Conexion;

namespace ApiRest.Controllers
{
    public class UsuarioController : ApiController
    {
        private UsuarioEntities dbcontext = new UsuarioEntities();
        [ActionName("getall")]
        [HttpGet]
        public IEnumerable<USUARIOS> GetAll()
        {
            using(UsuarioEntities user = new UsuarioEntities())
            {
                return user.USUARIOS.ToList();
            }
        }

        [ActionName("getxid")]
        [HttpGet]
        public USUARIOS GetXId(int id)
        {
            using (UsuarioEntities user = new UsuarioEntities())
            {
                return user.USUARIOS.FirstOrDefault(f=>f.Id == id);
            }
        }

        [ActionName("adduser")]
        [HttpPost]
        public IHttpActionResult AddUser ([FromBody]USUARIOS user)
        {
            if (ModelState.IsValid)
            {
                dbcontext.USUARIOS.Add(user);
                dbcontext.SaveChanges();
                return Ok(user);
            }
            else
                return BadRequest();
        }

        [ActionName("updateuser")]
        [HttpPut]
        public IHttpActionResult UpdateUser ([FromBody]USUARIOS user)
        {
            if (ModelState.IsValid)
            {
                var id = user.Id;
                var usuarioExiste = dbcontext.USUARIOS.Count(c => c.Id == id) > 0;
                if (usuarioExiste)
                {
                    dbcontext.Entry(user).State = EntityState.Modified;
                    dbcontext.SaveChanges();
                    return Ok();
                }
                else
                    return NotFound();
            }
            else
                return BadRequest();
        }

        [ActionName("dropuser")]
        [HttpDelete]
       public IHttpActionResult DropUser (int id)
        {
            var user = dbcontext.USUARIOS.Find(id);
            if (user != null)
            {
                dbcontext.USUARIOS.Remove(user);
                dbcontext.SaveChanges();
                return Ok(user);
            }
            else
                return NotFound();
        }
    }
}

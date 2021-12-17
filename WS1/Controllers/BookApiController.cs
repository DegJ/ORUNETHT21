using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Data;

namespace WS1.Controllers {
    public class BookApiController : ApiController {

        [Route("api/book/delete/{id}")]
        [HttpGet]
        public IHttpActionResult DeleteBook(int id) {
            using (var context = new ApplicationDbContext()) {
                var book = context.Books.FirstOrDefault(x => x.Id == id);
                if (book == null) {
                    return BadRequest();
                }

                context.Books.Remove(book);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
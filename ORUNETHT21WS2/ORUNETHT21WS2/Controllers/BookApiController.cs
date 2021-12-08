using System.Web.Http;
using Data;
using Shared.Models;

namespace ORUNETHT21WS2.Controllers {
    [RoutePrefix("api/book")]
    public class BookApiController : ApiController {

        public BookRepository BookRepository {
            get { return new BookRepository(); }
        }

        [HttpPost]   // Anropa med $.ajax eller $.post
        [Route("delete")] // api/book/delete
        public IHttpActionResult Delete(BookDeleteModel model) {
            try {
                var deletewasok = BookRepository.DeleteBook(model.BookId);
                if (deletewasok) {
                    return Ok();
                }

                return BadRequest();
            } catch {
                return BadRequest();
            }
        }
    }
}

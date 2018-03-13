using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ngLibrary.Core;
using ngLibrary.Model;
using ngLibrary.Data;
using ngLibrary.Web.Models.ViewModels;

namespace ngLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private IBookRepository _repo;
        private ILogger<BooksController> _logger;
        private IConfigurationRoot _config;
        private IMapper _mapper { get; set; }

        public BooksController(IBookRepository repo, IMapper mapper, ILogger<BooksController> logger, IConfigurationRoot config)
        {
            if (logger == null)
                throw new ArgumentNullException("Object implementing ILogger needed for object initialization");

            if (config == null)
                throw new ArgumentNullException("Object implementing IConfigurationRoot needed for object initialization");

            if (mapper == null)
                throw new ArgumentNullException("Object implementing IMapper(AutoMapper) needed for object initialization");

            if (repo == null)
                throw new ArgumentNullException("Object implementing IBatchRepository needed for object initialization");

            _config = config;
            _logger = logger;
            _repo = repo;
            _mapper = mapper;

        }

        // GET api/books
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _mapper.Map<BookViewModel>(_repo.GetAllBooks());

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, String.Format("ERROR: Could not retrieve book information"));
            }

            return BadRequest("Internal Error: Could not retrieve book information");
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Book ID needs to be provided");
            }

            try
            {
                var batch = _mapper.Map<BookViewModel>(_repo.GetByID(id));

                return Ok(batch);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, $"ERROR: Information could not be retrieved for BookID: {id}");
            }

            return BadRequest("Internal Server Error: Could not retrieve information for requested book");
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post([FromBody]BookViewModel record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var book = _mapper.Map<Book>(record);
                // TODO: Add insert logic here
                book.CreatedBy = User.Identity.Name;
                book.CreatedDate = DateTime.UtcNow;
                book.Timestamp = DateTime.UtcNow;

                var id = _repo.Add(book);

                _logger.LogInformation(LoggingEvents.Critical, $"Added BookID: {id} - Title:{record.Title}", id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, $"ERROR: Could not add book: {record.Title}");
            }

            return BadRequest("Book information could not be saved");
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BookViewModel record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, $"ERROR: Could not update BatchID: {record.ID}");
            }

            return BadRequest("Could not update batch");
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid record id");
            }

            try
            {
                // TODO: Add delete logic here

                return Ok(_repo.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "Error deleting FTP connnection:{0}", id);

            }
            return BadRequest($"Book ID:{id} could not be deleted");
        }
    }
}

using Library_Management_System.DataAccessLayer.Interfaces;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repo;

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {
            BookResponse response = new BookResponse();
            try
            {
                response = await _repo.GetAllRecord();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs:" + ex.Message;
            }
            return Ok(response);
        }


        [HttpGet("{id}")]
            public async Task<IActionResult> GetRecordById(int id)
        {
            var response = await _repo.GetRecordById(id);

            if (!response.IsSuccess || response.SingleData == null)
            {
                return NotFound(new { message = "Id not found" });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(Book request)
        {
            BookResponse response = new BookResponse();
            try
            {
                response = await _repo.InsertRecord(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs:" + ex.Message;
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecordById(int id, Book request)
        {
            BookResponse response = new BookResponse();
            try
            {
                request.Id = id;
                response = await _repo.UpdateRecordById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs:" + ex.Message;
            }
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordById(int id)
        {
            {
                BookResponse response = new BookResponse();

                try
                {
                    response = await _repo.DeleteRecordById(id);

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Exception Occurs:" + ex.Message;
                }
                return Ok(response);

            }
        }
    }

}


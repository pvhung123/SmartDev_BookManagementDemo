using BookService.Application.Contracts;
using BookService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookAppService _bookAppService;

        public BookController(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="input">Data model used to create new Book</param>
        /// <response code="200">Book created successfully</response>
        /// <response code="400">Data model invalid</response>
        /// <response code="500">There is an error with internal server</response>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBookDto input)
        {
            if (input == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            }

            try
            {
                var result = await _bookAppService.CreateAsync(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }
        /// <summary>
        /// Get a list books
        /// </summary>
        /// <response code="200">Get a list books successfully</response>                
        /// <response code="500">There is an error with internal server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _bookAppService.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a book
        /// </summary>
        /// <response code="200">Get a book successfully</response>                
        /// <response code="500">There is an error with internal server</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _bookAppService.GetAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Search Book
        /// </summary>
        /// <response code="200">Find books successfully</response>                
        /// <response code="500">There is an error with internal server</response>
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooksAsync([FromQuery] BookSearchDto bookSearchDto)
        {
            if (bookSearchDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            }

            try
            {
                var result = await _bookAppService.SearchBooksAsync(bookSearchDto.Title);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

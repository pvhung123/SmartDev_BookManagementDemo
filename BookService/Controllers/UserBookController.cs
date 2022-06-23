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
    public class UserBookController : ControllerBase
    {
        private readonly IUserBookAppService _userBookAppService;

        public UserBookController(IUserBookAppService userBookAppService)
        {
            _userBookAppService = userBookAppService;
        }

        /// <summary>
        /// Add a book for user
        /// </summary>
        /// <param name="input">Data model used to create new UserBook</param>
        /// <response code="200">UserBook created successfully</response>
        /// <response code="400">Data model invalid</response>
        /// <response code="500">There is an error with internal server</response>
        [HttpPost]
        public async Task<IActionResult> AddUserBookAsync(CreateUserBookDto input)
        {
            if (input == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad Request");
            }

            try
            {
                var result = await _userBookAppService.AddUserBookAsync(input);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get a list books of a user
        /// </summary>
        /// <response code="200">Get a list books successfully</response>                
        /// <response code="500">There is an error with internal server</response>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBooksAsync(int userId)
        {
            try
            {
                var result = await _userBookAppService.GetBooksByUserIdAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a list books completed reading
        /// </summary>
        /// <response code="200">Get a list books completed reading successfully</response>                
        /// <response code="500">There is an error with internal server</response>
        [HttpGet("completed/{userId}")]
        public async Task<IActionResult> GetBooksCompletedAsync(int userId)
        {
            try
            {
                var result = await _userBookAppService.GetBooksCompletedAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
    }
}

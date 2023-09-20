using SalesAndPurchase.Server.Application.Features.Categories.Commands;
using SalesAndPurchase.Shared.Dtos.Response.Base;

namespace SalesAndPurchase.Server.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CategoryController : BaseApiController<CategoryController>
    {
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        // GET: api/<CategoryController>
        // <All Category> => For Admin
        [HttpGet]
        public async ValueTask<ActionResult> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Category");

            var items = await Mediator.Send(new GetCategoriesQuery(), cancellationToken);
            var res = new BaseListDataResponse<CategoryResponse>
            {
                Success = true,
                Message = "Success Get Data",
                Data = items,
                Count = items == null ? 0 : items.Count()
            };
            return Ok(res);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async ValueTask<ActionResult> Get(string id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Category by Id");
            var items = await Mediator.Send(new GetCategoryByIdQuery() { Id = id }, cancellationToken);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post(AddCategoryCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Name must be filled" });
            }

            return Ok(await Mediator.Send(command, cancellationToken));
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> Put(string id, [FromBody] EditCategoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Id must equal with body" });
            }
            if (string.IsNullOrEmpty(command.Name))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Name must be filled" });
            }
            try
            {
                var res = await Mediator.Send(command, cancellationToken);
                if (res == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed update data {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed update data");
            }

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Id must be filled" });
            }
            var res = await Mediator.Send(new DeleteCategoryCommand() { Id = id }, cancellationToken);
            if (!res)
                return NotFound();

            return NoContent();
        }
    }
}

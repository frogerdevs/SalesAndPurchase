namespace SalesAndPurchase.Server.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CustomerController : BaseApiController<CustomerController>
    {
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        // GET: api/<CutomerController>
        // <All Category> => For Admin
        [HttpGet]
        public async ValueTask<ActionResult> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get");

            var items = await Mediator.Send(new GetCustomersQueries(), cancellationToken);
            return Ok(items);
        }

        // GET api/<CutomerController>/5
        [HttpGet("{id}")]
        public async ValueTask<ActionResult> Get(string id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get by Id");
            var items = await Mediator.Send(new GetCustomerByIdQuery() { Id = id }, cancellationToken);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // POST api/<CutomerController>
        [HttpPost]
        public async Task<IActionResult> Post(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Name must be filled" });
            }

            return Ok(await Mediator.Send(command, cancellationToken));
        }

        // PUT api/<CutomerController>/5
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> Put(string id, [FromBody] EditCustomerCommand command, CancellationToken cancellationToken)
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

        // DELETE api/<CutomerController>/5
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Id must be filled" });
            }
            var res = await Mediator.Send(new DeleteCustomerCommand() { Id = id }, cancellationToken);
            if (!res)
                return NotFound();

            return NoContent();
        }
    }

}

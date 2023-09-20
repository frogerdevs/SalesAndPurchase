namespace SalesAndPurchase.Server.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SupplierController : BaseApiController<SupplierController>
    {
        private readonly ILogger<SupplierController> _logger;
        public SupplierController(ILogger<SupplierController> logger)
        {
            _logger = logger;
        }
        // GET: api/<SupplierController>
        [HttpGet]
        public async ValueTask<ActionResult> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get");

            var items = await Mediator.Send(new GetSuppliersQuery(), cancellationToken);
            return Ok(items);
        }

        // GET api/<SupplierController>/5
        [HttpGet("{id}")]
        public async ValueTask<ActionResult> Get(string id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get by Id");
            var items = await Mediator.Send(new GetSupplierByIdQuery() { Id = id }, cancellationToken);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // POST api/<SupplierController>
        [HttpPost]
        public async Task<IActionResult> Post(AddSupplierCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Name must be filled" });
            }

            return Ok(await Mediator.Send(command, cancellationToken));
        }

        // PUT api/<SupplierController>/5
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> Put(string id, [FromBody] EditSupplierCommand command, CancellationToken cancellationToken)
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

        // DELETE api/<SupplierController>/5
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "Id must be filled" });
            }
            var res = await Mediator.Send(new DeleteSuplierCommand() { Id = id }, cancellationToken);
            if (!res)
                return NotFound();

            return NoContent();
        }
    }

}

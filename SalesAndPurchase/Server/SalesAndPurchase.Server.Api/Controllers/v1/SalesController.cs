namespace SalesAndPurchase.Server.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SalesController : BaseApiController<PurchaseController>
    {
        private readonly ILogger<SalesController> _logger;
        public SalesController(ILogger<SalesController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddSellCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.SupplierId))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "SupplierId must be filled" });
            }

            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}

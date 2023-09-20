namespace SalesAndPurchase.Server.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PurchaseController : BaseApiController<PurchaseController>
    {
        private readonly ILogger<PurchaseController> _logger;
        public PurchaseController(ILogger<PurchaseController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddPurchaseCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.SupplierId))
            {
                return BadRequest(new BaseResponse { Success = false, Message = "SupplierId must be filled" });
            }

            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}

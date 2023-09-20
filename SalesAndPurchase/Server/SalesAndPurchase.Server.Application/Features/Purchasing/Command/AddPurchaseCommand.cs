namespace SalesAndPurchase.Server.Application.Features.Purchasing.Command
{
    public partial class AddPurchaseCommand : PurchaseRequest, ICommand<PurchaseResponse?>
    {
    }

    public sealed class AddPurchaseCommandHandler : ICommandHandler<AddPurchaseCommand, PurchaseResponse?>
    {
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        private readonly IBaseRepositoryAsync<Purchase, string> _repo;
        private readonly IBaseRepositoryAsync<Product, string> _repoProduct;
        public AddPurchaseCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Purchase, string>();
            _repoProduct = _genericUnitOfWork.Repository<Product, string>();
        }

        public async ValueTask<PurchaseResponse?> Handle(AddPurchaseCommand command, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(command);
            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                var res = await _repo.AddAsync(entity, cancellationToken);
                foreach (var item in command.PurchaseDetails)
                {
                    var product = await _repoProduct.GetByIdAsync(item.ProductId, cancellationToken);
                    if (product != null)
                    {
                        product.Stock += item.Quantity;
                        await _repoProduct.UpdateAsync(product, item.ProductId, cancellationToken);
                    }
                }
                await _genericUnitOfWork.Commit(cancellationToken);
                return await MapToResponse(res);
            }
            catch (Exception)
            {
                await _genericUnitOfWork.Rollback();
                return null;
            }
        }

        private static ValueTask<PurchaseResponse> MapToResponse(Purchase res)
        {
            return new ValueTask<PurchaseResponse>(new PurchaseResponse
            {
                Id=res.Id,
                SupplierId=res.SupplierId,
                SkuCode=res.SkuCode,
                PurchaseDate=res.PurchaseDate,
                SubTotal=res.SubTotal,
                Discount=res.Discount,
                Tax=res.Tax,
                Total=res.Total,
                TotalPriceRemaining=res.TotalPriceRemaining,
                Status=res.Status
            });
        }

        private static Purchase MapToEntity(AddPurchaseCommand command)
        {
            var entity = new Purchase
            {
                SupplierId=command.SupplierId,
                SkuCode=command.SkuCode,
                PurchaseDate=command.PurchaseDate??DateTime.UtcNow,
                SubTotal=command.SubTotal,
                Discount=command.Discount,
                Tax=command.Tax,
                Total=command.Total,
                TotalPriceRemaining=command.TotalPriceRemaining,
                Status=command.Status
            };
            entity.Purchases = MapToPurchaseDetail(entity.Id, command.PurchaseDetails);
            return entity;
        }
        private static ICollection<PurchaseDetail>? MapToPurchaseDetail(string purchaseId, IEnumerable<PurchaseDetailRequest>? res)
        {
            return res?.Select(re => new PurchaseDetail
            {
                PurchaseId=purchaseId,
                ProductId=re.ProductId,
                Quantity=re.Quantity,
                Price=re.Price
            }).ToList();
        }
    }

}

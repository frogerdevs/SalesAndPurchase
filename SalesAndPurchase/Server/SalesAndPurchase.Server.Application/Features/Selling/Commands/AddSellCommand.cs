namespace SalesAndPurchase.Server.Application.Features.Selling.Commands
{
    public partial class AddSellCommand : SellRequest, ICommand<SellResponse?>
    {
    }

    public sealed class AddSellCommandHandler : ICommandHandler<AddSellCommand, SellResponse?>
    {
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        private readonly IBaseRepositoryAsync<Sell, string> _repo;
        private readonly IBaseRepositoryAsync<Product, string> _repoProduct;
        public AddSellCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Sell, string>();
            _repoProduct = _genericUnitOfWork.Repository<Product, string>();
        }

        public async ValueTask<SellResponse?> Handle(AddSellCommand command, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(command);
            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                var res = await _repo.AddAsync(entity, cancellationToken);
                foreach (var item in command.SellDetails)
                {
                    var product = await _repoProduct.GetByIdAsync(item.ProductId, cancellationToken);
                    if (product != null)
                    {
                        product.Stock -= item.Quantity;
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

        private static ValueTask<SellResponse> MapToResponse(Sell res)
        {
            return new ValueTask<SellResponse>(new SellResponse
            {
                SupplierId=res.SupplierId,
                SkuCode=res.SkuCode,
                SalesDate=res.SalesDate??DateTime.UtcNow,
                SubTotal=res.SubTotal,
                Discount=res.Discount,
                Tax=res.Tax,
                Total=res.Total,
                TotalPriceRemaining=res.TotalPriceRemaining,
                Status=res.Status,
                SellDetails=res.SellDetails?.Select(resSellDetail => new SellDetailResponse
                {
                    Id=resSellDetail.Id,
                    SellId=resSellDetail.SellId,
                    ProductId=resSellDetail.ProductId,
                    Quantity=resSellDetail.Quantity,
                    Price=resSellDetail.Price
                })
            });
        }

        private static Sell MapToEntity(AddSellCommand command)
        {
            var entity = new Sell
            {
                SupplierId=command.SupplierId,
                SkuCode=command.SkuCode,
                SalesDate=command.SalesDate,
                SubTotal=command.SubTotal,
                Discount=command.Discount,
                Tax=command.Tax,
                Total=command.Total,
                TotalPriceRemaining=command.TotalPriceRemaining,
                Status=command.Status,
            };
            entity.SellDetails = MapToDetail(entity.Id, command.SellDetails);
            return entity;
        }
        private static ICollection<SellDetail>? MapToDetail(string sellId, IEnumerable<SellDetailRequest>? res)
        {
            return res?.Select(re => new SellDetail
            {
                SellId=sellId,
                ProductId=re.ProductId,
                Quantity=re.Quantity,
                Price=re.Price
            }).ToList();
        }
    }


}

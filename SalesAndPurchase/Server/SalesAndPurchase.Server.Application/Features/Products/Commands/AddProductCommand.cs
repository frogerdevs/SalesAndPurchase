namespace SalesAndPurchase.Server.Application.Features.Products.Commands
{
    public partial class AddProductCommand : ProductRequest, ICommand<ProductResponse?>
    {
    }

    public sealed class AddEditBrandCommandHandler : ICommandHandler<AddProductCommand, ProductResponse?>
    {
        private readonly IBaseRepositoryAsync<Product, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public AddEditBrandCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Product, string>();
        }

        public async ValueTask<ProductResponse?> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(command);
            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                var res = await _repo.AddAsync(entity, cancellationToken);
                await _genericUnitOfWork.Commit(cancellationToken);
                return await MapToResponse(res);
            }
            catch (Exception)
            {
                await _genericUnitOfWork.Rollback();
                return null;
            }
        }

        private static ValueTask<ProductResponse> MapToResponse(Product res)
        {
            return new ValueTask<ProductResponse>(new ProductResponse
            {
                Id=res.Id,
                Code=res.Code,
                Name=res.Name,
                Price=res.Price,
                Stock=res.Stock,
                Active=res.Active,
                CategoryId=res.CategoryId,
                SellPrice = res.SellPrice
            });
        }

        private static Product MapToEntity(AddProductCommand command)
        {
            return new Product
            {
                Code=command.Code,
                Name=command.Name,
                Price=command.Price,
                Stock=command.Stock,
                Active=command.Active,
                CategoryId=command.CategoryId,
                ImageUrl=command.ImageUrl,
                SellPrice = command.SellPrice
            };
        }
    }

}

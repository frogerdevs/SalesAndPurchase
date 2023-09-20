namespace SalesAndPurchase.Server.Application.Features.Products.Commands
{
    public partial class EditProductCommand : PutProductRequest, ICommand<ProductResponse?>
    {
    }

    public sealed class EditBrandCommandHandler : ICommandHandler<EditProductCommand, ProductResponse?>
    {
        private readonly IBaseRepositoryAsync<Product, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;

        public EditBrandCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Product, string>();
        }

        public async ValueTask<ProductResponse?> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                var entity = await _repo.GetByIdAsync(command.Id, cancellationToken);
                if (entity != null)
                {
                    entity.Code = command.Code;
                    entity.Name = command.Name ?? entity.Name;
                    entity.Price = command.Price;
                    entity.Stock = command.Stock;
                    entity.Active = command.Active;
                    entity.ImageUrl=command.ImageUrl;

                    var res = await _repo.UpdateAsync(entity, command.Id, cancellationToken);
                    await _genericUnitOfWork.Commit(cancellationToken);
                    return await MapToResponse(res);
                }
                return null;
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
                CategoryId=res.CategoryId
            });
        }
    }
}

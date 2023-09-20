using SalesAndPurchase.Server.Application.Interfaces.UnitOfWorks;

namespace SalesAndPurchase.Server.Application.Features.Categories.Commands
{
    public partial class AddCategoryCommand : CategoryRequest, ICommand<CategoryResponse?>
    {
    }

    public sealed class AddEditBrandCommandHandler : ICommandHandler<AddCategoryCommand, CategoryResponse?>
    {
        private readonly IBaseRepositoryAsync<Category, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public AddEditBrandCommandHandler(IBaseRepositoryAsync<Category, string> repo, IGenericUnitOfWork genericUnitOfWork)
        {
            _repo = repo;
            _genericUnitOfWork = genericUnitOfWork;
        }

        public async ValueTask<CategoryResponse?> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
        {
            var entity = MapToBrand(command);
            await _genericUnitOfWork.BeginTransactionAsync();

            var res = await _genericUnitOfWork.Repository<Category, string>().AddAsync(entity, cancellationToken);
            //var res = await _repo.AddAsync(entity, cancellationToken);

            try
            {
                await _genericUnitOfWork.Commit(cancellationToken);
                return await MapToResponse(res);
            }
            catch (Exception)
            {
                await _genericUnitOfWork.Rollback();
                return null;
            }
        }

        private static ValueTask<CategoryResponse> MapToResponse(Category res)
        {
            return new ValueTask<CategoryResponse>(new CategoryResponse
            {
                Id=res.Id,
                Name=res.Name,
                Description=res.Description,
                ImageUrl=res.ImageUrl,
                Active=res.Active
            });
        }

        private static Category MapToBrand(AddCategoryCommand command)
        {
            return new Category
            {
                Name=command.Name,
                Description=command.Description,
                ImageUrl=command.ImageUrl,
                Active=command.Active
            };
        }
    }
}

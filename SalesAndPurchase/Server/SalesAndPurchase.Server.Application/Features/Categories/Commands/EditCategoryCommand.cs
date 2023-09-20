namespace SalesAndPurchase.Server.Application.Features.Categories.Commands
{
    public partial class EditCategoryCommand : PutCategoryRequest, ICommand<CategoryResponse?>
    {
    }

    public sealed class EditBrandCommandHandler : ICommandHandler<EditCategoryCommand, CategoryResponse?>
    {
        private readonly IBaseRepositoryAsync<Category, string> _repo;

        public EditBrandCommandHandler(IBaseRepositoryAsync<Category, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<CategoryResponse?> Handle(EditCategoryCommand command, CancellationToken cancellationToken)
        {
            //var brand = MapToBrand(command);
            //var res = await _repo.AddAsync(entity, cancellationToken);


            //if (command.BrandId == 0)
            //{
            //    var brand = _mapper.Map<Brand>(command);
            //    await _unitOfWork.Repository<Brand>().AddAsync(brand);
            //    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
            //    return await Result<int>.SuccessAsync(brand.Id, _localizer["Brand Saved"]);
            //}
            //else
            //{
            var entity = await _repo.GetByIdAsync(command.Id, cancellationToken);
            if (entity != null)
            {
                entity.Name = command.Name ?? entity.Name;
                entity.Description = command.Description ?? entity.Description;
                entity.Active = command.Active;
                entity.ImageUrl=command.ImageUrl;

                var res = await _repo.UpdateAsync(entity, command.Id, cancellationToken);
                //await _unitOfWork.Repository<Brand>().UpdateAsync(entity);
                //await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBrandsCacheKey);
                //return await Result<int>.SuccessAsync(entity.Id, _localizer["Brand Updated"]);
                return await MapToResponse(res);
            }
            return null;
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
    }
}

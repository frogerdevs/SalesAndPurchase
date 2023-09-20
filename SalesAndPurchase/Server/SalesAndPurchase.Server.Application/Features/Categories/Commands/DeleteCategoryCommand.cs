﻿namespace SalesAndPurchase.Server.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public required string Id { get; set; }
    }
    public sealed class DeleteCategoryCommandhandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IBaseRepositoryAsync<Category, string> _repo;

        public DeleteCategoryCommandhandler(IBaseRepositoryAsync<Category, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the entity to be deleted from the database
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            // Remove the entity from the database
            entity.Active = false;
            await _repo.UpdateAsync(entity, request.Id, cancellationToken);
            ////await _repo.DeleteAsync(entity, cancellationToken);

            return true; // Return a completed Task
        }
    }
}

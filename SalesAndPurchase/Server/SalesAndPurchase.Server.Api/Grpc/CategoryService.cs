using Grpc.Core;
using GrpcStica;

namespace SalesAndPurchase.Server.Api.Grpc
{
    public class CategoryService : CategoryGrpc.CategoryGrpcBase
    {
        readonly IMediator _mediator;
        public CategoryService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GrpcCategoryResponse> GetItems(GrpcEmptyRequest request, ServerCallContext context)
        {
            var items = await _mediator.Send(new GetCategoriesQuery());
            var res = new GrpcCategoryResponse();

            res.Data.AddRange(items.Select(c => MapToRes(c)).ToList());
            context.Status = new Status(StatusCode.OK, $"Success");

            return res;
        }
        public override async Task<GrpcCategoryItemResponse> GetItemById(GrpcByIdRequest request, ServerCallContext context)
        {
            var item = await _mediator.Send(new GetCategoryByIdQuery() { Id = request.Id });
            if (item == null)
            {
                context.Status = new Status(StatusCode.NotFound, $"Data dengan Id :{request.Id} tidak ditemukan");
                return new GrpcCategoryItemResponse();
            }
            var res = MapToRes(item);
            context.Status = new Status(StatusCode.OK, $" Success");
            return res;
        }
        private static GrpcCategoryItemResponse MapToRes(CategoryResponse item)
        {
            return new GrpcCategoryItemResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                Active = item.Active
            };
        }

    }
}

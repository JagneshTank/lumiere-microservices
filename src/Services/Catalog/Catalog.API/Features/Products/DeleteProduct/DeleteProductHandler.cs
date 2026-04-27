using Catalog.API.Exceptions;
using Catalog.API.Features.Products.GetProductById;

namespace Catalog.API.Features.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandvalidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandvalidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required.");
        }
    }
    internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            // Now this is not required because we implemented this in globle
            //logger.LogInformation("DeleteProducthandle.Handle called with {@Command}", command);


            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}


using Catalog.API.Exceptions;
using Catalog.API.Features.Products.GetProducts;
using JasperFx.CodeGeneration.Frames;

namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("product Id is required");
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            product.Name = command.Name;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            product.Description = command.Description;
            product.Category = command.Category;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }

    }
}

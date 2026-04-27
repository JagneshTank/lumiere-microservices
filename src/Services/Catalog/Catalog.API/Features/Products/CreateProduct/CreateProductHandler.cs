



namespace Catalog.API.Features.Products.CreateProduct
{
    //public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : IRequest<CreateproductResult>;
    //public record CreateproductResult(Guid Id);

    //internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateproductResult>
    //{
    //    public Task<CreateproductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    //    {
    //        // Business logic to create a product 
    //        throw new NotImplementedException();
    //    }
    //}

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreatePrductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreatePrductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image File is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be grater than 0.");


        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Business logic to create a product 

            // This is validation before actual loding but this is not good way, good way is to use mediatR pipeline
            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            //if (errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            //Create product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,  
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            // Save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return CreateProductResult  
            return new CreateProductResult(product.Id);


            //throw new NotImplementedException();
        }
    }
}

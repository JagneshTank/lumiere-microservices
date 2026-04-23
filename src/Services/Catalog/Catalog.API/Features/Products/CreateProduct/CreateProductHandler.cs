

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

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Business logic to create a product 

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

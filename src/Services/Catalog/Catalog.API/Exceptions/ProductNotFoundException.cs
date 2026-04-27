using Building_Blocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("product", Id)
        {
            
        }
    }
}

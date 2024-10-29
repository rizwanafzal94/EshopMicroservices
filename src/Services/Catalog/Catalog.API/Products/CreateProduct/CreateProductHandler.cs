namespace Catalog.API.Products.CreateProduct
{
   
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
   : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product //Create Product entity from command object
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // Save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            // return CreateProductResult result
            //Business Logic to create a product

            return new CreateProductResult(product.Id);
        }
    }
}

using System;

namespace Core.Entities.OrderAgregate;

public class ProductsItemOrdered
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductUrl { get; set; }
}

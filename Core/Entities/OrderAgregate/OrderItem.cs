using System;

namespace Core.Entities.OrderAgregate;

public class OrderItem : BaseEntity
{
    public ProductsItemOrdered ItemOrdered { get; set; } = null!;
    public decimal Price {get; set;}
    public int Quantity { get; set; }
}

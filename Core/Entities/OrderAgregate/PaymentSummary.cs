using System;

namespace Core.Entities.OrderAgregate;

public class PaymentSummary
{
    public int Last4 { get; set; }
    public required string Brand { get; set; }
    public int ExpMonth { get; set; }
    public int year { get; set; }
}

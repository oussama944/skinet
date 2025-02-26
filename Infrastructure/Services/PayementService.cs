using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services;

public class PayementService(
    IConfiguration config,
    ICartService cartService,
    IGenericRepository<Core.Entities.Product> productRepo,
    IGenericRepository<DeliveryMethod> dmRepo) : IPaymentService
{
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
        StripeConfiguration.ApiKey = config["StripeSetting:SecretKey"];
        var cart = await cartService.GetCartAsync(cartId);

        if (cart == null) return null;
        var shippingPrice = 0m;

        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await dmRepo.GetByIdAsync((int)cart.DeliveryMethodId);
            if (deliveryMethod == null) return null;

            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in cart.Items)
        {
            var productItem = await productRepo.GetByIdAsync(item.ProductId);

            if (productItem == null) return null;

            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();
        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PaymentInternId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                    + (long)shippingPrice * 100,
                Currency = "eur",
                PaymentMethodTypes = ["card"]
            };

            intent = await service.CreateAsync(options);
            cart.PaymentInternId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                    + (long)shippingPrice * 100
            };
            intent = await service.UpdateAsync(cart.PaymentInternId, options);
        }
        await cartService.SetCartAsync(cart);

        return cart;
    }
}

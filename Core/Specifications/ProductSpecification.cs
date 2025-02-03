using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{

    public ProductSpecification(ProductSpecParams specParams) : base(x =>
    (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search))&&
    (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
    (!specParams.Types.Any() || specParams.Types.Contains(x.Type))
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderby(x => x.Price);
                break;
            case "priceDesc":
                AddOrderbyDesc(x => x.Price);
                break;
            default:
                AddOrderby(x => x.Name);
                break;
        }
    }

}

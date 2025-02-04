
using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit etre au dessus de 0")]
    public decimal Price { get; set; }
    [Required]
    public string PictureUrl { get; set; } = string.Empty;
    [Required]
    public string Type { get; set; } = string.Empty;
    [Required]
    public string Brand { get; set; } = string.Empty;
    [Range(0, int.MaxValue, ErrorMessage = "La quantité doit etre au dessus de  0")]
    public int QuantityInStock { get; set; }
}

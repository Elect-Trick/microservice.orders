using DataAccessLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(order => order.UserId).NotNull().NotEmpty().WithMessage("UserId is required");
        RuleFor(order => order.OrderItems).NotNull().NotEmpty().WithMessage("Order needs to have at least one product");
        RuleForEach(order => order.OrderItems)
            .Must(item => item.Quantity > 0).WithMessage("Quantity must be greater than 0")
            .Must(item => item.ProductId > 0).WithMessage("ProductId is required")
            .Must(item => item.ProductName != string.Empty).WithMessage("ProductName is required")
            .Must(item => item.ProductName != null ).WithMessage("ProductName is required")
            .Must(item => item.UnitPrice > 0 ).WithMessage("UnitPrice must be greater than 0");
        
        
    }
    
}
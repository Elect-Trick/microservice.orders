using DataAccessLayer.DTOs;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(order => order.UserId).NotNull().NotEmpty().WithMessage("UserId is required");
        RuleFor(order => order.OrderItems).NotNull().NotEmpty().WithMessage("Order needs to have at least one product");
        
        
    }
    
}
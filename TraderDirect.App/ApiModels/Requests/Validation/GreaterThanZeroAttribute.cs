using System.ComponentModel.DataAnnotations;

namespace TraderDirect.App.ApiModels.Requests.Validation;
public class GreaterThanZeroAttribute : ValidationAttribute
{
    public GreaterThanZeroAttribute()
        : base("The {0} field must be greater than zero.") { }

    public override bool IsValid(object? value)
    {
        if (value is double doubleValue)
        {
            return doubleValue > 0;
        }
        return false;
    }
}

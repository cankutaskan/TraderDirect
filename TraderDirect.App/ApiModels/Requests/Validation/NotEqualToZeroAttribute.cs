using System.ComponentModel.DataAnnotations;

namespace TraderDirect.App.ApiModels.Requests.Validation;
public class NotEqualToZeroAttribute : ValidationAttribute
{
    public NotEqualToZeroAttribute()
        : base("The {0} field cannot be zero.") { }

    public override bool IsValid(object? value)
    {
        if (value is double doubleValue)
        {
            return doubleValue != 0;
        }
        return false;
    }
}

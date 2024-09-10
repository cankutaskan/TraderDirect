using System.ComponentModel.DataAnnotations;

namespace TraderDirect.App.ApiModels.Requests
{
    public record CreateUserRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public required string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Closet.Web.Areas.Admin.Models.Users
{
    public class AddUserToRowFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AgriStreamIoT.Models
{
    public class Users
    {
        [Required, StringLength(50), RegularExpression(@"^[A-Za-z0-9_-]+$")]
        public string UserId { get; set; } = "";

        [Required, StringLength(50), RegularExpression(@"^[A-Za-z0-9_-]+$")]
        public string Username { get; set; } = "";

        [Required, StringLength(50), RegularExpression(@"^[A-Za-z0-9_-]+$")]
        public string Password { get; set; } = "";

        [Required, StringLength(50), RegularExpression(@"^[A-Za-z0-9_-]+$")]
        public string Role { get; set; } = "farmer";
    }

    public record SaveResult(Users user, bool Created);
}

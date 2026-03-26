using System.ComponentModel.DataAnnotations;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Request
{
    public class MemberRequestDtoReg
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public required string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).+")]
        public required string Password { get; set; }

    }

    public class MemberRequestDtoLog
    {
        // Dbl check validity of format just to save useless db request
        [Required]
        [EmailAddress]
        public required string EmailAddress { get; set; }

        [Required]
        public required string Password { get; set; }

    }
}

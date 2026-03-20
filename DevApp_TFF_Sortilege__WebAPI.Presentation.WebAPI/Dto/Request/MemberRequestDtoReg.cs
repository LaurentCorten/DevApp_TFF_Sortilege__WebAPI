using System.ComponentModel.DataAnnotations;

namespace DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Dto.Request
{
    public class MemberRequestDtoReg        // TODO : Question : Pourquoi passer par 2 dto different si name est nullable ?
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public required string EmailAddress { get; set; }

        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}")]
        public required string Password { get; set; }

    }

    public class MemberRequestDtoLog        // TODO : Question : Pourquoi on ne test pas la validité des pattern ? Ça pourrait éviter des requête inutiles, non ?
    {
        [Required]
        public required string EmailAddress { get; set; }

        [Required]
        public required string Password { get; set; }

    }
}

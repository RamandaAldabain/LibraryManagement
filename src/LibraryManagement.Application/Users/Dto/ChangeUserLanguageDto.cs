using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
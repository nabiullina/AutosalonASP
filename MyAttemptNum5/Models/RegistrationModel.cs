using System.ComponentModel.DataAnnotations;

namespace MyAttemptNum5.Models
{
    public class RegistrationModel
    {
        [Display(Name = "Логин: ")]
        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Display(Name = "Адрес электронной почты:")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [UIHint("Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля: ")]
        [UIHint("Password")]
        [Required]
        public string Password2 { get; set; }


        [Display(Name = "Согласен с условиями использования: ")]
        public bool Accept { get; set; }


    }
}

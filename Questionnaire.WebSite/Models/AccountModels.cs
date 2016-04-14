using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Questionnaire.WebSite.Models
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public String OldPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String NewPassword { get; set; }

        [DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public String ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "*")]
        public String Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public Boolean RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage="*")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        public String SecondName { get; set; }

        [Required(ErrorMessage = "*")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress, ErrorMessage="Некорректный адрес")]
        public String Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        public Int32 PostID { get; set; }

        [Required(ErrorMessage = "*")]
        public Int32 TypeID { get; set; }
    }
}

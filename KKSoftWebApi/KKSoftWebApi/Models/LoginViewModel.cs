using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKSoftWebApi.Models
{
    public class LoginViewModel
    {
        //校验条件
        //EF
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)] 
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }        
    }
}
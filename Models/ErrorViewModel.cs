using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NishuPortFolio.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class Contact
    {
        [Required]
        public string SName { get; set; }

        public string Message { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Address cannot exceed 50 characters.")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

    }

}

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
    public class AdminModelLogin
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public int userid { get; set; }
    }

    public class PortFolioAdd
    {
        [Required]
        public string portfoliodescription { get; set; }

        [Required]
        public string portfoliotitle { get; set; }

        public int portfolioid { get; set; }

        public List<PortFolioAdd> PortFolioList { get; set; }
    }

    public class DegreeAdd
    {
        [Required]
        public string DegreeInstitute { get; set; }

        [Required]
        public string DegreeTitle { get; set; }

        public int DegreeID { get; set; }

        public List<DegreeAdd> DegreeList { get; set; }
    }

}

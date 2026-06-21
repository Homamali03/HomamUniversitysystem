using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class UserDTO
    {
        public string EncryptUserId { get; set; }
        public int UserId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="This field is required")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateOnly DateOfBirth { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Mobile { get; set; }

        public string Address { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public bool Gender { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public int NationalityId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }
        public string GenderDisplayName {  get; set; }
        public string NationalityName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
       // public List<NationalityDTO> Nationalities { get; set; }
        //public IFormFile Image { get; set; }
        public string ImageFullPath { get; set; }
    }
}

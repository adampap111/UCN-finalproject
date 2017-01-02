using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.ViewModels
{
    public class ProfileViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Zip { get; set; }
        public string Street { get; set; }        
        public int StreetNumber { get; set; }
        public string City { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}

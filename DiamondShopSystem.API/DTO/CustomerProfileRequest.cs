using System.ComponentModel.DataAnnotations;

namespace DiamondShopSystem.API.DTO
{
    public class CustomerProfileRequest
    {
        public int customerId { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string state { get; set; }

        [Required(ErrorMessage = "Zipcode is required")]
        public string zipcode { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }

        public string phonenumber { get; set; }
    }
}

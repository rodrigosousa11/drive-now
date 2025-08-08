using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Display(Name = "License Plate")]
        [RegularExpression(@"^[A-Z0-9]{2}-[A-Z0-9]{2}-[A-Z0-9]{2}$", ErrorMessage = "License plate must be in the format XX-XX-XX.")]
        public string LicensePlate { get; set; }

        [Required]
        [Range(1900, 2025, ErrorMessage = "Manufacture year must be between 1900 and 2025.")]
        [Display(Name = "Manufacture Year")]
        public int ManufactureYear { get; set; }

        [Required]
        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; } = false;
    }
}
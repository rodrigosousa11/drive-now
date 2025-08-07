using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Manufacture year must be between 1900 and 2100.")]
        [Display(Name = "Manufacture Year")]
        public int ManufactureYear { get; set; }

        [Required]
        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; } = false;

        public int Horsepower { get; set; }
    }
}
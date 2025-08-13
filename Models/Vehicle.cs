using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public enum FuelType
    {
    Diesel,
    Gas,
    Electric
    }
    public class Vehicle : IValidatableObject
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
        [Range(1900, int.MaxValue, ErrorMessage = "Manufacture year must be between 1900 and the current year.")]
        [Display(Name = "Manufacture Year")]
        public int ManufactureYear { get; set; }

        [Required]
        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ManufactureYear > DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    $"Manufacture year cannot be greater than {DateTime.Now.Year}.",
                    new[] { nameof(ManufactureYear) });
            }
        }
    }
}

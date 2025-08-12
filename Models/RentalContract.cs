using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public class RentalContract : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Initial Mileage")]
        public int InitialMileage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Start date cannot be in the past.",
                    new[] { nameof(StartDate) });
            }

            if (EndDate.Date <= StartDate.Date)
            {
                yield return new ValidationResult(
                    "End date must be later than start date.",
                    new[] { nameof(EndDate) });
            }
        }
    }
}

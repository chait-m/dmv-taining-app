using System.ComponentModel.DataAnnotations;

namespace dmv_training_app.Models
{
    public class VehicleTitleModel
    {
        // Vehicle Info
        [Required(ErrorMessage = "VIN is required")]
        public string VIN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [Range(1965, 9999, ErrorMessage = "Year must be 1965 or later")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body Type is required")]
        public string BodyType { get; set; } = string.Empty;

        [Range(1, 499999, ErrorMessage = "Odometer must be between 1 and 499999")]
        public int OdometerReading { get; set; }

        // Owner Info
        [Required(ErrorMessage = "Owner Name is required")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner Address is required")]
        public string OwnerAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string OwnerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{3}[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Phone must be a valid US number (e.g. 555-555-5555)")]
        public string OwnerPhone { get; set; } = string.Empty;

        // Documents
        public byte[]? BillOfSale { get; set; }
        public byte[]? ProofOfInsurance { get; set; }
        public byte[]? ProofOfIdentity { get; set; }
        // Fee
        public decimal Fee { get; set; } = 100.00M; // Example static fee

        // Application Number
        public int ApplicationNumber { get; set; }
    }
}

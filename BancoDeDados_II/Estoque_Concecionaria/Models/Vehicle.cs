using System.ComponentModel.DataAnnotations.Schema;

namespace Estoque_Concecionaria.Models
{
    public class Vehicle
    {
        public int Id { get; set; }


        public int? ManufacturerId { get; set; }
        public int? ClassificationId { get; set; }
        public string? Model { get; set; }
        public string? LicensePlate { get; set; }
        public string? Color { get; set; }
        public int Odometer { get; set; }
        public string? Description { get; set; }
    }
}

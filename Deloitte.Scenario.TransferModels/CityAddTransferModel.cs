using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deloitte.Scenario.TransferModels
{
    public class CityAddTransferModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public string State { get; set; }

        [Range(1, 5, ErrorMessage = "TouristRating must be between {1} and {2}.")]
        [Required(ErrorMessage = "TouristRating is required")]
        public int TouristRating { get; set; }

        [Required(ErrorMessage = "TouristRating is required")]
        public DateTime DateEstablished { get; set; }

        [Required(ErrorMessage = "EstimatedPopulation is required")]
        [Range(1, long.MaxValue, ErrorMessage = "EstimatedPopulation must be between greater than zero.")]
        public long EstimatedPopulation { get; set; }
    }
}

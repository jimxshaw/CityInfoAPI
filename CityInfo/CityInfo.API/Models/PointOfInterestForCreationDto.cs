using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(55)]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "The Description cannot have more than 255 characters.")]
        public string Description { get; set; }

    }
}

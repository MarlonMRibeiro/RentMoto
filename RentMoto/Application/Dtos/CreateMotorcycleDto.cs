using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateMotorcycleDto
    {
        [Required]
        public string Model { get; set; }

        [Required]
        public string Identity { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Year { get; set; }
    }
}

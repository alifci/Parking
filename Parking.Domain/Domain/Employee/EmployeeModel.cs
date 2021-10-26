using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parking.Domain.Domain.Employee
{
    public class EmployeeModel
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Position { get; set; }
        [Required]
        [Range(minimum: 18, maximum: 150)]
        public int Age { get; set; }
    }
}

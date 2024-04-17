using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.DTO
{
    public class AddPatientToCenterDTO
    {
        public Guid CenterId { get; set; }
        public string? Manufacturer { get; set; }

        public DateTime DateTaken { get; set; }

        public Guid PatientId { get; set; }

    }
}

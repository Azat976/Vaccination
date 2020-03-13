using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFDataApp.Models
{
    /// <summary>
    /// класса колекции foreach
    /// </summary>
    public class PatientListViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }

        public IEnumerable<Vaccination> Vaccinations { get; set; }

    }
}

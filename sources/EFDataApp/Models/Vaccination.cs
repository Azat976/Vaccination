using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFDataApp.Models
{
    /// <summary>
    /// Класс вакцина
    /// </summary>
    public class Vaccination
    {
        /// <summary>
        /// Идентификатор вакцины
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Название вакцины
        /// </summary>
        public string Pils { get; set; }

        /// <summary>
        /// Наличие согласия на вакцинацию
        /// </summary>
        public Boolean HaveVaccination { get; set; }

        /// <summary>
        /// Дата вакцинации
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата вакцинации")]
        public DateTime VaccinationDate { get; set; }

        /// <summary>
        /// Индефикатор пациента, для внешнего ключа
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Пациент для навигации
        /// </summary>
        public Patient Patient { get; set; }

    }
}

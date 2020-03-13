using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFDataApp.Models
{
    /// <summary>
    /// Класс пациент
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Индефикатор пациента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия пациента
        /// </summary>
        [Required(ErrorMessage = "Не указана Фамилия")]
        public string Family { get; set; }

        /// <summary>
        /// Имя пациента
        /// </summary>
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime Birth { get; set; }

        /// <summary>
        /// Пол пациента
        /// </summary>
        [Required(ErrorMessage = "Не указан пол")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть М или Ж")]
        public string Gender { get; set; }

        /// <summary>
        /// Снилс пациента
        /// </summary>
        [Required(ErrorMessage = "Не указан снилс")]
        public string Snils { get; set; }

        /// <summary>
        /// Лист вакцин пациента
        /// </summary>
        public List<Vaccination> Vaccinations { get; set; }
        //public Vaccination Vaccination1 { get; set; }

        public Patient()
        {
            Vaccinations = new List<Vaccination>();
        }

    }
}

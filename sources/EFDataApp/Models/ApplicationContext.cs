using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFDataApp.Models
{
    /// <summary>
    /// Класс для взаимодействия с БД
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Сопоставление с пациентами, в таблице с БД
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Сопоставление вакцин
        /// </summary>
        public DbSet<Vaccination> Vaccinations { get; set; }

        /// <summary>
        /// Создание базы при её отсутсвии
        /// </summary>
        /// <param name="options"></param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
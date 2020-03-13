using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFDataApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EFDataApp.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за главную страницу,отображающий таблицу с пациентами
    /// </summary>
    public class PatientsController : Controller
    {
        /// <summary>
        /// Класс ответсвенный для взаимодействия с БД
        /// </summary>
        private ApplicationContext db;
        public PatientsController(ApplicationContext context)
        {

            db = context;

            if (db.Vaccinations.Count() == 0)
            {

                Patient Patient1 = new Patient { Family = "Васильев", Name = "Олег", Birth = DateTime.Parse("1999-07-01"), Gender = "Муж", Snils = "785675" };
                Patient Patient2 = new Patient { Family = "Овсов", Name = "Александр ", Birth = DateTime.Parse("1998-09-26"), Gender = "Муж", Snils = "22222" };
                Patient Patient3 = new Patient { Family = "Петрова", Name = "Александра ", Birth = DateTime.Parse("1988-01-01"), Gender = "Жен", Snils = "6575" };
                Patient Patient4 = new Patient { Family = "Иванов", Name = "Иван ", Birth = DateTime.Parse("2001-04-11"), Gender = "Муж", Snils = "16324" };
                Patient Patient5 = new Patient { Family = "Смагина", Name = "Евгения ", Birth = DateTime.Parse("1998-12-30"), Gender = "Жен", Snils = "366886" };


                Vaccination Egeriks1 = new Vaccination { Pils = "Эджерикс", HaveVaccination = true, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient1 };
                Vaccination Viavanak1 = new Vaccination { Pils = " Вианвак", HaveVaccination = true, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient1 };
                Vaccination AKDS1 = new Vaccination { Pils = "АКДС", HaveVaccination = false, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient1 };
                Vaccination BCJ1 = new Vaccination { Pils = "БЦЖ", HaveVaccination = false, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient1 };

                Vaccination Egeriks2 = new Vaccination { Pils = "Эджерикс", HaveVaccination = true, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient2 };
                Vaccination Viavanak2 = new Vaccination { Pils = " Вианвак", HaveVaccination = true, VaccinationDate = DateTime.Parse("2004-07-14"), Patient = Patient2 };
                Vaccination AKDS2 = new Vaccination { Pils = "АКДС", HaveVaccination = true, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient2 };
                Vaccination BCJ2 = new Vaccination { Pils = "БЦЖ", HaveVaccination = true, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient2 };

                Vaccination Egeriks3 = new Vaccination { Pils = "Эджерикс", HaveVaccination = false, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient3 };
                Vaccination Viavanak3 = new Vaccination { Pils = " Вианвак", HaveVaccination = false, VaccinationDate = DateTime.Parse("2011-11-29"), Patient = Patient3 };
                Vaccination AKDS3 = new Vaccination { Pils = "АКДС", HaveVaccination = false, VaccinationDate = DateTime.Parse("2013-11-29"), Patient = Patient3 };
                Vaccination BCJ3 = new Vaccination { Pils = "БЦЖ", HaveVaccination = false, VaccinationDate = DateTime.Parse("2001-02-20"), Patient = Patient3 };

                Vaccination Egeriks4 = new Vaccination { Pils = "Эджерикс", HaveVaccination = false, VaccinationDate = DateTime.Parse("2005-11-29"), Patient = Patient4 };
                Vaccination Viavanak4 = new Vaccination { Pils = " Вианвак", HaveVaccination = true, VaccinationDate = DateTime.Parse("2007-12-19"), Patient = Patient4 };
                Vaccination AKDS4 = new Vaccination { Pils = "АКДС", HaveVaccination = false, VaccinationDate = DateTime.Parse("2008-01-29"), Patient = Patient4 };
                /*            Vaccination BCJ4 = new Vaccination { Pils = "БЦЖ", HaveVaccination = false, Patient = Patient1 }*/
                ;
                db.Vaccinations.AddRange(Egeriks1, Viavanak1, AKDS1, BCJ1, Egeriks2, Viavanak2, AKDS2, BCJ2, Egeriks3, Viavanak3, AKDS3, BCJ3, Egeriks4, Viavanak4, AKDS4);
                db.Patients.AddRange(Patient1, Patient2, Patient3, Patient4, Patient5);
                db.SaveChanges();
            }

        }

        /// <summary>
        /// Метод возвращающий представление нового пациента
        /// </summary>
        /// <returns>
        /// Представление новго пациента
        /// </returns>
        public IActionResult Create()
        {

            return View();
        }
        /// <summary>
        /// Метод добавления пациента в БД
        /// </summary>
        /// <param name="Patient">Параметры пациента</param>
        /// <returns>
        /// Представление о новом пациенте на главной станице
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(Patient Patient)
        {
            db.Patients.Add(Patient);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод представления о пациенте
        /// </summary>
        /// <param name="id">Индефикатор выбранного пациента</param>
        /// <returns>Представление с информацией о пациенте
        /// Или возвращает сообщение об ошибке NotFound при отсутсвии пациента
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await db.Patients.Include(s => s.Vaccinations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        /// <summary>
        /// Метод представления редактирования пациента
        /// </summary>
        /// <param name="id">Индефикатор выбранного пациента</param>
        /// <returns>
        /// Возвращение представления редактировая пациента
        /// Или возвращает сообщение об ошибке NotFound при отсутсвии пациента
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Patient Patient = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (Patient != null)
                    return View(Patient);
            }
            return NotFound();
        }

        /// <summary>
        /// Метод редактирования пациента
        /// </summary>
        /// <param name="Patient">Параметры пациента</param>
        /// <returns>
        /// Результат изменения пациента, на странице Index
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Patient Patient)
        {
            db.Patients.Update(Patient);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Метод представления пациента, которого пользователь может удалить
        /// </summary>
        /// <param name="id">Индефикатор выбранного пациента</param>
        /// <returns>
        /// Возвращает предаставление пациента, которые пользователь может удалить
        /// Или возвращает сообщение об ошибке NotFound при отсутсвии пациента
        /// </returns>
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Patient Patient = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (Patient != null)
                    return View(Patient);
            }
            return NotFound();
        }

        /// <summary>
        /// Метод получающий удаленного пациента и удаляет его
        /// </summary>
        /// <param name="id">Индефикатор выбранного пациента</param>
        /// <returns>
        /// Переадресация на станицу Index с результатом удаления
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Patient Patient = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (Patient != null)
                {
                    db.Patients.Remove(Patient);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        /// <summary>
        /// Метод представления списка пациентов, с результатми поиска
        /// </summary>
        /// <param name="Vaccination">Параметры вакцинации</param>
        /// <param name="searchString">Строка с поиском пациента по Имени, фамили и снилс</param>
        /// <returns>
        /// Представление списка пациентов
        /// </returns>
        public IActionResult Index(int? Vaccination, string searchString)
        {
            IQueryable<Patient> Patients = db.Patients.Include(p => p.Vaccinations);
            if (Vaccination != null && Vaccination != 0)
            {
                Patients = Patients.Where(p => p.Id == Vaccination);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                Patients = Patients.Where(p => p.Name.Contains(searchString) || p.Family.Contains(searchString) || p.Snils.Contains(searchString));
            }
            List<Vaccination> vaccinations = db.Vaccinations.ToList();

            vaccinations.Insert(0, new Vaccination { Name = "Все", Id = 0 });

            PatientListViewModel viewModel = new PatientListViewModel
            {
                Patients = Patients.ToList(),
            };

            return View(viewModel);
        }
    }
}
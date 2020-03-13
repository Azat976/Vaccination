using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFDataApp.Models;

namespace EFDataApp.Controllers
{
    /// <summary>
    /// Контроллер отвечающий за редактирование, удаление и добавление вакцины для пациента (Метод Index, остался для доработки формы с вакцинами, при необходимости)
    /// </summary>
    public class VaccinationsController : Controller
    {
        private readonly ApplicationContext _context;

        public VaccinationsController(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод представления всех вакцин(не используется)
        /// </summary>
        /// <returns></returns>
        // GET: VaccinationsTTT
        public async Task<IActionResult> Index()
        {
            var vaccinations = _context.Vaccinations
                .Include(c => c.Patient)
                .AsNoTracking();
            return View(await vaccinations.ToListAsync());
        }
        /// <summary>
        /// Метод вывода представления информации о вакцине
        /// </summary>
        /// <param name="id">Индефекатор вакцины</param>
        /// <returns>Представление с информацией о вакцине</returns>
        // GET: VaccinationsTTT/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        /// <summary>
        /// Метод возвращает представления индефикаотр и имени пациента
        /// </summary>
        /// <returns>
        /// Индефикатор и имя пациента
        /// </returns>
        // GET: VaccinationsTTT/Create
        public IActionResult Create()
        {
            var patients = new SelectList(_context.Patients, "Id", "Name");
            ViewBag.Patients = patients;
            return View();
        }


        /// <summary>
        /// Метод представления добавления вакцины для пациента
        /// </summary>
        /// <param name="vaccination">Параметры вакцины</param>
        /// <returns>
        /// Представление с добавленной вакциной
        /// </returns>
        // POST: VaccinationsTTT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Pils,HaveVaccination,VaccinationDate, PatientId")] Vaccination vaccination)
        {

            if (ModelState.IsValid)
            {
                _context.Add(vaccination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccination);
        }


        /// <summary>
        /// Метод возвращает форму, с объектами вакцины, для редактирования
        /// </summary>
        /// <param name="id">Индефикатор вакцины</param>
        /// <returns>
        /// Представление вакцины для редактирования
        /// Или ошибка NotFound при отсутсвии вакцины
        /// </returns>
        // GET: VaccinationsTTT/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            return View(vaccination);
        }


        /// <summary>
        /// Метод передающий отредактированные данные вакцины
        /// </summary>
        /// <param name="id">Индефикатор вакцины</param>
        /// <param name="vaccination">Параметры вакцины</param>
        /// <returns>
        /// Представление с отредактированной вакциной
        /// </returns>
        // POST: VaccinationsTTT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Pils,HaveVaccination,VaccinationDate, PatientId")] Vaccination vaccination)
        {
            if (id != vaccination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationExists(vaccination.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaccination);
        }


        /// <summary>
        /// Метод представления удаляемого объекта вакцины
        /// </summary>
        /// <param name="id">Индефикатор вакцины</param>
        /// <returns>
        /// Представление удаляемой вакцины
        /// </returns>
        // GET: VaccinationsTTT/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }


        /// <summary>
        /// Метод удаляющий вакцину из БД
        /// </summary>
        /// <param name="id">Индефикаотр вакцины</param>
        /// <returns>
        /// Представление на странице Index (Содержится ошибка)
        /// </returns>
        // POST: VaccinationsTTT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccination = await _context.Vaccinations.FindAsync(id);
            _context.Vaccinations.Remove(vaccination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
            return _context.Vaccinations.Any(e => e.Id == id);
        }
    }
}

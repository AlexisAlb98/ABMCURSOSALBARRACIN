using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ABMCURSOSALBARRACIN.Data;
using ABMCURSOSALBARRACIN.Models;
using static ABMCURSOSALBARRACIN.Models.Curso;

namespace ABMCURSOSALBARRACIN.Controllers
{
    public class CursosController : Controller
    {
        private readonly AbmcursosContext _context;

        public CursosController(AbmcursosContext context)
        {
            _context = context;
        }

        private void PopulateDropDowns()
        {
            ViewBag.Dias = Enum.GetValues(typeof(DiaSemana)).Cast<DiaSemana>().Select(d => new SelectListItem
            {
                Value = d.ToString(),
                Text = d.ToString()
            }).ToList();

            ViewBag.Horarios = new List<SelectListItem>
            {
                new SelectListItem { Value = "18:00", Text = "18:00-19:00" },
                new SelectListItem { Value = "18:30", Text = "18:30-19:30" },
                new SelectListItem { Value = "19:00", Text = "19:00-20:00" },
                new SelectListItem { Value = "19:30", Text = "19:30-20:30" },
                new SelectListItem { Value = "20:00", Text = "20:00-21:00" },
                new SelectListItem { Value = "20:30", Text = "20:30-21:30" }

            };
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            return _context.Cursos != null ?
                        View(await _context.Cursos.ToListAsync()) :
                        Problem("Entity set 'AbmcursosContext.Cursos'  is null.");
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.IdCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        // POST: Cursos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCurso,NombreCurso,Profesor,CantidadAlumnos,Dia,Horario")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns();
            return View(curso);
        }

        // GET: Cursos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            PopulateDropDowns();
            return View(curso);
        }

        // POST: Cursos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCurso,NombreCurso,Profesor,CantidadAlumnos,Dia,Horario")] Curso curso)
        {
            if (id != curso.IdCurso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.IdCurso))
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
            PopulateDropDowns();
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.IdCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cursos == null)
            {
                return Problem("Entity set 'AbmcursosContext.Cursos'  is null.");
            }
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return (_context.Cursos?.Any(e => e.IdCurso == id)).GetValueOrDefault();
        }

        // GET: Cursos/AgregarAlumno/5
        public async Task<IActionResult> AgregarAlumno(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            // Verificar si se supera el límite de alumnos
            if (curso.CantidadAlumnos >= 50)
            {
                TempData["ErrorMessage"] = "No se puede agregar más alumnos. La cantidad máxima de alumnos ya se ha alcanzado.";
                return RedirectToAction(nameof(Index));
            }

            // Agregar un alumno al curso
            curso.CantidadAlumnos++;
            _context.Update(curso);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Cursos/EliminarAlumno/5
        public async Task<IActionResult> EliminarAlumno(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            // Verificar que haya al menos un alumno
            if (curso.CantidadAlumnos <= 0)
            {
                TempData["ErrorMessage"] = "No se puede eliminar más alumnos. No hay alumnos en este curso.";
                return RedirectToAction(nameof(Index));
            }

            // Eliminar un alumno del curso
            curso.CantidadAlumnos--;
            _context.Update(curso);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
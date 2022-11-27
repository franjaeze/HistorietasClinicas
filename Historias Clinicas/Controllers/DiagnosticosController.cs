﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Authorization;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class DiagnosticosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public DiagnosticosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Diagnosticos
        public IActionResult Index()
        {
            return View(_context.Diagnosticos.ToList());
        }

        // GET: Diagnosticos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = _context.Diagnosticos
                .FirstOrDefault(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            var diagnostico1 = _context.Diagnosticos.Find(id);
            ViewData["EpicrisisId"] = diagnostico1.EpicrisisId;

            return View(diagnostico);
        }

        // GET: Diagnosticos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnosticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id,EpicrisisId,Descripcion,Recomendacion,Especialidad")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                diagnostico.EpicrisisId = id;
                ViewData["EpicrisisId"] = diagnostico.EpicrisisId; 

                diagnostico.Id = 0;

                _context.Add(diagnostico);
                _context.SaveChanges();
                var epicrisis = _context.Epicrisis.Find(id);
                int numeroEp = epicrisis.EpisodioId;

                return RedirectToAction("DarAlta","Episodios", new {id = numeroEp });
            }
            return View(diagnostico);
        }

        // GET: Diagnosticos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = _context.Diagnosticos.Find(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            return View(diagnostico);
        }

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EpicrisisId,Descripcion,Recomendacion,Especialidad")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    diagnostico.EpicrisisId = id;
                    _context.Update(diagnostico);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                ViewData["EpicrisisId"] = diagnostico.EpicrisisId;
                return RedirectToAction("DiagnosticoPorEpicrisis", new { id = diagnostico.EpicrisisId });
            }
            return View(diagnostico);
        }

        // GET: Diagnosticos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = _context.Diagnosticos
                .FirstOrDefault(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var diagnostico = _context.Diagnosticos.Find(id);
            _context.Diagnosticos.Remove(diagnostico);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
            return _context.Diagnosticos.Any(e => e.Id == id);
        }

        public IActionResult DiagnosticoPorEpicrisis(int id)
        {
            var epicrisis = _context.Epicrisis.Find(id);

            var diagnostico = _context.Diagnosticos
                .Where(x => x.EpicrisisId == epicrisis.Id);

            ViewData["episodioId"] = epicrisis.EpisodioId;
            ViewData["EpicrisisId"] = epicrisis.Id;

            return View(diagnostico);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CierreAdministrativo(int id, int historiaClinicaId, [Bind("Id,EpicrisisId,Descripcion,Recomendacion,Especialidad")] Diagnostico diagnostico)
        {
            var episodioDb = _context.Episodios.Find(id);

            if (diagnostico == null)
            {
                return NotFound();
            }
            //diagnostico.EpicrisisId = id;
            //_context.Add(diagnostico);

            //_context.SaveChanges();

            //_context.Update(episodioDb);
            //_context.SaveChanges();

            return RedirectToAction("CargarDiagnostico","Episodio", new { id = historiaClinicaId });
        }
        public IActionResult CierreAdministrativo(int id, int historiaClinicaId)
        {
            @TempData["historiaId"] = historiaClinicaId;
            @TempData["EpisodioId"] = id;

            return View();
        }
    }
    }



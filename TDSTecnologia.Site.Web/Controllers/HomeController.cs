﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppContexto _context;

        public HomeController(AppContexto context)
        {
            _context = context;
        }
        /*public IActionResult Index()
        {
            return View();
        }*/

        public async Task<IActionResult> Index()
        {
            
            List<Curso> cursos = await _context.CursoDao.ToListAsync();

            cursos.ForEach(c =>
            {
                if (c.Banner != null)
                {
                    c.BannerBase64 = "data:image/png;base64," + Convert.ToBase64String(c.Banner, 0, c.Banner.Length);
                }
            });

            return View(await _context.CursoDao.ToListAsync());
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo([Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno")] Curso curso, IFormFile arquivo)            
        {

            if (arquivo != null && arquivo.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                await arquivo.OpenReadStream().CopyToAsync(ms);
                curso.Banner = ms.ToArray();
            }

            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.CursoDao.FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        public async Task<IActionResult> Alterar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.CursoDao.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar(int id, [Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(curso);
                _context.Entry<Curso>(curso).Property(c => c.Banner).IsModified = false;
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }


        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.CursoDao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var curso = await _context.CursoDao.FindAsync(id);
            _context.CursoDao.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

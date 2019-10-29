using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;
using TDSTecnologia.Site.Infrastructure.Repository;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;
using X.PagedList;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppContexto _context;

        private readonly CursoRespository _cursoRespository;

        private readonly CursoService _cursoService;

        /*public HomeController(AppContexto context)
        {
            _context = context;
        }
        */
        /*public IActionResult Index()
        {
            return View();
        }*/


        /*public HomeController(AppContexto context, CursoRespository cursoRespository)
        {
            _context = context;
            _cursoRespository = cursoRespository;
        }*/

        public HomeController(CursoService cursoService)
        {
            _cursoService = cursoService;
        }


        /*public async Task<IActionResult> Index()
        {
            
            //List<Curso> cursos = await _context.CursoDao.ToListAsync();
            List<Curso> cursos = await _cursoRespository.ListarTodos();

            return View(cursos);

            /*
             cursos.ForEach(c =>
            {
                if (c.Banner != null)
                {
                    c.BannerBase64 = "data:image/png;base64," + Convert.ToBase64String(c.Banner, 0, c.Banner.Length);
                }
            });

            return View(await _context.CursoDao.ToListAsync());
        
        }
        */


        public IActionResult Index(int? pagina)
        {
            //List<Curso> cursos = _cursoService.ListarTodos();

            IPagedList<Curso> cursos = _cursoService.ListarComPaginacao(pagina);
            var viewModel = new CursoViewModel
            {
                CursosComPaginacao = cursos
            };

            return View(viewModel);
        }

        
        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo([Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno,Modalidade,Nivel,Vagas")] Curso curso, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                curso.Banner = UtilImagem.ConverterParaByte(arquivo);
                _cursoService.Salvar(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        /*
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
        */
        /* public async Task<IActionResult> Detalhes(int? id)
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
         */

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        /*
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
        */

        /*
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
    */
        /*
            public IActionResult Alterar(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var curso = _cursoService.PesquisarPorId(id);

                if (curso == null)
                {
                    return NotFound();
                }
                return View(curso);
            }
            */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(int id, [Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno,Modalidade,Nivel,Vagas")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _cursoService.Atualizar(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        /*public async Task<IActionResult> Excluir(int? id)
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
        */


        public IActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        /*
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var curso = await _context.CursoDao.FindAsync(id);
            _context.CursoDao.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            _cursoService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PesquisarCurso(CursoViewModel pesquisa)
        {
            if (pesquisa.Texto != null && !String.IsNullOrEmpty(pesquisa.Texto))
            {
                List<Curso> cursos = _cursoService.PesquisarPorNomeDescricao(pesquisa.Texto);
                var viewModel = new CursoViewModel
                {
                    Cursos = cursos
                };
                return View("Index", viewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;

namespace TDSTecnologia.Site.Infrastructure.Repository
{
    public class CursoRespository : BasicRepository
    {
        public CursoRespository(AppContexto context) : base(context)
        {

        }


        /*
         public async Task<List<Curso>> ListarTodos()
        {
            List<Curso> cursos = await _context.CursoDao.ToListAsync();             

            return cursos;
        }
        */

        public List<Curso> ListarTodos()
        {
            return _context.CursoDao.ToList();
        }

        public Curso PesquisarPorId(int? id)
        {
            return _context.CursoDao.Find(id);
        }

        public void Salvar(Curso curso)
        {
            _context.Add(curso);
        }

        public void Atualizar(Curso curso)
        {
            _context.Update(curso);
            _context.Entry<Curso>(curso).Property(c => c.Banner).IsModified = false;
        }

        public void Excluir(Curso curso)
        {
            _context.CursoDao.Remove(curso);
        }
    }
}

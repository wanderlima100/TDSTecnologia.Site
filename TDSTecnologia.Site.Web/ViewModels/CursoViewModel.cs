﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using X.PagedList;

namespace TDSTecnologia.Site.Web.ViewModels
{
    public class CursoViewModel
    {
        public IPagedList<Curso> CursosComPaginacao { get; set; }
        public string Texto { get; internal set; }
        public List<Curso> Cursos { get; internal set; }
    }
}

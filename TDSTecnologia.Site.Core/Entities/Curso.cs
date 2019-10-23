﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TDSTecnologia.Site.Core.Entities
{
    [Table("tb01_curso")]
    public class Curso
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("quantidade_aula")]
        public int QuantidadeAula { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("banner")]
        public byte[] Banner { get; set; }

        [NotMapped]
        public string BannerBase64 { get; set; }

        [Column("turno")]
        public DomTurno Turno { get; set; }
    }
}

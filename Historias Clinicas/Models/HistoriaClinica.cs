﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class HistoriaClinica
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        public List<Episodio> Episodios { get; set; }
    }
}

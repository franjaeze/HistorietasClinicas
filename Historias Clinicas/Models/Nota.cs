﻿using Microsoft.VisualBasic;

namespace Historias_Clinicas.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public string MedicoID { get; set; }

        public string Mensaje { get; set; }

        public DateAndTime FechaYHora { get; set; }

    }
}

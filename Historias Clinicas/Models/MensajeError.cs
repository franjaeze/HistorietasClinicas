﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{ 
			public static class MensajeError{
		
			public const string Requerido = "El campo es obligatorio";
			public const string MinMaxString = "El campo {0} no se encuentra dentro de los rangos";
            public const string NumerosPositivos = "El campo {0} debe contener numeros positivos";
			public const string UnaOpcion = "Se debe registrar al menos una opcion";
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clase_12_Library_2; // Se agrega using

namespace Clase_12_Library
{
    public class Automovil : Vehiculo // Se agrega herencia
    {
        public Automovil(EMarca marca, string patente, ConsoleColor color)
            : base(patente, marca, color)
        {
        }
        /// <summary>
        /// Los automoviles tienen 4 ruedas
        /// </summary>
        public override short CantidadRuedas
        {
            get
            {
                return 4;
            }
            set { } // Se agrego set heredado
        }

        public override sealed string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("AUTOMOVIL");
            sb.AppendLine(base.Mostrar());
            sb.AppendFormat("RUEDAS : {0}", this.CantidadRuedas); // Se cambi AppendLine a AppendFormat
            sb.AppendLine("");
            sb.AppendLine("---------------------");

            return sb.ToString(); // Se agrego ToString()
        }
    }
}

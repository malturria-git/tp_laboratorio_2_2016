using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using Excepciones;
using Archivos;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesInstanciables
{
    [Serializable]
    public class Jornada
    {
        #region Atributos
        /// <summary>
        /// Atributos Instructor, Clase y Alumnos que toman dicha clase.
        /// </summary>
        private List<Alumno> _alumnos;
        private Gimnasio.EClases _clase;
        private Instructor _instructor;
        #endregion

        #region Propiedades
        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { this._alumnos = value; }
        }
        public Gimnasio.EClases Clases
        {
            get { return this._clase; }
            set { }
        }
        public Instructor Instructor
        {
            get { return this._instructor; }
            set { }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Se inicializará la lista de alumnos en el constructor por defecto.
        /// </summary>
        private Jornada()
        {
            this._alumnos = new List<Alumno>();
        }

        public Jornada(Gimnasio.EClases clase, Instructor instructor)
            : this()
        {
            this._clase = clase;
            this._instructor = instructor;
        }
        #endregion

        #region Sobrecargas

        public static bool operator ==(Jornada j, Alumno a)
        {
            bool retorno = false;
            if (!object.Equals(j, null) && !object.Equals(a, null))
            {
                if (a == j._clase)
                    retorno = true;
            }
            return retorno;
        }

        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }


        /// <summary>
        /// Agregar Alumnos a la clase por medio del operador +, validando que no estén previamente
        /// cargados.
        /// </summary>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            bool esta = false;

            for (int i = 0; i < j._alumnos.Count; i++)
            {
                if (a == j._alumnos[i])
                {
                    esta = true;
                    throw new AlumnoRepetidoException();
                }
            }

            if (!esta)
                j._alumnos.Add(a);

            return j;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("JORNADA:");
            sb.AppendLine("CLASE DE " + this._clase.ToString() + " POR " + this._instructor.ToString());
            sb.AppendLine("NACIONALIDAD: " + this._instructor.Nacionalidad.ToString());
            sb.AppendLine();
            sb.AppendLine("ALUMNOS: ");
            foreach (Alumno item in this._alumnos)
            {
                sb.AppendLine(item.ToString());
            }
            sb.AppendLine("<--------------------------------------------------->");

            return sb.ToString();
        }
        #endregion

        #region Salida a Texto
        public static bool Guardar(Jornada jornada)
        {
            Texto t = new Texto();
            return t.guardar(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Jornada.txt", jornada.ToString());
        }
        #endregion
    }
}
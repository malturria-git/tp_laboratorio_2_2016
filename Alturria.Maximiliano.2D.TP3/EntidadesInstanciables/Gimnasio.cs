using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using System.Xml;
using System.Xml.Serialization;
using Archivos;
using EntidadesAbstractas;

namespace EntidadesInstanciables
{
    [Serializable]

    public class Gimnasio
    {
        #region Enum EClases
        public enum EClases { CrossFit, Natacion, Pilates, Yoga }
        #endregion

        #region Atributos
        /// <summary>
        /// Atributos Alumnos (lista de inscriptos), Instructores (lista de quienes pueden dar clases) y Jornadas.
        /// </summary>
        private List<Alumno> _alumnos;
        private List<Instructor> _instructores;
        private List<Jornada> _jornadas;
        #endregion

        #region Propiedad
        public Jornada this[int i]
        {
            get
            {
                return this._jornadas[i];
            }
        }

        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { }
        }

        public List<Instructor> Instructores
        {
            get { return this._instructores; }
            set { }
        }

        public List<Jornada> Jornadas
        {
            get { return this._jornadas; }
            set { }
        }



        #endregion

        #region Constructor
        public Gimnasio()
        {
            this._alumnos = new List<Alumno>();
            this._instructores = new List<Instructor>();
            this._jornadas = new List<Jornada>();
        }
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Un Gimnasio será igual a un Alumno si el mismo está inscripto en él.
        /// </summary>
        public static bool operator ==(Gimnasio g, Alumno a)
        {
            bool retorno = false;
            if (!object.Equals(g, null) && !object.Equals(a, null))
            {
                foreach (Alumno item in g._alumnos)
                {
                    if (item == a)
                        retorno = true;
                }
            }
            return retorno;
        }

        public static bool operator !=(Gimnasio g, Alumno a)
        {
            return !(g == a);
        }

        /// <summary>
        /// Un Gimnasio será igual a un Instructor si el mismo está dando clases en él.
        /// </summary>
        public static bool operator ==(Gimnasio g, Instructor i)
        {
            bool retorno = false;
            if (!object.Equals(g, null) && !object.Equals(i, null))
            {
                for (int j = 0; j < g._instructores.Count; j++)
                {
                    if (g._instructores[j] == i)
                        retorno = true;
                }
            }
            return retorno;
        }

        public static bool operator !=(Gimnasio g, Instructor i)
        {
            return !(g == i);
        }

        /// <summary>
        /// Se agregarán Alumnos e Instructores mediante el operador +, validando que no estén previamente
        /// cargados.
        /// </summary>
        public static Gimnasio operator +(Gimnasio g, Alumno a)
        {
            foreach (var item in g._alumnos)
            {
                if (g == a)
                {
                    throw new AlumnoRepetidoException();
                }
            }

            g._alumnos.Add(a);
            return g;
        }

        /// <summary>
        /// Se agregarán Alumnos e Instructores mediante el operador +, validando que no estén previamente
        /// cargados.
        /// </summary>
        public static Gimnasio operator +(Gimnasio g, Instructor i)
        {
            foreach (var item in g._instructores)
            {
                if (item == i)
                {
                    throw new AlumnoRepetidoException();
                }
            }

            g._instructores.Add(i);
            return g;
        }

        /// <summary>
        /// Al agregar una clase a un Gimnasio se deberá generar y agregar una nueva Jornada indicando la
        /// clase, un Instructor que pueda darla (según su atributo ClasesDelDia) y la lista de alumnos que la
        /// toman (todos los que coincidan en su campo ClaseQueToma).
        /// </summary>
        public static Gimnasio operator +(Gimnasio g, Gimnasio.EClases clase)
        {
            Instructor i = null;
            bool hayInstructor = false;
            foreach (var ins in g._instructores)
            {
                if (ins == clase)
                {
                    i = ins;
                    hayInstructor = true;
                }
            }
            if (hayInstructor)
            {
                Jornada jornada = new Jornada(clase, i);
                foreach (Alumno alu in g._alumnos)
                {
                    if (alu == clase)
                        jornada += alu;
                }

                g._jornadas.Add(jornada);
            }
            else
            {
                throw new SinInstructorException();
            }

            return g;
        }

        #endregion

        #region MÉTODOS
        /// <summary>
        /// ToString hará públicos los datos del Alumno.
        /// Llama a MostrarDatos()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Gimnasio.MostrarDatos(this);
        }


        static string MostrarDatos(Gimnasio gim)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gim._jornadas.Count; i++)
            {
                sb.AppendLine(gim[i].ToString());
            }
            return sb.ToString();
        }
        #endregion

        #region Serializacion

        public static bool Guardar(Gimnasio gimnasio)
        {
            Xml<Gimnasio> xmlFile = new Xml<Gimnasio>();
            return xmlFile.guardar(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\gimnasio.xml", gimnasio);
        }

        /// <summary>
        /// Recupera los datos del Gimnasio de un archivo xml
        /// </summary>
        /// <returns>Gimnasio con los datos recuperados</returns>
        public static Gimnasio Leer()
        {
            try
            {
                Gimnasio g = new Gimnasio();
                Xml<Gimnasio> xml = new Xml<Gimnasio>();
                xml.leer(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\gimnasio.Xml", out g);
                return g;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}

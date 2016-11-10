using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesInstanciables
{
    [Serializable]
    public sealed class Instructor : PersonaGimnasio
    {
        #region Atributos
        /// <summary>
        /// Atributos ClasesDelDia del tipo Cola y random del tipo Random y estático.
        /// </summary>
        private Queue<Gimnasio.EClases> _clasesDelDia;
        private static Random _random;
        #endregion

        #region CONSTRUCTORES
        public Instructor() { }

        /// <summary>
        /// Se inicializará a random sólo en un constructor.
        /// </summary>
        static Instructor()
        {
            _random = new Random();
        }

        /// <summary>
        /// En el constructor de instancia se inicializará ClasesDelDia y se asignarán dos clases al azar al
        /// instructor mediante el método _randomClases. Las dos clases pueden o no ser la misma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Instructor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this._clasesDelDia = new Queue<Gimnasio.EClases>();
            _randomClases();
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Sobreescribirá el método MostrarDatos con todos los datos del alumno. (Error en el TP??? Deberia ser todos los datos del instructor)
        /// </summary>
        /// <returns></returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.MostrarDatos());
            sb.AppendLine(this.ParticiparEnClase());
            return sb.ToString();
        }

        /// <summary>
        /// ParticiparEnClase retornará la cadena "CLASES DEL DÍA " junto al nombre de la clases que da.
        /// </summary>
        /// <returns></returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CLASES DEL DIA: ");

            foreach (Gimnasio.EClases obj in this._clasesDelDia)
                sb.AppendLine(obj.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// ToString hará públicos los datos del Instructor.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }

        /// <summary>
        /// 
        /// </summary>
        private void _randomClases()
        {
            for (int i = 0; i < 2; i++)
            {
                this._clasesDelDia.Enqueue((Gimnasio.EClases)_random.Next(3));
            }
        }
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Un Instructor será igual a un EClase si da esa clase.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator ==(Instructor i, Gimnasio.EClases clase)
        {
            bool retorno = false;

            if (!object.Equals(i, null))
            {
                foreach (Gimnasio.EClases eClase in i._clasesDelDia)
                {
                    if (eClase == clase)
                        retorno = true;
                }
            }
            return retorno;
        }

        public static bool operator !=(Instructor i, Gimnasio.EClases clase)
        {
            return !(i == clase);
        }
        #endregion
    }
}

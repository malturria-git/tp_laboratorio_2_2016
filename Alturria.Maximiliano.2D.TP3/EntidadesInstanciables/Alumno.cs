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
    public sealed class Alumno : PersonaGimnasio
    {
        #region Enum EEstadoCuenta
        public enum EEstadoCuenta { MesPrueba, Deudor, AlDia }
        #endregion

        #region Atributos
        private Gimnasio.EClases _claseQueToma;
        private EEstadoCuenta _estadoCuenta;
        #endregion

        #region Constructores
        public Alumno() { }

        public Alumno(int id, string nombre, string apellido, string dni, Persona.ENacionalidad nacionalidad, Gimnasio.EClases claseQueToma)
            : base(id, nombre, apellido, dni, nacionalidad)
        {
            this._claseQueToma = claseQueToma;
        }

        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Gimnasio.EClases claseQueToma, EEstadoCuenta estadoCuenta)
            : this(id, nombre, apellido, dni, nacionalidad, claseQueToma)
        {
            this._estadoCuenta = estadoCuenta;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Sobreescribirá el método MostrarDatos con todos los datos del alumno.
        /// Muestra todos los datos del alumno, subiendo a la herencia PersonaGimnasio -> Persona.
        /// </summary>
        /// <returns></returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("ESTADO DE CUENTA: " + this._estadoCuenta.ToString());
            sb.AppendLine(this.ParticiparEnClase());
            return sb.ToString();
        }

        /// <summary>
        /// ParticiparEnClase retornará la cadena "TOMA CLASE DE " junto al nombre de la clase que toma.
        /// Metodo utilizado por MostrarDatos()
        /// </summary>
        /// <returns></returns>
        protected override string ParticiparEnClase()
        {
            return "TOMA CLASES DE " + this._claseQueToma.ToString();
        }

        /// <summary>
        /// ToString hará públicos los datos del Alumno.
        /// Llama a MostrarDatos()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }

        #endregion

        #region Sobrecargas
        /// <summary>
        /// Un Alumno será igual a un EClase si toma esa clase y su estado de cuenta no es Deudor.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator !=(Alumno a, Gimnasio.EClases clase)
        {
            return !(a == clase);
        }

        public static bool operator ==(Alumno a, Gimnasio.EClases clase)
        {
            if (!object.Equals(a, null))
            {
                return (a._estadoCuenta != EEstadoCuenta.Deudor && a._claseQueToma == clase);
            }
            else { return false; }
        }
        #endregion
    }
}

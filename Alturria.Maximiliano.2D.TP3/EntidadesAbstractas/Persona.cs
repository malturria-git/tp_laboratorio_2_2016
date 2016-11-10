using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions; // Se agrega para utilizar Regex en ValidarNombreApellido (https://msdn.microsoft.com/es-es/library/3y21t6y4(v=vs.110).aspx)
using Excepciones;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesAbstractas
{
    [Serializable]
    public abstract class Persona
    {
        #region Enumerado ENacionalidad
        public enum ENacionalidad { Argentino, Extranjero }
        #endregion

        #region Atributos
        private string _apellido;
        private int _dni;
        private ENacionalidad _nacionalidad;
        private string _nombre;
        #endregion Atributos

        #region Propiedades
        public int DNI
        {
            get { return _dni; }
            set { this._dni = ValidarDni(this.Nacionalidad, value); }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { this._nombre = ValidarNombreApellido(value); }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { this._apellido = ValidarNombreApellido(value); }
        }
        public ENacionalidad Nacionalidad
        {
            get { return _nacionalidad; }
            set { this._nacionalidad = value; }
        }
        public string StringToDNI
        {
            set
            {
                this._dni = ValidarDni(this.Nacionalidad, value);
            }
        }
        #endregion Propiedades

        #region Constructores
        /// <summary>
        /// Constructor agregado para serializar
        /// </summary>
        public Persona() { }

        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nacionalidad = nacionalidad;
        }

        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.DNI = dni;
        }

        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : this(nombre, apellido, nacionalidad)
        {
            this.StringToDNI = dni;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Valida que el dni sea coherente con la nacionalidad
        /// </summary>
        /// <param name="nacionalidad">Enumerado tipo ENacionalidad</param>
        /// <param name="dato">int que corresponde al dni</param>
        /// <returns></returns>
        private int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            if (nacionalidad == ENacionalidad.Argentino)
            {
                if ((dato >= 1) && (dato <= 89999999))
                {
                    return dato;
                }
            }
            else
            {
                if (dato >= 90000000)
                {
                    return dato;
                }
            }
            throw new NacionalidadInvalidaException();
        }

        /// <summary>
        /// Valida que el dni sea coherente con la nacionalidad, reutiliza el ValidarDni que recibe el dni como tipo int
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns></returns>
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int aux;
            if (Int32.TryParse(dato, out aux))
            {
                return ValidarDni(nacionalidad, aux);
            }
            else
            {
                throw new DniInvalidoException();
            }
        }


        /// <summary>
        /// Valida que tanto el nombre como el apellido sean solo caracteres
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        private string ValidarNombreApellido(string dato)
        {
            Regex reg = new Regex("^[A-Za-z]+$");
            if (reg.IsMatch(dato))
                return dato;
            else
                return "";
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("NOMBRE COMPLETO: " + this.Apellido + ", " + this.Nombre);
            sb.AppendLine("NACIONALIDAD: " + this.Nacionalidad.ToString());
            return sb.ToString();
        }
        #endregion
    }
}

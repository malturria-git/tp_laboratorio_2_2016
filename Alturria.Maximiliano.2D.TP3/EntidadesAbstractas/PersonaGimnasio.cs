using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EntidadesAbstractas
{
    [Serializable]
    public abstract class PersonaGimnasio : Persona
    {
        #region Atributos
        private int _identificador;
        #endregion

        #region Constructores
        public PersonaGimnasio()
        { }

        public PersonaGimnasio(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad)
            : base(nombre, apellido, dni, nacionalidad)
        {
            this._identificador = id;
        }

        #endregion

        #region Metodos
		/// <summary>
        /// Método protegido y virtual MostrarDatos retornará todos los datos de la PersonaGimnasio.
        /// </summary>
        /// <returns></returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("CARNET NUMERO: " + this._identificador);
            return sb.ToString();
        }

        /// <summary>
        /// Método protegido y abstracto ParticiparEnClase.
        /// </summary>
        /// <returns></returns>
        protected abstract string ParticiparEnClase();
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Dos PersonaGimnasio serán iguales si y sólo si son del mismo Tipo y su Id o DNI son iguales.
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns></returns>
        public static bool operator ==(PersonaGimnasio pg1, PersonaGimnasio pg2)
        {
            bool retorno = false;
            if (!(object.Equals(pg1, null)) && !(object.Equals(pg2, null)))
            {
                if (pg1.GetType() == pg2.GetType())
                {
                    if ((pg1._identificador == pg2._identificador) || (pg1.DNI == pg2.DNI))
                    {
                        retorno = true;
                    }
                }
            }

            return retorno;
        }

        public static bool operator !=(PersonaGimnasio pg1, PersonaGimnasio pg2)
        {
            return !(pg1 == pg2);
        }
        #endregion
    }
}

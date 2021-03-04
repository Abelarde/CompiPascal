using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.analizador.simbolo
{
    /// <summary>
    /// Diferentes tipos de datos permitidos en el lenguaje
    /// </summary>
    public enum Tipos
    {
        STRING = 0,
        INTEGER = 1,
        REAL = 2,
        BOOLEAN = 3,
        OBJECT = 4,
        ARRAY = 5,
        ERROR = 6
    }

    class Tipo
    {
        public Tipos tipo;
        public string tipoAuxiliar; //OBJECT, STRUCT, CLASES
       
        public Tipo(Tipos tipo, string tipoAuxiliar)
        {
            this.tipo = tipo;
            this.tipoAuxiliar = tipoAuxiliar;
        }

        public Tipo(string tipo)
        {
            Tipos tipoCasteado = Tipo.castearTipo(tipo);
            if (tipoCasteado != Tipos.OBJECT)
            {
                //tipos nativo
                this.tipo = tipoCasteado;
                this.tipoAuxiliar = null;
            }
            else
            {
                //tipo object
                this.tipo = Tipos.OBJECT;
                this.tipoAuxiliar = tipo;
            }
        }

        /// <summary>
        /// Obtiene el tipo de dato desde un string
        /// </summary>
        /// <param name="tipo">tipo de dato</param>
        /// <returns></returns>
        public static Tipos castearTipo(string tipo)
        {
            switch (tipo)
            {
                case "STRING":
                    return Tipos.STRING;
                case "INTEGER":
                    return Tipos.INTEGER;
                case "REAL":
                    return Tipos.REAL;
                case "BOOLEAN":
                    return Tipos.BOOLEAN;
                case "ARRAY":
                    return Tipos.ARRAY;
                case "ERROR":
                    return Tipos.ERROR;
                default:
                    return Tipos.OBJECT;

            }
        }
    }
}

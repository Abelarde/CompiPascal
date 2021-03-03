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
        REAL = 0,
        INTEGER = 1,
        BOOLEAN = 2,
        CHAR = 3,
        SUBRANGE = 4,
        ARRAY = 5,
        STRING = 6,
        OBJECT = 7,
        ERROR = 8
    }

    //TODO: pregunta: porque tenemos una clase Tipo y con un atributo tipoAuxiliar
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

        public static Tipos castearTipo(string tipo)
        {
            switch (tipo)
            {
                case "REAL":
                    return Tipos.REAL;
                case "INTEGER":
                    return Tipos.INTEGER;
                case "BOOLEAN":
                    return Tipos.BOOLEAN;
                case "CHAR":
                    return Tipos.CHAR;
                case "SUBRANGE":
                    return Tipos.SUBRANGE;
                case "ARRAY":
                    return Tipos.ARRAY;
                case "STRING":
                    return Tipos.STRING;
                case "ERROR"://cuando retorno este tipo error
                    return Tipos.ERROR;
                default:
                    return Tipos.OBJECT;

            }
        }
    }
}

using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.analizador.simbolo
{
    /// <summary>
    /// Diferentes tipos de datos permitidos en el lenguaje
    /// </summary>
    public enum Tipos
    {
        REAL = 0, 
        INTEGER = 1,
        BOOLEAN = 2,
        ARRAY = 3,
        STRING = 4, 
        OBJECT = 5,
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

        public Tipo(string tipo, Entorno entorno)
        {
            Tipos tipoCasteado = Tipo.castearTipo(tipo, entorno);
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
        public static Tipos castearTipo(string tipo, Entorno entorno)
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
                default://type, days=integer; Rectangle=object;
                    {
                        Simbolo type_id = entorno.getVariable(tipo);
                        if(type_id != null)
                        {
                            if (type_id.tipo.tipo != Tipos.OBJECT)
                            {
                                return type_id.tipo.tipo;
                            }
                            else
                            {
                                return Tipos.OBJECT;
                            }
                        }
                        return Tipos.OBJECT; //TODO: arreglar esto porque aqui deberia de ir null pero tendria que ir a arreglar las 
                        //validaciones donde lo utilizo
                    }

            }
        }
    }
}

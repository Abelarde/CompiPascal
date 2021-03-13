using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.analizador.simbolo
{
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
        public string tipoAuxiliar; //OBJECT, ARRAYS
       
        public Tipo(Tipos tipo, string tipoAuxiliar)//yo se el tipo y lo completo 
        {
            this.tipo = tipo;
            this.tipoAuxiliar = tipoAuxiliar;
        }

        public Tipo(string tipo, Entorno entorno)//no se el tipo
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
                default:
                    {   //viene un ID, obtengo el tipo
                        Simbolo variable = entorno.getVariable(tipo);
                        if(variable != null)
                        {
                            if (variable.tipo.tipo != Tipos.OBJECT)
                                return variable.tipo.tipo;
                            else
                                return Tipos.OBJECT;
                        }
                        return Tipos.OBJECT; 
                    }
            }
        }
    }
}

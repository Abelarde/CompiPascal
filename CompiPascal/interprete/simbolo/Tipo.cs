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
        ERROR = 7
            //OBJECT = 7
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
    }
}

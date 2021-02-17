using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.analizador.simbolo
{
    public enum Tipos
    {
        //TODO: ver todos los tipos
        INT = 0,
        BOOLEAN = 1,
        ERROR = 2
    }
    class Tipo
    {
        public Tipos tipo;
        public string tipoAuxiliar;

        public Tipo(Tipos tipo, string tipoAuxiliar)
        {
            this.tipo = tipo;
            this.tipoAuxiliar = tipoAuxiliar;
        }
    }
}

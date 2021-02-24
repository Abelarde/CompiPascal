using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ArrayCallInstruccion : Instruccion
    {
        private Expresion id;
        private Expresion posicion;

        public ArrayCallInstruccion(Expresion id, Expresion posicion)
        {
            this.id = id;
            this.posicion = posicion;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

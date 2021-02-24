using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class VariableInicializarInstruccion : Instruccion
    {
        private Expresion id;
        private Expresion valor;

        public VariableInicializarInstruccion(Expresion id, Expresion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

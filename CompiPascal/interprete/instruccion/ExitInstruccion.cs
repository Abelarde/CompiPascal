using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ExitInstruccion : Instruccion
    {
        private Expresion valor;
        public Simbolo resultado;

        public ExitInstruccion(Expresion valor)
        {
            this.valor = valor;
            resultado = null;
        }

        public override object ejecutar(Entorno entorno)
        {
            Simbolo resultado = valor.evaluar(entorno);
            this.resultado = resultado;
            return this;
        }
    }
}

using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ExitInstruccion : Instruccion
    {
        private Expresion valor;
        public Simbolo simbolResultado;

        public ExitInstruccion(Expresion valor)
        {
            this.valor = valor;
            simbolResultado = null;
        }

        public override object ejecutar(Entorno entorno)
        {
            Simbolo resultado = valor.evaluar(entorno);
            this.simbolResultado = resultado;
            return this;
        }
    }
}

using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Const : Instruccion
    {
        private string id;
        private Expresion valor;

        public Const(string id, Expresion valor)
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

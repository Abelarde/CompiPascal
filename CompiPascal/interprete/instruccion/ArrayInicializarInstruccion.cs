using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ArrayInicializarInstruccion : Instruccion
    {
        private Expresion id;
        private Expresion rango;
        private Expresion valor;

        public ArrayInicializarInstruccion(Expresion id, Expresion rango, Expresion valor)
        {
            this.id = id;
            this.rango = rango;
            this.valor = valor;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

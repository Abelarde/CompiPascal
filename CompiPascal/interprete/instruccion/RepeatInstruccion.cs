using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class RepeatInstruccion : Instruccion
    {
        private LinkedList<Instruccion> lista_instrucciones;
        private Expresion valor_condicional;

        public RepeatInstruccion(LinkedList<Instruccion> lista_instrucciones, Expresion valor_condicional)
        {
            this.lista_instrucciones = lista_instrucciones;
            this.valor_condicional = valor_condicional;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

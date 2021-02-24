using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class WhileInstruccion : Instruccion
    {
        private Expresion valor_condicional;
        private LinkedList<Instruccion> lista_instrucciones;


        public WhileInstruccion(Expresion valor_condicional, LinkedList<Instruccion> lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.lista_instrucciones = lista_instrucciones;

        }
        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

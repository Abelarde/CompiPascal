using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class FunctionCallInstruccion : Instruccion
    {
        private Expresion id;
        private LinkedList<Expresion> lista_expresiones;


        public FunctionCallInstruccion(Expresion id, LinkedList<Expresion> lista_expresiones)
        {
            this.id = id;
            this.lista_expresiones = lista_expresiones;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

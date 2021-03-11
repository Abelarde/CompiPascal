using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ProcedureCallinstruccion : Instruccion
    {
        private Expresion id;
        private LinkedList<Expresion> lista_expresiones;


        public ProcedureCallinstruccion(Expresion id, LinkedList<Expresion> lista_expresiones)
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

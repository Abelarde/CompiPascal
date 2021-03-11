using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ObjectInstruccion : Instruccion
    {
        private Expresion id;
        private LinkedList<Instruccion> lista_vars; //lista de vars new Vars();

        public ObjectInstruccion(Expresion id, LinkedList<Instruccion> lista_vars)
        {
            this.id = id;
            this.lista_vars = lista_vars;
        }




        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

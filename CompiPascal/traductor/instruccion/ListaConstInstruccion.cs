using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ListaConstInstruccion : Instruccion
    {
        LinkedList<Const> Const_lista;

        public ListaConstInstruccion(LinkedList<Const> Const_lista)
        {
            this.Const_lista = Const_lista;
        }

        public override object ejecutar(Entorno entorno)
        {
            foreach (Const cons in Const_lista)
            {
                if (cons != null)
                    cons.ejecutar(entorno);
            }
            return null;
        }
    }
}

using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ListaVarInstruccion : Instruccion
    {
        LinkedList<Var> Var_lista;

        public ListaVarInstruccion(LinkedList<Var> Var_lista)
        {
            this.Var_lista = Var_lista;
        }

        public override object ejecutar(Entorno entorno)
        {
            foreach (Var vars in Var_lista)
            {
                if (vars != null)
                    vars.ejecutar(entorno);
            }
            return null;
        }
    }
}

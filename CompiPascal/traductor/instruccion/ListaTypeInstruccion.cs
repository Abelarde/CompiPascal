using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ListaTypeInstruccion : Instruccion
    {
        LinkedList<TypeInstruccion> type_lista;

        public ListaTypeInstruccion(LinkedList<TypeInstruccion> type_lista)
        {
            this.type_lista = type_lista;
        }

        public override object ejecutar(Entorno entorno)
        {
            foreach (TypeInstruccion type in type_lista)
            {
                if (type != null)
                    type.ejecutar(entorno);
            }
            return null;
        }
    }
}

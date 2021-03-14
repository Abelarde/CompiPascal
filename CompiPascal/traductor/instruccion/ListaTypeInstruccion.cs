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

            string cadena = string.Empty;
            cadena += "Type" + Environment.NewLine;

            foreach (TypeInstruccion type in type_lista)
            {
                if (type != null)
                {
                    object resultado = type.ejecutar(entorno);
                    if (resultado != null)
                        cadena += Convert.ToString(resultado);
                }
            }
            return cadena;
        }
    }
}

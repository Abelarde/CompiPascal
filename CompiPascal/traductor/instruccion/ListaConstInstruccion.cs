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

            string cadena = string.Empty;
            cadena += "CONST" + Environment.NewLine;

            foreach (Const cons in Const_lista)
            {
                if (cons != null)
                {
                    object resultado = cons.ejecutar(entorno);
                    if (resultado != null)
                        cadena += Convert.ToString(resultado);
                }

            }
            return cadena;
        }
    }
}

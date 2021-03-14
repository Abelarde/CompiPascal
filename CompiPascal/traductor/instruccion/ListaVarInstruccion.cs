using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ListaVarInstruccion : Instruccion
    {
        public LinkedList<Var> Var_lista;


        public ListaVarInstruccion(LinkedList<Var> Var_lista)
        {
            this.Var_lista = Var_lista;
        }

        public override object ejecutar(Entorno entorno)
        {
            string cadena = string.Empty;
            cadena += "Var" + Environment.NewLine;

            foreach (Var vars in Var_lista)
            {
                if (vars != null)
                {
                    object resultado = vars.ejecutar(entorno);
                    if (resultado != null)
                        cadena += Convert.ToString(resultado);
                }
            }
            return cadena;
        }
    }
}

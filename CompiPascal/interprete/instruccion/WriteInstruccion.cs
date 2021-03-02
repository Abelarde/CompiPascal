using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class WriteInstruccion : Instruccion
    {
        private LinkedList<Expresion> valores;
        private bool newLine;

        public WriteInstruccion(LinkedList<Expresion> valores, bool newLine)
        {
            this.valores = valores;
            this.newLine = newLine;
            
        }

        public override object ejecutar(Entorno entorno)
        {
            string res = string.Empty;
            foreach (Expresion exp in valores)
            {//TODO: entorno, porque cuando sea una variable o algo asi...
                Simbolo resultado = exp.evaluar(entorno);
                res += resultado.valor.ToString();
            }

            if (newLine)
            {
                return res + "\n";
            }
            else
            {
                return res;
            }
        }
    }
}

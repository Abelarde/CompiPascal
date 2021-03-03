using CompiPascal.interprete.analizador;
using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class WriteInstruccion : Instruccion
    {
        private LinkedList<Expresion> valores;
        private bool newLine;

        Form1 form;

        public WriteInstruccion(LinkedList<Expresion> valores, bool newLine, Form1 formp)
        {
            this.valores = valores;
            this.newLine = newLine;
            form = formp;
            
        }

        public override object ejecutar(Entorno entorno)
        {
            string acumulado = string.Empty;

            try
            {
                foreach (Expresion exp in valores)
                {
                    Simbolo resultado = exp.evaluar(entorno);//TODO: entorno, porque cuando sea una variable o algo asi...
                    if (validaciones(resultado))
                    {
                        acumulado += resultado.valor.ToString();
                    }

                }

                if (newLine)
                    form.Write(acumulado + "\r\n");
                else
                    form.Write(acumulado);
            }
            catch (ErrorPascal e)
            {
                //te imprimo todo o hasta donde encontro el error, por eso coloque
                //este codigo aqui
                if (newLine)
                    form.Write(acumulado + "\r\n");
                else
                    form.Write(acumulado);
                e.ToString();
            }
            return null;
        }

        private bool validaciones(Simbolo resultado)
        {
            if(resultado != null)
            {
                if(resultado.valor != null)
                {
                    return true;
                }
                else
                {
                    throw new ErrorPascal("[Write] Error, el valor no ha sido definido aun para lo que desea imprimir",0,0,"semantico");
                }
            }
            else
            {
                throw new ErrorPascal("[Write] Error al obtener el simbolo a imprimir", 0, 0, "semantico");
            }
        }
    }
}

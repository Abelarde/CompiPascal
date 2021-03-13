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
                {//gracias a este condicional que esta afuera de las validaciones,
                 //puedo seguir imprimiendo los demas valores aunque venga entre esos alguno un null
                    if (exp != null)
                    {
                        Simbolo resultado = exp.evaluar(entorno);//TODO: entorno, porque cuando sea una variable o algo asi...
                        if (validaciones(resultado))
                            acumulado += resultado.valor.ToString();
                    }
                }

                if (newLine)
                    form.Write(acumulado + Environment.NewLine);
                else
                    form.Write(acumulado);
            }
            catch (ErrorPascal e)
            {
                //te imprimo todo o hasta donde encontro el error.
                if (newLine)
                    form.Write(acumulado + Environment.NewLine);
                else
                    form.Write(acumulado);
                e.ToString();
            }
            return null;
        }

        //puedo imprimir todos hasta que se encuentre un error
        //o podria implementarlo para imprimir todos los de resultados buenos incluso si se encuentra algun error
        //o no imprimir ninguno si viene error
        private bool validaciones(Simbolo resultado)
        {
            if (resultado == null)
                throw new ErrorPascal("[Write] Error al obtener el simbolo a imprimir", 0, 0, "semantico");
            if (resultado.valor == null)
                throw new ErrorPascal("[Write] Error, el valor no ha sido definido aun para lo que desea imprimir", 0, 0, "semantico");

            return true;
        }
    }
}

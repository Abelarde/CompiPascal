using CompiPascal.traductor.analizador;
using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class WriteInstruccion : Instruccion
    {
        private LinkedList<Expresion> valores;
        private bool newLine;

        Form1 form;

        string traductor_contenido;
        string traductor_lista_exp;

        public WriteInstruccion(LinkedList<Expresion> valores, bool newLine, Form1 formp)
        {
            this.valores = valores;
            this.newLine = newLine;
            form = formp;
            traductor_contenido = string.Empty;
            traductor_lista_exp = string.Empty;
        }

        public override object ejecutar(Entorno entorno)
        {
            string acumulado = string.Empty;

            try
            {
                int contador = 1;
                foreach (Expresion exp in valores)
                {                 
                    if (exp != null)
                    {
                        Simbolo resultado = exp.evaluar(entorno);
                        if (validaciones(resultado))
                        {
                            if (resultado.id != null)
                            {
                                if(contador == valores.Count)
                                    traductor_lista_exp += resultado.id;
                                else
                                    traductor_lista_exp += resultado.id + ",";

                            }
                            else if (resultado.valor != null)
                            {
                                if (contador == valores.Count)
                                {
                                    if(resultado.tipo.tipo == Tipos.STRING)
                                        traductor_lista_exp += "'"+Convert.ToString(resultado.valor)+ "'";
                                    else
                                        traductor_lista_exp += Convert.ToString(resultado.valor);
                                }
                                else
                                {
                                    if (resultado.tipo.tipo == Tipos.STRING)
                                        traductor_lista_exp += "'" + Convert.ToString(resultado.valor) + "'" + ",";
                                    else
                                        traductor_lista_exp += Convert.ToString(resultado.valor) + ",";
                                }

                            }
                        }
                    }
                    contador++;
                }

                if (newLine)
                {
                    traductor_contenido += "writeln("+ traductor_lista_exp + ");" + Environment.NewLine;
                    form.Write(acumulado + Environment.NewLine);
                }
                else
                {
                    traductor_contenido += "write("+ traductor_lista_exp + ");" + Environment.NewLine;
                    form.Write(acumulado);
                }
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

            return traductor_contenido;
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

using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class CallFuncProc : Instruccion
    {
        public string id;
        public LinkedList<Expresion> lista_expresiones;
        string traductor_lista_exp;

        public CallFuncProc(string id, LinkedList<Expresion> lista_expresiones)
        {
            this.id = id;
            this.lista_expresiones = lista_expresiones;
            this.traductor_lista_exp = string.Empty;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                if (id == "")
                    throw new ErrorPascal("el id viene especificado",0,0,"semantico");

                Funcion funcion = retorna_funcionValor(id, entorno);
                if (funcion != null)
                {
                    LinkedList<Simbolo> valores = new LinkedList<Simbolo>();

                    int contador = 1;
                    foreach (Expresion valor in lista_expresiones)
                    {
                        Simbolo val = valor.evaluar(entorno);
                        valores.AddLast(val);

                        if (validaciones(val))
                        {
                            if (val.id != null)
                            {
                                if (contador == lista_expresiones.Count)
                                    traductor_lista_exp += val.id;
                                else
                                    traductor_lista_exp += val.id + ", ";

                            }
                            else if (val.valor != null)
                            {
                                if (contador == lista_expresiones.Count)
                                {
                                    if (val.tipo.tipo == Tipos.STRING)
                                        traductor_lista_exp += "'" + Convert.ToString(val.valor) + "'";
                                    else
                                        traductor_lista_exp += Convert.ToString(val.valor);
                                }
                                else
                                {
                                    if (val.tipo.tipo == Tipos.STRING)
                                        traductor_lista_exp += "'" + Convert.ToString(val.valor) + "'" + ", ";
                                    else
                                        traductor_lista_exp += Convert.ToString(val.valor) + ", ";
                                }

                            }
                        }
                        contador++;

                    }

                    string para = string.Empty;
                    int contador1 = 1;
                    foreach (ParametroInst paramIns in funcion.lista_parametros)
                    {
                        foreach (string ids in paramIns.lista_ids)
                        {
                            if(contador1 == paramIns.lista_ids.Count)
                                para += ids;
                            else
                                para += ", "+ids;
                            contador1++;
                        }
                    }

                    if(funcion.lista_parametros.Count != lista_expresiones.Count)
                        traductor_lista_exp += para;

                    string total = id + "(" + traductor_lista_exp + ")" + ";" + Environment.NewLine;
                    return total;
                }
                else
                    throw new ErrorPascal("no existe una funcion o metodo con ese id", 0, 0, "semantico");
            }
            catch (ErrorPascal ex)
            {
                //ErrorPascal.cola.Enqueue(ex.ToString());
                return null;
            }
        }

        private Funcion retorna_funcionValor(string id, Entorno entorno)
        {
            if (entorno.getFuncion(id) != null)
                return entorno.getFuncion(id);
            else
                return null;
        }
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

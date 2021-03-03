using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Var : Instruccion
    {
        private LinkedList<string> lista_ids;
        private string tipo; //nativo, predefinido
        private Expresion expresion;

        public Var(LinkedList<string> lista_ids, string tipo, Expresion expresion)
        {
            this.lista_ids = lista_ids;
            this.tipo = tipo;
            this.expresion = expresion;
        }

        //TODO: validar que valor != null
        public override object ejecutar(Entorno entorno)
        {
            Tipo tipoFinal;

            if (tipo != string.Empty)
                tipoFinal = new Tipo(this.tipo);
            else
                throw new ErrorPascal("[Declaracion] El tipo de dato para la variable no viene especificada",0,0,"semantico");

            if (lista_ids.Count <= 0)
                throw new ErrorPascal("[Declaracion] La lista de id's en la declaracion esta vacia.",0,0,"semantico");

            try
            {
                if (expresion != null)
                {
                    //un nuevo simbolo porque tomare el valor y no la referencia para la declaracion-asignacion
                    //sera distinto cuando se trabaje con arrays
                    try
                    {
                        Simbolo valor = this.expresion.evaluar(entorno);
                        if (valor != null)
                        {
                            if (valor.valor != null)
                            {
                                if (valor.tipo.tipo != tipoFinal.tipo)
                                    throw new ErrorPascal("[Declaracion-Asignacion] El valor de la variable " + lista_ids.First.Value + " no coincide con el tipo declarado.", 0, 0, "semantico");

                                if (entorno.getVariable(lista_ids.First.Value) == null)
                                {
                                    valor.id = lista_ids.First.Value;
                                    valor.tipo = tipoFinal; //porque si es de tipo object aqui le digo que tipo
                                                            //valor.valor ya tiene asignado
                                    entorno.guardarVariable(valor.id, valor);
                                }
                                else
                                {
                                    throw new ErrorPascal("[Declaracion-Asignacion] Ya existe la variable " + lista_ids.First.Value, 0, 0, "semantico");
                                }

                                if (lista_ids.Count > 1)
                                    throw new ErrorPascal("[Declaracion-Asignacion] Tratas de declarar y asignar una lista de variables y no es permitido, sin embargo se logro hacer la operacion solo para el primer id", 0, 0, "semantico");
                            }
                            else
                            {
                                throw new ErrorPascal("[Declaracion-Asignacion] La variable aun no tiene un valor asignado.", 0, 0, "semantico");
                            }

                        }
                        else
                        {
                            throw new ErrorPascal("[Declaracion-Asignacion] Variable no ha sido declarada, Error al calcular u obtener el valor para la variable a declarar y asignar.", 0, 0, "semantico");
                        }
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }

                }
                else
                {
                    string msnError = string.Empty;
                    //no esta declarando e inicilizando, solo declarando.
                    foreach (string id in lista_ids)
                    {
                        if (entorno.getVariable(id) == null)
                        {
                            Simbolo variable = new Simbolo(null, tipoFinal, id);
                            entorno.guardarVariable(id, variable);
                        }
                        else
                        {
                            msnError += "[Declaracion] ya existe la variable " + id + Environment.NewLine;
                        }

                    }

                    if (msnError != "")
                        throw new ErrorPascal("[Declaracion] Se declararon algunas variables sin embargo " + Environment.NewLine + msnError, 0, 0, "semantico");
                }
            }
            catch (ErrorPascal e)
            {
                e.ToString();
            }


            
            return null; //porque no necesito nada de esta instruccion retornar
        }
    }
}
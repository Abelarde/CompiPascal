using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class Funcion : Instruccion
    {
        private string id_funcion;
        private Tipo tipo_funcion;

        private LinkedList<ParametroInst> lista_parametros;
        public LinkedList<Simbolo> valores_parametros_simbolos;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;


        public Funcion(string id_funcion, Tipo tipo_funcion, LinkedList<ParametroInst> lista_parametros, LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones)
        {
            this.id_funcion = id_funcion;
            this.tipo_funcion = tipo_funcion;
            this.lista_parametros = lista_parametros;
            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;
            valores_parametros_simbolos = null;
        }

        public override object ejecutar(Entorno entorno)
        {

            if (lista_parametros.Count == valores_parametros_simbolos.Count)
            {
                ParametroInst[] lista_parametroInst = new ParametroInst[lista_parametros.Count];
                Simbolo[] lista_valores = new Simbolo[valores_parametros_simbolos.Count];

                int i = 0;
                foreach (ParametroInst parametroInsta in lista_parametros)
                {
                    lista_parametroInst[i] = parametroInsta;
                    i++;
                }

                i = 0;
                foreach (Simbolo sim in valores_parametros_simbolos)
                {
                    lista_valores[i] = sim;
                    i++;
                }

                for (int j = 0; j < lista_parametroInst.Length; j++)
                {
                    //lista_parametroInst[j]; //LinkedList<string> lista_ids, string tipo_nativo_id, bool isReferencia
                    foreach (string id in lista_parametroInst[j].lista_ids)
                    {
                        if (entorno.getVariable(id) == null)
                        {
                            if (lista_parametroInst[j].isReferencia)
                            {
                                Simbolo nueva_variable = lista_valores[j];
                                entorno.guardarVariable(id, nueva_variable);
                                //if (entorno.getGlobal().getVariable(lista_valores[j].id) != null)
                                //   entorno.guardarVariable(lista_valores[j].id, entorno.getGlobal().getVariable(lista_valores[j].id));
                            }
                            else
                            {
                                Simbolo nueva_variable = new Simbolo(new Tipo(lista_parametroInst[j].tipo_nativo_id, entorno), id, lista_valores[j].valor);
                                entorno.guardarVariable(nueva_variable.id, nueva_variable);

                            }
                        }
                        else
                            throw new ErrorPascal("[Declaracion-Asignacion] Ya existe la variable " + id, 0, 0, "semantico");
                    }

                }

            }
            else
            {
                throw new ErrorPascal("la cantidad de valores no coincide con la cantidad de parametros para la funcion", 0, 0, "semantico");
            }

            //ejecuto el header
            foreach (Instruccion header in header_instrucciones)
            {
                if (header != null)
                    header.ejecutar(entorno);
            }

            //ejecuto el body
            object guardado = null;
            foreach (Instruccion body in body_instrucciones)
            {
                if (body != null)
                {
                    object resultado = body.ejecutar(entorno); 
                    if (resultado != null)
                    {
                        if (resultado is ExitInstruccion)
                            return resultado;//ya no sigue ejecutando las intrucciones
                        else if (resultado is ReturnInstruccion)
                            guardado = resultado;//sigue ejecutando, pero guarde el valor del return
                    }
                } 
            }
            return guardado;
        }
    }
}

/*
 
        public void setParametros(int posicion, Simbolo valor)
        {
            if(posicion <= parametros.Length && posicion >= 0)
                parametros[posicion].simbolo = valor;
            else
                throw new ErrorPascal("no coindice el envio del parametro con los parametros de la funcion", 0, 0, "semantico");
        }

        public void setTear(LinkedList<Expresion> lista_asignar, Entorno actual_AfueraFuncion)
        {
            int i = 0;
            foreach (Expresion exp in lista_asignar)
            {
                Simbolo res = exp.evaluar(actual_AfueraFuncion);//TODO: validar que sean del mismo tipo y demas
                setParametros(i, res);
                i++;
            }
        }
*/
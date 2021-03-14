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
        public string id_funcion;
        private Tipo tipo_funcion; //null podria ser si es proc

        public LinkedList<ParametroInst> lista_parametros;
        public LinkedList<Simbolo> valores_parametros_simbolos;

        private LinkedList<Instruccion> header_instrucciones;
        private LinkedList<Instruccion> body_instrucciones;

        public bool isFuncion;

        public Funcion(string id_funcion, Tipo tipo_funcion, LinkedList<ParametroInst> lista_parametros, LinkedList<Instruccion> header_instrucciones, LinkedList<Instruccion> body_instrucciones, bool isFuncion)
        {
            this.id_funcion = id_funcion;
            this.tipo_funcion = tipo_funcion;
            this.lista_parametros = lista_parametros;
            this.header_instrucciones = header_instrucciones;
            this.body_instrucciones = body_instrucciones;
            valores_parametros_simbolos = null;
            this.isFuncion = isFuncion;
        }

        public override object ejecutar(Entorno entorno)
        {
            int totalParametros = 0;
            foreach (ParametroInst parametros in lista_parametros)
            {
                foreach(string id in parametros.lista_ids)
                {
                    totalParametros++;
                }
            }

            if (totalParametros == valores_parametros_simbolos.Count)
            {
                Parametro[] lista_param = new Parametro[totalParametros];
                Simbolo[] lista_valores = new Simbolo[valores_parametros_simbolos.Count];

                int i = 0;
                foreach (ParametroInst parametroInsta in lista_parametros)
                {
                    foreach (string id in parametroInsta.lista_ids)
                    {
                        lista_param[i] = new Parametro(id, parametroInsta.tipo_nativo_id, parametroInsta.isReferencia);
                        i++;
                    }
                }

                i = 0;
                foreach (Simbolo sim in valores_parametros_simbolos)
                {
                    lista_valores[i] = sim;
                    i++;
                }

                for (int j = 0; j < lista_param.Length; j++)
                {
                    if (entorno.getVariable(lista_param[j].id) == null)
                    {
                        if (lista_param[j].isReferencia)
                        {
                            Simbolo nueva_variable = lista_valores[j];
                            entorno.guardarVariable(lista_param[j].id, nueva_variable);
                            //if (entorno.getGlobal().getVariable(lista_valores[j].id) != null)
                            //   entorno.guardarVariable(lista_valores[j].id, entorno.getGlobal().getVariable(lista_valores[j].id));
                        }
                        else
                        {
                            Simbolo nueva_variable = new Simbolo(new Tipo(lista_param[j].tipo_nativo_id, entorno), lista_param[j].id, lista_valores[j].valor, entorno, 0);
                            entorno.guardarVariable(nueva_variable.id, nueva_variable);

                        }
                    }
                    else
                        throw new ErrorPascal("[Declaracion-Asignacion] Ya existe la variable " + lista_param[j].id, 0, 0, "semantico");
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
                    if (resultado != null && isFuncion)
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

    class Parametro
    {
        public string id;
        public string tipo_nativo_id;
        public bool isReferencia;

        public Parametro(string id, string tipo_nativo_id, bool isReferencia)
        {
            this.id = id;
            this.tipo_nativo_id = tipo_nativo_id;
            this.isReferencia = isReferencia;
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
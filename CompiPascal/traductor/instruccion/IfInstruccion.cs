using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    //TODO: ver si esta bien 2 listas para los else_if o mejor 
    //algo como lista de listas
    class IfInstruccion : Instruccion
    {
        private Expresion valor_condicional;

        private LinkedList<Instruccion> if_lista_instrucciones;

        private LinkedList<Instruccion> else_lista_instrucciones;

        private LinkedList<ElseIf> else_if_lista;


        public IfInstruccion(Expresion valor_condicional, LinkedList<Instruccion> if_lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.if_lista_instrucciones = if_lista_instrucciones;
        }

        public IfInstruccion(Expresion valor_condicional, LinkedList<Instruccion> if_lista_instrucciones,
            LinkedList<Instruccion> else_lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.if_lista_instrucciones = if_lista_instrucciones;
            this.else_lista_instrucciones = else_lista_instrucciones;
        }

        public IfInstruccion(Expresion valor_condicional, LinkedList<Instruccion> if_lista_instrucciones,
            LinkedList<ElseIf> else_if_lista,
            LinkedList<Instruccion> else_lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.if_lista_instrucciones = if_lista_instrucciones;

            this.else_if_lista = else_if_lista;

            this.else_lista_instrucciones = else_lista_instrucciones;
        }


        public override object ejecutar(Entorno entorno)
        {

            object guardado = null;
            try
            {
                Simbolo condicion = validaciones(valor_condicional, entorno);

                if (Convert.ToBoolean(condicion.valor))
                {
                    foreach (Instruccion instruccion in if_lista_instrucciones)
                    {
                        if (instruccion != null)
                        {
                            object resultado = instruccion.ejecutar(entorno);
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
                else
                {
                    if (else_if_lista != null)
                    {
                        foreach (ElseIf else_if in else_if_lista)
                        {
                            if (else_if != null)
                            {
                                object resultado = else_if.ejecutar(entorno);
                                if (resultado != null)
                                {
                                    if (resultado is ExitInstruccion)
                                        return resultado;//ya no sigue ejecutando las intrucciones
                                    else if (resultado is ReturnInstruccion)
                                        guardado = resultado;//sigue ejecutando, pero guarde el valor del return
                                }

                                if (else_if.bandera)
                                    return guardado;                                
                            }
                        }
                    }

                    if (else_lista_instrucciones != null)
                    {
                        foreach (var instruccion in else_lista_instrucciones)
                        {
                            if (instruccion != null)
                            {
                                object resultado = instruccion.ejecutar(entorno);
                                if (resultado != null)
                                {
                                    if (resultado is ExitInstruccion)
                                        return resultado;//ya no sigue ejecutando las intrucciones
                                    else if (resultado is ReturnInstruccion)
                                        guardado = resultado;//sigue ejecutando, pero guarde el valor del return
                                }
                            }
                        }
                    }
                    return guardado;
                }
            }
            catch (Exception ex)
            {
                throw new ErrorPascal("",0,0,"");
            }                                    
        }

        private Simbolo validaciones(Expresion expCondicion, Entorno entorno)
        {
            if (expCondicion == null)
                throw new ErrorPascal("[IF] Error al calcular la expresion de la condicion", 0, 0, "semantico");

            Simbolo condicion = expCondicion.evaluar(entorno);

            if (condicion == null)
                throw new ErrorPascal("[if] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.valor == null)
                throw new ErrorPascal("[if] El valor de la condicion no tiene un valor definido", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[if] El tipo de la condicion no es boolean", 0, 0, "d");

            return condicion;
        }


    }
}

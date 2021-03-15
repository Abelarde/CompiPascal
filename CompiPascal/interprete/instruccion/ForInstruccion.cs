using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ForInstruccion : Instruccion
    {
        private Expresion id;
        private Expresion valor;

        private Expresion condicional;
        private LinkedList<Instruccion> lista_instrucciones;

        private bool isTo;
        public ForInstruccion(Expresion id, Expresion valor, Expresion condicional, LinkedList<Instruccion> lista_instrucciones, bool isTo)
        {
            this.id = id;
            this.valor = valor;
            this.condicional = condicional;
            this.lista_instrucciones = lista_instrucciones;
            this.isTo = isTo;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                Simbolo limite = validaciones(condicional, entorno);//valor,tipo
                Simbolo val = validaciones(valor, entorno);//valor,tipo
                //Simbolo variable = 
                validacionesID(id, entorno);//id,tipo

                if (!(Convert.ToInt32(val.valor) <= Convert.ToInt32(limite.valor)))
                    throw new ErrorPascal("[for] El valor de la variable no entran en la condicion del for",0,0,"semantico");

                //asignar [variable]
                VariableInicializarInstruccion inicializar = new VariableInicializarInstruccion(id, valor);
                inicializar.ejecutar(entorno);

                //ir a recuperar de la tabla de simbolos y manipular su valor
                Simbolo variable = validaciones(id, entorno);//id,tipo y [valor] [AHORA]                

                bool banderaRestadora = false;

                if (isTo)
                {
                    //siempre para arribar [SUMANDO]
                    for (variable.valor = Convert.ToInt32(val.valor); Convert.ToInt32(variable.valor) <= Convert.ToInt32(limite.valor); variable.valor = Convert.ToInt32(variable.valor) + 1)
                    {
                        foreach (Instruccion instruccion in lista_instrucciones)
                        {
                            if (instruccion != null)
                            {
                                object resultado = instruccion.ejecutar(entorno);

                                if (resultado != null)//null
                                {
                                    if (resultado is BreakInstruccion)//break
                                        break;
                                    else if (resultado is ContinueInstruccion)//continue
                                    {
                                        variable = validaciones(id, entorno); //actualizo el valor del condicional
                                        continue;
                                    }
                                    else if (resultado is ExitInstruccion)//exit
                                        return resultado;
                                    else if (resultado is ReturnInstruccion)//return
                                        return resultado;
                                }
                            }
                        }

                        banderaRestadora = true;
                        //TODO: revisar esto, pareciera innecesario.
                        variable = validaciones(id, entorno);//actualizo el valor del condicional y lo evaluo 
                                                             //si hubiera error me salgo gracias al throw y voy al catch...desde aqui
                    }

                    if (banderaRestadora)
                        variable.valor = Convert.ToInt32(variable.valor) - 1;
                }
                else
                {
                    //siempre para arribar [SUMANDO]
                    for (variable.valor = Convert.ToInt32(limite.valor); Convert.ToInt32(variable.valor) >= Convert.ToInt32(val.valor); variable.valor = Convert.ToInt32(variable.valor) - 1)
                    {
                        foreach (Instruccion instruccion in lista_instrucciones)
                        {
                            if (instruccion != null)
                            {
                                object resultado = instruccion.ejecutar(entorno);

                                if (resultado != null)//null
                                {
                                    if (resultado is BreakInstruccion)//break
                                        break;
                                    else if (resultado is ContinueInstruccion)//continue
                                    {
                                        variable = validaciones(id, entorno); //actualizo el valor del condicional
                                        continue;
                                    }
                                    else if (resultado is ExitInstruccion)//exit
                                        return resultado;
                                    else if (resultado is ReturnInstruccion)//return
                                        return resultado;
                                }
                            }
                        }

                        banderaRestadora = true;
                        //TODO: revisar esto, pareciera innecesario.
                        variable = validaciones(id, entorno);//actualizo el valor del condicional y lo evaluo 
                                                             //si hubiera error me salgo gracias al throw y voy al catch...desde aqui
                    }

                }

            }
            catch (Exception ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en la sentencia for", 0, 0, "semantico");

            }
            return null;
        }


        //variable de tipo primitivo [INTEGER]
        //valores que puede tomar la variable [MISMO TIPO]
        //que sean enteros [INTEGER]
        //que coincidan en tipo

        private Simbolo validaciones(Expresion exp, Entorno entorno)
        {
            if (exp == null)
                throw new ErrorPascal("[for] Error al calcular la expresion", 0, 0, "semantico");

            Simbolo resultado = exp.evaluar(entorno);

            if (resultado == null)
                throw new ErrorPascal("[for] Error al obtener el valor del simbolo", 0, 0, "d");
            if (resultado.valor == null)
                throw new ErrorPascal("[for] El valor del simbolo no tiene un valor definido aun", 0, 0, "d");
            if (resultado.tipo == null)
                throw new ErrorPascal("[for] El tipo del simbolo no esta definida", 0, 0, "d");
            if (resultado.tipo.tipo != Tipos.INTEGER)
                throw new ErrorPascal("[for] El tipo del simbolo no es Integer", 0, 0, "d");
            return resultado;
        }

        private Simbolo validacionesID(Expresion exp, Entorno entorno)
        {
            if (exp == null)
                throw new ErrorPascal("[for] Error al calcular la expresion", 0, 0, "semantico");

            Simbolo resultado = exp.evaluar(entorno);

            if (resultado == null)
                throw new ErrorPascal("[for] Error al obtener el valor del simbolo", 0, 0, "d");
            if (resultado.tipo == null)
                throw new ErrorPascal("[for] El tipo del simbolo no esta definida", 0, 0, "d");
            if (resultado.tipo.tipo != Tipos.INTEGER)
                throw new ErrorPascal("[for] El tipo del simbolo no es Integer", 0, 0, "d");
            return resultado;

        }

    }
}

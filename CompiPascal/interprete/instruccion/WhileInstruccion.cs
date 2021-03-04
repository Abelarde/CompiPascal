using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class WhileInstruccion : Instruccion
    {
        private Expresion expCondicion;
        private LinkedList<Instruccion> lista_instrucciones;

        public WhileInstruccion(Expresion expCondicion, LinkedList<Instruccion> lista_instrucciones)
        {
            this.expCondicion = expCondicion;
            this.lista_instrucciones = lista_instrucciones;

        }
        public override object ejecutar(Entorno entorno)
        {
            try
            {
                Simbolo condicion = validaciones(expCondicion, entorno);

                while (Convert.ToBoolean(condicion.valor))
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
                                    condicion = validaciones(expCondicion, entorno); //actualizo el valor del condicional
                                    continue;
                                }
                                else if (resultado is ExitInstruccion)//exit
                                    return resultado;
                                else if (resultado is ReturnInstruccion)//return
                                    return resultado;
                            }
                        }
                    }

                    condicion = validaciones(expCondicion, entorno);//actualizo el valor del condicional y lo evaluo 
                                                                    //si hubiera error me salgo gracias al throw y voy al catch...desde aqui
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;      //TODO:ver si tengo que regresar algo      
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

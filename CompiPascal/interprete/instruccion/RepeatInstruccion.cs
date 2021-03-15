using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class RepeatInstruccion : Instruccion
    {
        private LinkedList<Instruccion> lista_instrucciones;
        private Expresion expCondicion;

        public RepeatInstruccion(LinkedList<Instruccion> lista_instrucciones, Expresion expCondicion)
        {
            this.lista_instrucciones = lista_instrucciones;
            this.expCondicion = expCondicion;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                Simbolo condicion = validaciones(expCondicion, entorno);

                do
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
                    //actualiza el valor:
                    //esto sucede porque antes en sus instrucciones ya ejecute alguna asignacion o algo que cambiara la 
                    //variable que tiene la expresion, en este caso la espresion tiene el id pero con ese id buca en la
                    //tabla de simbolos y encuentra que ya tiene un nuevo valor. 
                    condicion = validaciones(expCondicion, entorno);//actualizo el valor del condicional y lo evaluo 

                } while (!Convert.ToBoolean(condicion.valor));

            }
            catch (Exception ex)
            {

                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en el repeat", 0, 0, "semantico");
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

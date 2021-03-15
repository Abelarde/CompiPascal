using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class CaseInstruccion : Instruccion
    {
        private Expresion valor_condicional;
        private LinkedList<Cases> lista_cases;

        private LinkedList<Instruccion> else_or_otherwise_instrucciones;


        public CaseInstruccion(Expresion valor_condicional,
            LinkedList<Cases> lista_cases,
            LinkedList<Instruccion> else_or_otherwise_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.lista_cases = lista_cases;
            this.else_or_otherwise_instrucciones = else_or_otherwise_instrucciones;
        }

        public override object ejecutar(Entorno entorno)
        {
            //tipo expresion == tipo de casos
            //cases: numeros enteros, caracteres, booleanos, 

            object guardado = null;

            try
            {
                Simbolo condicion = validaciones(valor_condicional, entorno);

                if(lista_cases != null)
                {
                    foreach (Cases casos in lista_cases)
                    {
                        if (casos != null)
                        {
                            casos.condicion = condicion;//le envio la condicion
                            object res = casos.ejecutar(entorno);
                            if (res != null)
                            {
                                if (res is ExitInstruccion)
                                    return res;//ya no sigue ejecutando las intrucciones
                                else if (res is ReturnInstruccion)
                                    guardado = res;//sigue ejecutando, pero guarde el valor del return
                            }

                            if (casos.bandera)
                                return guardado;
                        }
                    }
                }


                if (else_or_otherwise_instrucciones != null)
                {
                    foreach (var instruccion in else_or_otherwise_instrucciones)
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
            }
            catch (Exception ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en los cases ", 0, 0, "semantico");
            }

            return guardado;
        }

        private Simbolo validaciones(Expresion expCondicion, Entorno entorno)
        {
            if (expCondicion == null)
                throw new ErrorPascal("[case] Error al calcular la expresion de la condicion", 0, 0, "semantico");

            Simbolo condicion = expCondicion.evaluar(entorno);

            if (condicion == null)
                throw new ErrorPascal("[case] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.valor == null)
                throw new ErrorPascal("[case] El valor de la condicion no tiene un valor definido", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.INTEGER && 
                condicion.tipo.tipo != Tipos.STRING  &&
                condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[case] El tipo de la condicion no es ni entero, ni string, ni boolean. Por lo tanto no es valida", 0, 0, "d");

            return condicion;
        }
    }
}

/* Todas las constantes de casos deben tener el mismo tipo.
 * If no else part is present, and no case constant matches the expression value, program flow continues after the final end
 * los case's/else pueden tener el bloque BEGIN - END
 * integers, characters, boolean or enumerated data items
 * The case label for a case must be the same data type as the expression in the case statement, and it must be a constant or a literal.
 * TODO: verificar si los cases pueden tener una variable o solo valores ya conocidos
 */
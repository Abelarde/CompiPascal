using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.expresion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
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
                            casos.ejecutar(entorno);
                            if (casos.bandera)//verifico si ya entro en un case
                                return null;
                        }
                    }
                }

                if (else_or_otherwise_instrucciones != null)
                {
                    foreach (var instruccion in else_or_otherwise_instrucciones)
                    {
                        if (instruccion != null)
                            instruccion.ejecutar(entorno);
                    }
                    return null;
                }


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return null;
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
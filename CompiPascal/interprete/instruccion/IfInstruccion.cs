using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
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
            //calcular
            Simbolo condicion = this.valor_condicional.evaluar(entorno);

            //TODO verificar errores
            if (condicion == null || condicion.valor == null)
                throw new ErrorPascal("[if] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[if] El tipo de la condicion no es boolean", 0, 0,"d");

            if (Convert.ToBoolean(condicion.valor))
            {
                try
                {//Entorno aqui iria si tuvieramos que manejar los ambitos en cada instruccion pero pascal funciona diferente
                    foreach (var instruccion in if_lista_instrucciones)
                    {
                        if(instruccion != null)
                            print += instruccion.ejecutar(entorno);
                    }
                    return print;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {

                if (else_if_lista != null)
                {
                    foreach (ElseIf else_if in else_if_lista)
                    {
                        if (else_if != null)
                        {//aqui tendria que crear un entorno nuevo si fuera otro tipo de lenguaje
                            print = else_if.ejecutar(entorno).ToString();
                            if (else_if.bandera)
                                return print;
                        }
                    }

                }

                if (else_lista_instrucciones != null)
                {
                    try
                    {//Entorno aqui iria si tuvieramos que manejar los ambitos en cada instruccion pero pascal funciona diferente
                        foreach (var instruccion in else_lista_instrucciones)
                        {
                            if (instruccion != null)
                                print += instruccion.ejecutar(entorno);
                        }
                        return print;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                }

            }
            return print;
        }

    }
}

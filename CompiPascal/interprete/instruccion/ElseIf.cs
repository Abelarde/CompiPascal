using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ElseIf : Instruccion
    {
        private Expresion valor_condicional;
        private LinkedList<Instruccion> if_lista_instrucciones;

        public bool bandera;

        public ElseIf(Expresion valor_condicional, LinkedList<Instruccion> if_lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.if_lista_instrucciones = if_lista_instrucciones;

            bandera = false;
        }


        public override object ejecutar(Entorno entorno)
        {
            //calcular
            Simbolo condicion = this.valor_condicional.evaluar(entorno);

            //TODO verificar errores
            if (condicion == null || condicion.valor == null)
                throw new ErrorPascal("[else if] Error al obtener el valor de la condicion", 0, 0, "d");
            if (condicion.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("[else if] El tipo de la condicion no es boolean", 0, 0, "d");

            if (Convert.ToBoolean(condicion.valor))
            {
                try
                {//Entorno aqui iria si tuvieramos que manejar los ambitos en cada instruccion pero pascal funciona diferente
                    foreach (var instruccion in if_lista_instrucciones)
                    {
                        if (instruccion != null)
                            print += instruccion.ejecutar(entorno);
                    }
                    this.bandera = true;
                    return print;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return print;
        }
    }
}

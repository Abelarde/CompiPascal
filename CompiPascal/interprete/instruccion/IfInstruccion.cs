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

        private LinkedList<Expresion> elseIf_lista_valor_condicional;
        private LinkedList<Instruccion> elseIf_lista_instrucciones;


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
            LinkedList<Expresion> elseIf_lista_valor_condicional,
            LinkedList<Instruccion> elseIf_lista_instrucciones, 
            LinkedList<Instruccion> else_lista_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.if_lista_instrucciones = if_lista_instrucciones;

            this.elseIf_lista_valor_condicional = elseIf_lista_valor_condicional;
            this.elseIf_lista_instrucciones = elseIf_lista_instrucciones;

            this.else_lista_instrucciones = else_lista_instrucciones;
        }


        public override object ejecutar(Entorno entorno)
        {
            Simbolo valor = this.valor_condicional.evaluar(entorno);

            //TODO verificar errores
            if (valor.tipo.tipo != Tipos.BOOLEAN)
                throw new ErrorPascal("El tipo no es booleano para el IF", 0, 0,"d");

            if (bool.Parse(valor.valor.ToString()))
            {
                try
                {
                    //Entorno aqui iria si tuvieramos que manejar los ambitos en cada instruccion pero pascal funciona diferente
                    
                    //foreach (var instruccion in instrucciones)
                    //{
                    //    instruccion.ejecutar(entorno);  return?
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //if (_else != null) _else.ejecutar(entorno);
            }
            return null;
        }

    }
}

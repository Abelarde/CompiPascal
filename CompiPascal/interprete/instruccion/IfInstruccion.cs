using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
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
            throw new NotImplementedException();
        }
    }
}

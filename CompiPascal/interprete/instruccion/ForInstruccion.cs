using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
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


        public ForInstruccion(Expresion id, Expresion valor, Expresion condicional, LinkedList<Instruccion> lista_instrucciones)
        {
            this.id = id;
            this.valor = valor;
            this.condicional = condicional;
            this.lista_instrucciones = lista_instrucciones;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

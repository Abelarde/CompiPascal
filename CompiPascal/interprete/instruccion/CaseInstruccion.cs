using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class CaseInstruccion : Instruccion
    {
        private Expresion valor_condicional;
        private LinkedList<Expresion> cases_valores;
        private LinkedList<Instruccion> cases_Instrucciones;

        private LinkedList<Instruccion> else_or_otherwise_instrucciones;


        public CaseInstruccion(Expresion valor_condicional, LinkedList<Expresion> cases_valores, 
            LinkedList<Instruccion> cases_Instrucciones,
            LinkedList<Instruccion> else_or_otherwise_instrucciones)
        {
            this.valor_condicional = valor_condicional;
            this.cases_valores = cases_valores;
            this.cases_Instrucciones = cases_Instrucciones;
            this.else_or_otherwise_instrucciones = else_or_otherwise_instrucciones;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

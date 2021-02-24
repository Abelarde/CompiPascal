using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class TypeInstruccion : Instruccion
    {
        private LinkedList<string> lista_ids;
        private string tipo_variable;

        private Expresion valor;
        private string tipo_array;

        public TypeInstruccion(LinkedList<string> lista_ids, string tipo_variable)
        {
            this.lista_ids = lista_ids;
            this.tipo_variable = tipo_variable;
        }
        public TypeInstruccion(LinkedList<string> lista_ids, string tipo_variable, Expresion valor, string tipo_array)
        {
            this.lista_ids = lista_ids;
            this.tipo_variable = tipo_variable;
            this.valor = valor;
            this.tipo_array = tipo_array;
        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}

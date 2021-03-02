using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class Var : Instruccion
    {
        private LinkedList<string> lista_ids;
        private string tipo; //nativo, predefinido
        private Expresion valor;

        public Var(LinkedList<string> lista_ids, string tipo, Expresion valor)
        {
            this.lista_ids = lista_ids;
            this.tipo = tipo;
            this.valor = valor;
        }

        //TODO: validar que valor != null
        public override object ejecutar(Entorno entorno)
        {
            Simbolo valor = this.valor.evaluar(entorno);
            entorno.DeclararVariable("aqui van los ids o el id", valor);
            return null;
        }
    }
}

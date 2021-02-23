using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    /// <summary>
    /// clase que represanta una literal de valor, es decir el valor mas minimo de
    /// una expresion: tipos de datos? o variables?
    /// </summary>
    class Literal : Expresion
    {
        private string tipo;
        private object valor;
        private object rangoInferior;
        private object rangoSuperior;

        public Literal(string tipo, object valor)
        {
            this.tipo = tipo;
            this.valor = valor;
        }
        public Literal(string tipo, object rangoInferior, object rangoSuperior) //".."
        {
            this.tipo = tipo;
            this.rangoInferior = rangoInferior;
            this.rangoSuperior = rangoSuperior;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            //TODO: tipos
            return new Simbolo(this.valor, new Tipo(Tipos.INTEGER, null), null);

        }
    }
}

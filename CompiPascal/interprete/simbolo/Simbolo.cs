using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.analizador.simbolo
{
    /// <summary>
    /// Simbolo es un objeto que se guardara en la tabla de simbolos
    /// Representa: una variable, una constante, una funcion, un metodo
    /// Puede tener: atributos que ayuden al interprete
    /// </summary>
    class Simbolo
    {
        public object valor;
        public string id;
        public Tipo tipo;
        //linea
        //columna

        /// <summary>
        /// construccion de un simbolo
        /// </summary>
        /// <param name="valor">valor del simbolo</param>
        /// <param name="tipo">tipo del simbolo</param>
        /// <param name="id">id del simbolo</param>
        public Simbolo(object valor, Tipo tipo, string id)
        {
            this.valor = valor;
            this.tipo = tipo;
            this.id = id;
        }

        public override string ToString()
        {
            return this.valor.ToString();
        }

    }
}

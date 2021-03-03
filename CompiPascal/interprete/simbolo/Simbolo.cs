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
        public Tipo tipo; //tipo de dato del simbolo
        public string id; //identificador asociado al simbolo
        public object valor; //guarda el valor del simbolo
        //puede ser un tipo dato primitivo, un arreglo, un object
        //una funcion, un procedimiento


        //linea
        //columna

        public Simbolo(Tipo tipo, string id, object valor)
        {
            this.tipo = tipo;
            this.id = id;
            this.valor = valor;
        }
    }
}

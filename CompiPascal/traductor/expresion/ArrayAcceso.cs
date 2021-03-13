using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class ArrayAcceso : Expresion
    {
        Expresion id_simbolo;
        LinkedList<Expresion> lista_expr;

        public ArrayAcceso(Expresion id_simbolo, LinkedList<Expresion> lista_expr)
        {
            this.id_simbolo = id_simbolo;
            this.lista_expr = lista_expr;
        }

        public override Simbolo evaluar(Entorno entorno)
        {            
            Simbolo variable_array = id_simbolo.evaluar(entorno);

            if (variable_array.tipo.tipo != Tipos.ARRAY)
                throw new ErrorPascal("la variable no es un arreglo",0,0,"semantico");

            Simbolo dimension_valor = variable_array;
            foreach(Expresion indice in lista_expr)
            {
                Simbolo indi = indice.evaluar(entorno);
                dimension_valor = obtenerIndiceByDimension(Convert.ToInt32(indi.valor), dimension_valor);
            }

            return dimension_valor;
        }

        private Simbolo obtenerIndiceByDimension(int indice, Simbolo dimension)//indice
        {
            Arreglo arreglo = dimension.valor as Arreglo;
            return arreglo.getIndice(indice);
        }
    }
}

//tiene que coincidir la cantidad de expresiones con la cantidad de dimensiones
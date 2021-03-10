using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class ArrayAsignar : Instruccion
    {
        Expresion id_simbolo;
        LinkedList<Expresion> lista_expr;
        Expresion valor;

        public ArrayAsignar(Expresion id_simbolo, LinkedList<Expresion> lista_expr, Expresion valor)
        {
            this.id_simbolo = id_simbolo;
            this.lista_expr = lista_expr;
            this.valor = valor;
        }


        public override object ejecutar(Entorno entorno)
        {
            Simbolo variable_array = id_simbolo.evaluar(entorno);


            Simbolo valorFinal = valor.evaluar(entorno);

            //Simbolo dimension_valor = variable_array;
            int limite = lista_expr.Count;
            foreach (Expresion indice in lista_expr)
            {
                Simbolo indi = indice.evaluar(entorno);
                variable_array = obtenerIndiceByDimension(Convert.ToInt32(indi.valor), variable_array, limite, valorFinal);
                limite--;
            }


            return null;
        }

        private Simbolo obtenerIndiceByDimension(int indice, Simbolo dimension, int limite, Simbolo valorFinal)//indice
        {
            Arreglo arreglo = dimension.valor as Arreglo;
            if(limite != 1)
                return arreglo.getIndice(indice);
            else
            {
                arreglo.setIndice(indice, valorFinal);
                return null;
            }
        }
    }
}

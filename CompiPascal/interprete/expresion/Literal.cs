﻿using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
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

        private Expresion min;
        private Expresion max;

        public Literal(string tipo, object valor)
        {
            this.tipo = tipo;
            this.valor = valor;
        }
        public Literal(string tipo, Expresion min, Expresion max) //".." //"."
        {
            this.tipo = tipo;
            this.min = min;
            this.max = max;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            switch (tipo)
            {
                case "CADENA":
                    return new Simbolo(new Tipo(Tipos.STRING, null), null, Convert.ToString(this.valor).Trim('\''));
                case "NUMBER":
                    if (this.valor.ToString().Contains("."))
                        return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(this.valor));
                    else
                        return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(this.valor));
                case "TRUE":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToBoolean(this.valor));
                case "FALSE":
                    return new Simbolo(new Tipo(Tipos.BOOLEAN, null), null, Convert.ToBoolean(this.valor));
                
                case "ID"://otro id, no! porque igual yo antes en la asignacion ya le extraje su tipo y se lo asigne
                    //por lo tanto no tenga un tipo de otra "variable" sino el valor primitivo, array u object
                    //excepto si es un object entonces si es de tipo de otra variable pero realmente es un object
                    //pero igual es Tipos.OBJECT -> tipoauxiliar [es decir otro Simbolo :)]

                    //no me interesa yo solo retorno el simbolo y ya de ahi, puedo saber su tipo
                    //y dependiendo su tipo entonces puedo obtener su valor respectivo.
                    //es decir si es primitivo -> obtengo un simbolo con un valor primitivo en el atributo valor
                    //si es array -> obtengo un simbolo con un array en el atributo valor
                    //si es object -> obtengo un simbolo con un object en el atributo valor
                    return retorna_simbolo(this.valor.ToString(), entorno);

                case "PERIOD":
                    return new Simbolo(null, null, null);

                case "PERIOD_PERIOD"://crea un nuevo arreglo
                    Simbolo minR = validaciones(min, entorno);
                    Simbolo maxR = validaciones(max, entorno);
                    //.tipo, .valor(object)[Arreglo]//.isConst//.isType//.isArray
                    return new Simbolo(new Tipo(Tipos.ARRAY, null), getIndice(minR), getIndice(maxR));
                    //id //.valor(object)[Arreglo] -> tipoDatos

                default:
                    return null;
            }
        }

        /// <summary>
        /// Retorna un simbolo si existe en la tabla de simbolos
        /// </summary>
        /// <param name="id">id de la variable a buscar</param>
        /// <param name="entorno">entorno en donde deberia de estar</param>
        /// <returns>El simbolo si existe o null</returns>
        private Simbolo retorna_simbolo(string id, Entorno entorno)//Simbolo
        {
            if (entorno.getVariable(id) != null)
                return entorno.getVariable(id); //Simbolo -> primitivo, array, object
            else
                return null;
        }       

        private Simbolo validaciones(Expresion exp, Entorno entorno)
        {
            if (exp == null)
                throw new ErrorPascal("[literal] Error al calcular la expresion", 0, 0, "semantico");

            Simbolo simbolo = exp.evaluar(entorno);

            if (simbolo == null)
                throw new ErrorPascal("[literal] Error al obtener el simbolo", 0, 0, "d");
            if (simbolo.valor == null)
                throw new ErrorPascal("[literal] El valor del simbolo no tiene un valor definido", 0, 0, "d");
            if (simbolo.tipo == null)
                throw new ErrorPascal("[literal] El tipo del simbolo no esta definido", 0, 0, "d");

            return simbolo;
        }

        private int getIndice(Simbolo valor)
        {
            if (valor == null)
                throw new ErrorPascal("error al obtener el rango uno de los rangos.", 0, 0, "semantico");
            if (valor.valor == null)
                throw new ErrorPascal("error al obtener el rango uno de los rangos.", 0, 0, "semantico");
            return Convert.ToInt32(valor.valor);
        }





    }

}

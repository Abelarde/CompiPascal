using CompiPascal.interprete.analizador.simbolo;
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
        private object valor1;
        private object valor2;

        public Literal(string tipo, object valor)
        {
            this.tipo = tipo;
            this.valor = valor;
        }
        public Literal(string tipo, object valor1, object valor2) //".." //"."
        {
            this.tipo = tipo;
            this.valor1 = valor1;
            this.valor2 = valor2;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            switch (tipo)
            {
                case "CADENA":
                    return new Simbolo(Convert.ToString(this.valor).Trim('\''), new Tipo(Tipos.STRING, null), null);

                case "NUMBER":
                    if (this.valor.ToString().Contains("."))
                        return new Simbolo(Convert.ToDouble(this.valor), new Tipo(Tipos.REAL, null), null);
                    else
                        return new Simbolo(Convert.ToInt32(this.valor), new Tipo(Tipos.INTEGER, null), null);

                case "TRUE":
                    return new Simbolo(Convert.ToBoolean(this.valor), new Tipo(Tipos.BOOLEAN, null), null);

                case "FALSE":
                    return new Simbolo(Convert.ToBoolean(this.valor), new Tipo(Tipos.BOOLEAN, null), null);
                
                case "ID"://array,object,nativo,otroid



                case "function_call":
                    //falta buscar en la tabla de simbolos, 
                    //hacer las validaciones respectivas y 
                    //darle su valor o darle su referencia o ver que debeo de retornar aqui                    
                    return new Simbolo(null, null, null);

                case "array_call":
                    return new Simbolo(null, null, null);

                case "PERIOD":
                    return new Simbolo(null, null, null);

                case "PERIOD_PERIOD":

                default:
                    return null;
            }
        }


        private Simbolo id(string id, Entorno entorno)
        {//array,object,nativo,otroid
            if (entorno.getVariable(id) != null)
                return entorno.getVariable(id); //Simbolo
            else if (entorno.getArreglo(id) != null)
                return entorno.getArreglo(id);
            else
                return null;
        }

    }
}

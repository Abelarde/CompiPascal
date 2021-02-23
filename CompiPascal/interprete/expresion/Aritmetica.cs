using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class Aritmetica : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private string tipoOperacion;

        public Aritmetica(Expresion izquierda, Expresion derecha, string tipoOperacion)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipoOperacion = tipoOperacion;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo izquierda = this.izquierda.evaluar(entorno);
            Simbolo derecha = this.derecha.evaluar(entorno);
            Simbolo resultado;
            Tipos tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);

            if (tipoResultante == Tipos.ERROR)
            {
                throw new ErrorPascal("Tipos de dato incorrectos", 0, 0, "Semantico");
            }

            //TODO: validar estas condicionales
            if (tipoResultante != Tipos.INTEGER && tipoOperacion != "+")
            {
                throw new Exception("Algun mensaje");
            }

            //TODO: validar el tipo para que sea Tipos.INTEGER y no el izquierda.tipo
            //en el default alguna variable error? 
            //operaciones con cadenas no solo enteros
            switch (tipoOperacion)
            {
                case "+":
                    //TODO: validar cuando es unario
                    //el izquierdo es == null
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) + double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                case "-":
                    //TODO: validar cuando es unario
                    //el izquierdo es == null
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) - double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                case "*":
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) * double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                case "/":
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) / double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                case "%":
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) % double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                default:
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) + double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
            }
        }
    }
}

using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class Logica : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private string tipoOperacion;
        public Logica(Expresion izquierda, Expresion derecha, string tipoOperacion)
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
            Tipo tipo = new Tipo(Tipos.BOOLEAN, null); //porque pueden venir variables no precisamente booleanas
            Tipos tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);

            if (tipoResultante == Tipos.ERROR)
            {
                throw new ErrorPascal("Tipos de dato incorrectos", 0, 0, "Semantico");
            }

            //TODO: verificar si es double.Parse o es boolean.Parse?
            switch (tipoOperacion)
            {
                //TODO: hacer las conversiones al tipo de variable correcta y corregir los operando para el resultado
                case "AND":
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) == double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
                case "OR":
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) != double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
                case "NOT":
                    //TODO: validar cuando es unario
                    //el izquierdo es == null
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) != double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
                default:
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) < double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
            }

        }
    }
}

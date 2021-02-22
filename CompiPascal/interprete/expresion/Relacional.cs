using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class Relacional : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private char tipoOperacion;
        public Relacional(Expresion izquierda, Expresion derecha, char tipoOperacion)
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
                case '=':
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) == double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
                case '!':
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) != double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
                case '>':
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) > double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
                default:
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) < double.Parse(derecha.ToString()), tipo, null);
                    return resultado;
            }


        }
    }
}

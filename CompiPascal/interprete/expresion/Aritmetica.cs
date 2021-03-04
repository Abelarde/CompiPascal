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
        private Expresion expIzq;
        private Expresion expDer;
        private string tipoOperacion;

        public Aritmetica(Expresion expIzq, Expresion expDer, string tipoOperacion)
        {
            this.expIzq = expIzq;
            this.expDer = expDer;
            this.tipoOperacion = tipoOperacion;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            //TODO: que pasa si colo un try aqui, si aqui termino el throw entonces en la condicional
            //que pasaria?

            Simbolo derecha = validaciones(expDer, entorno);

            Tipos tipoResultante = derecha.tipo.tipo;

            Simbolo izquierda = null;

            if (expIzq != null)
            {
                izquierda = validaciones(expIzq, entorno);

                tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);
            }

            if (tipoResultante == Tipos.ERROR)
            {
                if(izquierda != null)
                    throw new ErrorPascal("Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                else
                    throw new ErrorPascal("Tipo de dato incorrecto [" + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
            }


            switch (tipoOperacion)
            {
                case "+":

                    if (izquierda != null)
                    {
                        if (tipoResultante == Tipos.INTEGER)
                            return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(izquierda.valor) + Convert.ToInt32(derecha.valor));
                        else if (tipoResultante == Tipos.REAL)
                            return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(izquierda.valor) + Convert.ToDouble(derecha.valor));
                        else if (tipoResultante == Tipos.STRING)
                            return new Simbolo(new Tipo(Tipos.STRING, null), null, Convert.ToString(izquierda.valor) + Convert.ToString(derecha.valor));
                        else 
                            throw new ErrorPascal("Operacion + entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }
                    else
                    {
                        if (tipoResultante == Tipos.INTEGER)
                            return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(derecha.valor) * +1);
                        else if (tipoResultante == Tipos.REAL)
                            return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(derecha.valor) * +1);
                        else
                            throw new ErrorPascal("Operacion + con un Tipo de dato incorrecto [" + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }

                case "-":

                    if(izquierda != null)
                    {
                        if (tipoResultante == Tipos.INTEGER)
                            return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(izquierda.valor) - Convert.ToInt32(derecha.valor));

                        else if (tipoResultante == Tipos.REAL)
                            return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(izquierda.valor) - Convert.ToDouble(derecha.valor));

                        else
                            throw new ErrorPascal("Operacion - entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }
                    else
                    {
                        if (tipoResultante == Tipos.INTEGER)
                            return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(derecha.valor) * -1);

                        else if (tipoResultante == Tipos.REAL)
                            return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(derecha.valor) * -1);

                        else
                            throw new ErrorPascal("Operacion - con un Tipo de dato incorrecto [" + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }

                case "*":
                    if(tipoResultante == Tipos.INTEGER)
                        return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(izquierda.valor) * Convert.ToInt32(derecha.valor));
                    else if (tipoResultante == Tipos.REAL)
                        return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(izquierda.valor) * Convert.ToDouble(derecha.valor));
                    else
                        throw new ErrorPascal("Operacion * entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");

                case "/":
                    if (tipoResultante == Tipos.INTEGER)
                        return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(izquierda.valor) / Convert.ToInt32(derecha.valor));
                    else if (tipoResultante == Tipos.REAL)
                        return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(izquierda.valor) / Convert.ToDouble(derecha.valor));
                    else
                        throw new ErrorPascal("Operacion / entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");

                case "%":
                    if (tipoResultante == Tipos.INTEGER) 
                        return new Simbolo(new Tipo(Tipos.INTEGER, null), null, Convert.ToInt32(izquierda.valor) % Convert.ToInt32(derecha.valor));
                    else if (tipoResultante == Tipos.REAL)
                        return new Simbolo(new Tipo(Tipos.REAL, null), null, Convert.ToDouble(izquierda.valor) % Convert.ToDouble(derecha.valor));
                    else
                        throw new ErrorPascal("Operacion % entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");

                default:
                    throw new ErrorPascal("Operacion aritmetica desconocida", 0, 0, "Semantico");
            }
        }

        private Simbolo validaciones(Expresion exp, Entorno entorno)
        {
            if (exp == null)
                throw new ErrorPascal("[aritmetica] Error al calcular la expresion", 0, 0, "semantico");

            Simbolo simbolo = exp.evaluar(entorno);

            if (simbolo == null)
                throw new ErrorPascal("[aritmetica] Error al obtener el simbolo", 0, 0, "d");
            //if (simbolo.valor == null)
            //    throw new ErrorPascal("[aritmetica] El valor del simbolo no tiene un valor definido", 0, 0, "d");
            if (simbolo.tipo == null)
                throw new ErrorPascal("[aritmetica] El tipo del simbolo no esta definido", 0, 0, "d");

            return simbolo;
        }
    }
}


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
            //evaluar
            //evaluar error

            Simbolo derecha = this.expDer.evaluar(entorno);
            Simbolo izquierda = null;

            Tipos tipoResultante = derecha.tipo.tipo;

            if (expIzq != null)
            {
                izquierda = this.expIzq.evaluar(entorno);
                tipoResultante = TablaTipos.GetTipo(izquierda.tipo, derecha.tipo);
            }


            if (tipoResultante == Tipos.ERROR)
            {
                if(izquierda != null)
                {
                    throw new ErrorPascal("Tipos de datos incorrectos ["+izquierda.tipo.tipo +", "+ tipoOperacion + ", " + derecha.tipo.tipo +"]", 0, 0, "Semantico");
                }
                else
                {
                    throw new ErrorPascal("Tipo de dato incorrecto [" + tipoOperacion + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                }
            }

            Simbolo resultado;

            //en el default alguna variable error? 
            switch (tipoOperacion)
            {
                case "+":

                    if (izquierda != null)
                    {
                        if (tipoResultante == Tipos.INTEGER)
                        {
                            resultado = new Simbolo(Convert.ToInt32(izquierda.valor) + Convert.ToInt32(derecha.valor), new Tipo(Tipos.INTEGER, null), null);
                        }
                        else if (tipoResultante == Tipos.REAL)
                        {
                            resultado = new Simbolo(Convert.ToDouble(izquierda.valor) + Convert.ToDouble(derecha.valor), new Tipo(Tipos.REAL, null), null);
                        }
                        else if (tipoResultante == Tipos.STRING)
                        {
                            resultado = new Simbolo(Convert.ToString(izquierda.valor) + Convert.ToString(derecha.valor), new Tipo(Tipos.STRING, null), null);
                        }
                        else
                        {
                            throw new ErrorPascal("Operacion + entre Tipos de datos incorrectos ["+ izquierda.tipo.tipo +", "+derecha.tipo.tipo+ "]", 0, 0, "Semantico");
                        }

                    }
                    else
                    {
                        if (tipoResultante == Tipos.INTEGER)
                        {
                            resultado = new Simbolo(Convert.ToInt32(derecha.valor) * +1, new Tipo(Tipos.INTEGER, null), null);
                        }
                        else if (tipoResultante == Tipos.REAL)
                        {
                            resultado = new Simbolo(Convert.ToDouble(derecha.valor) * +1, new Tipo(Tipos.REAL, null), null);
                        }
                        else
                        {
                            throw new ErrorPascal("Operacion + con un Tipo de dato incorrecto [" + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                        }
                    }

                    return resultado;


                case "-":

                    if(izquierda != null)
                    {
                        if (tipoResultante == Tipos.INTEGER)
                        {
                            resultado = new Simbolo(Convert.ToInt32(izquierda.valor) - Convert.ToInt32(derecha.valor), new Tipo(Tipos.INTEGER, null), null);
                        }
                        else if (tipoResultante == Tipos.REAL)
                        {
                            resultado = new Simbolo(Convert.ToDouble(izquierda.valor) - Convert.ToDouble(derecha.valor), new Tipo(Tipos.REAL, null), null);
                        }
                        else
                        {
                            throw new ErrorPascal("Operacion - entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                        }

                    }
                    else
                    {
                        if (tipoResultante == Tipos.INTEGER)
                        {
                            resultado = new Simbolo(Convert.ToInt32(derecha.valor) * - 1, new Tipo(Tipos.INTEGER, null), null);
                        }
                        else if (tipoResultante == Tipos.REAL)
                        {
                            resultado = new Simbolo(Convert.ToDouble(derecha.valor) * -1, new Tipo(Tipos.REAL, null), null);
                        }
                        else
                        {
                            throw new ErrorPascal("Operacion - con un Tipo de dato incorrecto [" + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                        }
                    }                    

                    return resultado;

                case "*":
                    if(tipoResultante == Tipos.INTEGER)
                    {
                        resultado = new Simbolo(Convert.ToInt32(izquierda.valor) * Convert.ToInt32(derecha.valor), new Tipo(Tipos.INTEGER, null), null);
                    }
                    else if(tipoResultante == Tipos.REAL)
                    {
                        resultado = new Simbolo(Convert.ToDouble(izquierda.valor) * Convert.ToDouble(derecha.valor), new Tipo(Tipos.REAL, null), null);
                    }
                    else
                    {
                        throw new ErrorPascal("Operacion * entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }
                    return resultado;

                case "/":
                    if (tipoResultante == Tipos.INTEGER)
                    {
                        resultado = new Simbolo(Convert.ToInt32(izquierda.valor) / Convert.ToInt32(derecha.valor), new Tipo(Tipos.INTEGER, null), null);
                    }
                    else if (tipoResultante == Tipos.REAL)
                    {
                        resultado = new Simbolo(Convert.ToDouble(izquierda.valor) / Convert.ToDouble(derecha.valor), new Tipo(Tipos.REAL, null), null);
                    }
                    else
                    {
                        throw new ErrorPascal("Operacion / entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }
                    return resultado;
                case "%":
                    if (tipoResultante == Tipos.INTEGER)
                    {
                        resultado = new Simbolo(Convert.ToInt32(izquierda.valor) % Convert.ToInt32(derecha.valor), new Tipo(Tipos.INTEGER, null), null);
                    }
                    else if (tipoResultante == Tipos.REAL)
                    {
                        resultado = new Simbolo(Convert.ToDouble(izquierda.valor) % Convert.ToDouble(derecha.valor), new Tipo(Tipos.REAL, null), null);
                    }
                    else
                    {
                        throw new ErrorPascal("Operacion % entre Tipos de datos incorrectos [" + izquierda.tipo.tipo + ", " + derecha.tipo.tipo + "]", 0, 0, "Semantico");
                    }
                    return resultado;

                default:
                    throw new ErrorPascal("Operacion aritmetica desconocida", 0, 0, "Semantico");
            }
        }
    }
}


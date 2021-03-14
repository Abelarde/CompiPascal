using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.instruccion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;


//"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\VC\Tools\MSVC\14.28.29333\bin\Hostx64\x64\EDITBIN.EXE" /stack:4097152 CompiPascal.exe
namespace CompiPascal.traductor.expresion
{
    class FuncionLlamada : Expresion
    {
        private string id;
        private LinkedList<Expresion> lista_expresiones;
        string traductor_lista_exp;

        public FuncionLlamada(string id, LinkedList<Expresion> lista_expresiones)
        {
            this.id = id;
            this.lista_expresiones = lista_expresiones;
            this.traductor_lista_exp = string.Empty;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            Funcion funcion = retorna_funcionValor(id, entorno);            

            if (funcion != null)
            {
                LinkedList<Simbolo> valores = new LinkedList<Simbolo>();

                int contador = 1;
                foreach (Expresion valor in lista_expresiones)
                {
                    Simbolo val = valor.evaluar(entorno);
                    valores.AddLast(val);

                    if (validaciones(val))
                    {
                        if (val.id != null)
                        {
                            if (contador == valores.Count)
                                traductor_lista_exp += val.id;
                            else
                                traductor_lista_exp += val.id + ",";

                        }
                        else if (val.valor != null)
                        {
                            if (contador == valores.Count)
                            {
                                if (val.tipo.tipo == Tipos.STRING)
                                    traductor_lista_exp += "'" + Convert.ToString(val.valor) + "'";
                                else
                                    traductor_lista_exp += Convert.ToString(val.valor);
                            }
                            else
                            {
                                if (val.tipo.tipo == Tipos.STRING)
                                    traductor_lista_exp += "'" + Convert.ToString(val.valor) + "'" + ",";
                                else
                                    traductor_lista_exp += Convert.ToString(val.valor) + ",";
                            }

                        }
                    }


                    contador++;
                }

                funcion.valores_parametros_simbolos = valores;

                Entorno nuevo = new Entorno(entorno.getGlobal());

                object resultado = funcion.ejecutar(nuevo);
                if(resultado != null)
                {
                    if (resultado is ExitInstruccion)
                    {
                        ExitInstruccion a = resultado as ExitInstruccion;
                        if (a != null)
                            return a.simbolResultado;
                    }
                    else if (resultado is ReturnInstruccion)
                    {
                        ReturnInstruccion a = resultado as ReturnInstruccion;
                        if (a != null)
                            return a.simbolResultado;
                    }
                }
                return null;
            }
            //else
            //    throw new ErrorPascal("no existe una funcion con ese id", 0, 0, "semantico");

            string total = id + "(" + traductor_lista_exp + ")" + ";" + Environment.NewLine;

            Simbolo comodin = new Simbolo();
            comodin.id = null;
            comodin.valor = total;
            return comodin;
        }

        private Funcion retorna_funcionValor(string id, Entorno entorno)
        {
            if (entorno.getFuncion(id) != null)
                return entorno.getFuncion(id); 
            else
                return null;
        }

        private bool validaciones(Simbolo resultado)
        {
            if (resultado == null)
                throw new ErrorPascal("[Write] Error al obtener el simbolo a imprimir", 0, 0, "semantico");
            if (resultado.valor == null)
                throw new ErrorPascal("[Write] Error, el valor no ha sido definido aun para lo que desea imprimir", 0, 0, "semantico");

            return true;
        }
    }
}

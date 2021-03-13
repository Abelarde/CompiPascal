using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class CallFuncProc : Instruccion
    {
        private string id;
        private LinkedList<Expresion> lista_expresiones;

        public CallFuncProc(string id, LinkedList<Expresion> lista_expresiones)
        {
            this.id = id;
            this.lista_expresiones = lista_expresiones;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                if (id == "")
                    throw new ErrorPascal("el id viene especificado",0,0,"semantico");

                Funcion funcion = retorna_funcionValor(id, entorno);
                if (funcion != null)
                {
                    LinkedList<Simbolo> valores = new LinkedList<Simbolo>();

                    foreach (Expresion valor in lista_expresiones)
                    {
                        Simbolo val = valor.evaluar(entorno);
                        valores.AddLast(val);
                    }

                    funcion.valores_parametros_simbolos = valores;

                    Entorno nuevo = new Entorno(entorno.getGlobal());

                    object resultado = funcion.ejecutar(nuevo);

                    if (resultado != null)
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
                else
                    throw new ErrorPascal("no existe una funcion o metodo con ese id", 0, 0, "semantico");
            }
            catch (ErrorPascal ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                return null;
            }
        }

        private Funcion retorna_funcionValor(string id, Entorno entorno)
        {
            if (entorno.getFuncion(id) != null)
                return entorno.getFuncion(id);
            else
                return null;
        }



    }

}

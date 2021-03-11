using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.instruccion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class FuncionLlamada : Expresion
    {
        private string id;
        private LinkedList<Expresion> lista_expresiones;

        public FuncionLlamada(string id, LinkedList<Expresion> lista_expresiones)
        {
            this.id = id;
            this.lista_expresiones = lista_expresiones;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            Funcion funcion = retorna_funcionValor(id, entorno);

            if(funcion != null)
            {
                Entorno nuevo = new Entorno(entorno.getGlobal());

                int i = 0;
                foreach (Expresion exp in lista_expresiones)
                {
                    //TODO: validar que sean del mismo tipo y demas
                    Simbolo res = exp.evaluar(entorno);
                    funcion.setParametros(i, res);
                }

                object resultado = funcion.ejecutar(nuevo);
                if(resultado != null)
                {
                    if (resultado is ExitInstruccion)
                    {
                        ExitInstruccion a = resultado as ExitInstruccion;
                        if (a != null)
                            return a.resultado;
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
            {
                throw new ErrorPascal("no existe una funcion con ese id", 0, 0, "semantico");
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

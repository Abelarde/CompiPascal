using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.instruccion;
using CompiPascal.traductor.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.expresion
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
                LinkedList<Simbolo> valores = new LinkedList<Simbolo>();

                foreach(Expresion valor in lista_expresiones)
                {
                    Simbolo val = valor.evaluar(entorno);
                    valores.AddLast(val);
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
            else
                throw new ErrorPascal("no existe una funcion con ese id", 0, 0, "semantico");

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

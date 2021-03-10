using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.expresion
{
    class FuncionLlamada : Expresion
    {
        private Expresion id;
        private LinkedList<Expresion> lista_expresiones;

        public FuncionLlamada(Expresion id, LinkedList<Expresion> lista_expresiones)
        {
            this.id = id;
            this.lista_expresiones = lista_expresiones;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            //ir a traer la funcion con el id al entorno
                //en la tabla de funciones
            //ya lo tengo ejecuto sus instrucciones del header, body
            throw new NotImplementedException();
            //retornar el valor
        }

        private Simbolo retorna_funcionValor(string id, Entorno entorno)
        {
            if (entorno.getVariable(id) != null)
                return entorno.getVariable(id); //Simbolo -> primitivo, array, object
            else
                return null;
        }
    }
}

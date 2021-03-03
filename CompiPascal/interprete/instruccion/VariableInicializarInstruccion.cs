using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.expresion;
using CompiPascal.interprete.simbolo;
using CompiPascal.interprete.util;

namespace CompiPascal.interprete.instruccion
{
    class VariableInicializarInstruccion : Instruccion
    {
        private Expresion id;
        private Expresion valor;

        public VariableInicializarInstruccion(Expresion id, Expresion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public override object ejecutar(Entorno entorno)
        {
            if(id != null && valor != null)
            {
                Simbolo variable = this.id.evaluar(entorno); //id, tipo de la variable
                Simbolo valor = this.valor.evaluar(entorno); //valor, tipo de valro

                try
                {
                    if (validaciones(variable, valor))
                    {
                        variable.valor = valor.valor;
                    }
                }
                catch (ErrorPascal e)
                {
                    e.ToString();
                }


            }
            else
            {
                //errr al obtener informacion de la expresion
            }
            return null;
        }

        private bool validaciones(Simbolo variable, Simbolo valor)
        {
            //simbolo
            //tipo
            //valor
            if (variable != null)
            {
                if (valor != null)
                {
                    if (variable.tipo != null && valor.tipo != null)
                    {
                        if (variable.tipo.tipo == valor.tipo.tipo)
                        {
                            if (valor.valor != null)
                            {
                                return true;
                            }
                            else
                            {
                                throw new ErrorPascal("[Asignacion] el valor a asignar no existe aun", 0, 0, "semantico");
                            }
                        }
                        else
                        {
                            throw new ErrorPascal("[Asignacion] error los tipos no son compatibles entre la variable y lo que intentas asignar", 0, 0, "semantico");
                        }
                    }
                    else
                    {
                        throw new ErrorPascal("[Asignacion] error al obtener informacion del tipo de la variable a asignar y del valor", 0, 0, "semantico");
                    }
                }
                else
                {
                    throw new ErrorPascal("[Asignacion] error al obtener el valor para asignar a la varible", 0, 0, "semantico");
                }
            }
            else
            {
                throw new ErrorPascal("[Asignacion] la variable no existe, aun no ha sido declarada", 0, 0, "semantico");
            }
        }

    }
}

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
            try
            {

                if (id != null && valor != null)
                {
                    Simbolo variable = this.id.evaluar(entorno); //id, tipo de la variable
                    Simbolo valor = this.valor.evaluar(entorno); //valor, tipo de valor

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
                    throw new ErrorPascal("[Inicializacion] Error al obtener la expresion a ser asignada y/o la expresion asignatoria", 0, 0, "semantico");
                return null;

            }
            catch(ErrorPascal ex)
            {
                ErrorPascal.cola.Enqueue(ex.ToString());
                throw new ErrorPascal("error en la asignacion", 0, 0, "semantico");

            }
        }


        //TODO: ver que hacer cuando sea un object
        private bool validaciones(Simbolo variable, Simbolo valor)
        {
            if (variable == null)
                throw new ErrorPascal("[Asignacion] la variable no existe, aun no ha sido declarada", 0, 0, "semantico");
            if (valor == null)
                throw new ErrorPascal("[Asignacion] error al obtener el valor para asignar a la varible", 0, 0, "semantico");
            
            if (variable.isConst)
                throw new ErrorPascal("[Asignacion] error intentas asignar un valor a una constante", 0, 0, "semantico");
            if (variable.isType)
                throw new ErrorPascal("[Asignacion] error intentas asignar un valor a un type", 0, 0, "semantico");
            if (variable.tipo == null && valor.tipo == null)
                throw new ErrorPascal("[Asignacion] error al obtener informacion del tipo de la variable a asignar y el tipo del valor", 0, 0, "semantico");
            
            if (valor.valor == null)
                throw new ErrorPascal("[Asignacion] el valor a asignar no existe aun", 0, 0, "semantico");
            if (variable.tipo.tipo != valor.tipo.tipo)
                throw new ErrorPascal("[Asignacion] error los tipos no son compatibles entre la variable y lo que intentas asignar", 0, 0, "semantico");
            
            return true;
        }

    }
}

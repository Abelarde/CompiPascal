using CompiPascal.interprete.simbolo;

namespace CompiPascal.interprete.instruccion
{
    /// <summary>
    /// Clase abstracta para implementar las funcionalidades especificas
    /// de cada instruccion con el metodo ejecutar.
    /// </summary>
    abstract class Instruccion
    {
        //TODO: entorno local y/o global tambien

        public string print = string.Empty;
        /// <summary>
        /// Ejecuta las acciones especificas de cada instruccion
        /// </summary>
        /// <param name="entorno"></param>
        /// <returns></returns>
        public abstract object ejecutar(Entorno entorno);

    }
}

using CompiPascal.traductor.simbolo;

namespace CompiPascal.traductor.instruccion
{
    /// <summary>
    /// Clase abstracta para implementar las funcionalidades especificas
    /// de cada instruccion con el metodo ejecutar.
    /// </summary>
    abstract class Instruccion
    {
        /// <summary>
        /// Ejecuta las acciones especificas de cada instruccion
        /// </summary>
        /// <param name="entorno"></param>
        /// <returns></returns>
        public abstract object ejecutar(Entorno entorno);

    }
}

using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.simbolo;

namespace CompiPascal.interprete.expresion
{
    /// <summary>
    /// Clase abstracta para evaluar las diferentes expresiones
    /// en el lenguaje y asi devolver el Simbolo resultante
    /// </summary>
    abstract class Expresion
    {
        /// <summary>
        /// Evalua la expresion tomando en cuenta el entorno
        /// </summary>
        /// <param name="entorno"></param>
        /// <returns></returns>
        public abstract Simbolo evaluar(Entorno entorno);
    }
}

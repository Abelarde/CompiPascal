using System;
using System.Collections.Generic;
using System.Text;
using CompiPascal.interprete.analizador.simbolo;

namespace CompiPascal.interprete.util
{
    /// <summary>
    /// Definicion de combinaciones permitidas para los diferentes tipos
    /// de datos permitidos.
    /// </summary>
    class TablaTipos
    {
        /// <summary>
        /// conjugacion entre los tipos
        /// </summary>
        public static Tipos[,] tipos = new Tipos[5, 5]
        {
            { Tipos.REAL, Tipos.REAL, Tipos.ERROR, Tipos.ERROR, Tipos.ERROR},
            { Tipos.REAL,Tipos.INTEGER, Tipos.ERROR,Tipos.ERROR,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR, Tipos.BOOLEAN,Tipos.ERROR,Tipos.ERROR},           
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ARRAY,Tipos.ERROR},
            { Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.ERROR,Tipos.STRING}

        };

        /// <summary>
        /// retorna el tipo resultante
        /// </summary>
        /// <param name="izquierda"> Tipo del operando izquierdo</param>
        /// <param name="derecha"> Tipo del operando derecho</param>
        /// <returns></returns>
        public static Tipos GetTipo(Tipo izquierda, Tipo derecha)
        {
            return tipos[(int)izquierda.tipo, (int)derecha.tipo];
        }

    }
}


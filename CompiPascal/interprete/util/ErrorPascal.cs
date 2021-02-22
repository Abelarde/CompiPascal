using System;

namespace CompiPascal.interprete.util
{
    /// <summary>
    /// Clase creada para llevar nuestros propios errores en el lenguaje
    /// </summary>
    class ErrorPascal: Exception
    {
        private int linea, columna;
        private string mensaje;
        private string tipo;

        /// <summary>
        /// Se crea un nuevo error semantico
        /// </summary>
        /// <param name="mensaje">mensaje del error</param>
        /// <param name="linea">linea del error</param>
        /// <param name="columna">columna del error</param>
        /// <param name="tipo">tipo del error</param>
        public ErrorPascal(string mensaje, int linea, int columna, string tipo)
        {
            this.mensaje = mensaje;
            this.linea = linea;
            this.columna = columna;
            this.tipo = tipo;
        }

        /// <summary>
        /// Para poder presentar nuestro error de una manera mas presentable al usuario
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "semantico,"+this.mensaje+","+this.linea+","+this.columna+","+this.tipo;
        }
    }
}

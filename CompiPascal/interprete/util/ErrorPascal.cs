using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.util
{
    class ErrorPascal: Exception
    {
        private int linea, columna;
        private string mensaje;
        private string tipo;

        public ErrorPascal(string mensaje, int linea, int columna, string tipo)
        {
            this.mensaje = mensaje;
            this.linea = linea;
            this.columna = columna;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return "semantico,"+this.mensaje+","+this.linea+","+this.columna+","+this.tipo;
        }
    }
}

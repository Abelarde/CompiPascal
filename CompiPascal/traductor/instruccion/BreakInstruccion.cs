﻿using CompiPascal.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.interprete.instruccion
{
    class BreakInstruccion : Instruccion
    {
        bool isBreak;

        public BreakInstruccion(bool isBreak)
        {
            this.isBreak = isBreak;
        }
        public override object ejecutar(Entorno entorno)
        {
            return new BreakInstruccion(true);
        }
    }
}

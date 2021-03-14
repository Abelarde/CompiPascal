﻿using CompiPascal.traductor.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ContinueInstruccion : Instruccion
    {
        bool isContinue;
        public ContinueInstruccion(bool isContinue)
        {
            this.isContinue = isContinue;
        }
        public override object ejecutar(Entorno entorno)
        {
            return new ContinueInstruccion(true);
        }
    }
}

using CompiPascal.traductor.analizador.simbolo;
using CompiPascal.traductor.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.simbolo
{
    class Structs
    {
        private Dictionary<string, Simbolo> atributos;

        public Structs(Dictionary<string, Simbolo> atributos)
        {
            this.atributos = atributos;
        }

        public Structs()
        {
            this.atributos = new Dictionary<string, Simbolo>();
        }

        public Simbolo getAtributo(string idAtributo)
        {
            if (atributos.ContainsKey(idAtributo))
                return atributos[idAtributo];
            else
                throw new ErrorPascal("No existe un atributo con ese id en el object",0,0,"semantico");
        }

        public void setAtributo(string idAtributo, Simbolo valor)
        {
            if (!atributos.ContainsKey(idAtributo))
                atributos.Add(idAtributo, valor);
            else
                throw new ErrorPascal("Ya existe un atributo con ese id en el object",0,0,"semantico");
        }



    }
}

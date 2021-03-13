using CompiPascal.interprete.analizador.simbolo;
using CompiPascal.interprete.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CompiPascal.interprete.simbolo
{
    class Structs
    {
        public Dictionary<string, Simbolo> atributos;

        public Structs(Dictionary<string, Simbolo> atributos)
        {
            this.atributos = atributos;
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

        public Structs Clone()
        {
            return (Structs)this.MemberwiseClone();
        }
    }
}

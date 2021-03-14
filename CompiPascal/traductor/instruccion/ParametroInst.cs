using System;
using System.Collections.Generic;
using System.Text;

namespace CompiPascal.traductor.instruccion
{
    class ParametroInst
    {
        public LinkedList<string> lista_ids;
        public string tipo_nativo_id;
        public bool isReferencia;

        public ParametroInst(LinkedList<string> lista_ids, string tipo_nativo_id, bool isReferencia)
        {
            this.lista_ids = lista_ids;
            this.tipo_nativo_id = tipo_nativo_id;
            this.isReferencia = isReferencia;
        }
    }
}

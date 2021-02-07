using Irony.Parsing;

namespace CompiPascal
{
    class AstIronyDOT
    {
        private static int contador;
        private static string grafo;

        public static string getDot(ParseTreeNode actual)
        {
            grafo = "digraph G{\n";
            grafo += "node[shape=\"box\"]";
            grafo += "nodo0[label=\"" + escapar(actual.ToString()) + "\"];\n";
            contador = 1;
            recorrerAST("nodo0", actual);
            grafo += "}";
            return grafo;
        }

        private static string escapar(string cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }

        private static void recorrerAST(string padre, ParseTreeNode subArbol)
        {
            foreach (ParseTreeNode hijo in subArbol.ChildNodes)
            {
                string nombreHijo = "nodo" + contador.ToString();
                grafo += nombreHijo + "[label=\"" + escapar(hijo.ToString()) + "\"];\n";
                grafo += padre + "->" + nombreHijo + ";\n";
                contador++;
                recorrerAST(nombreHijo, hijo);
            }

        }
    }
}

using Irony.Parsing;
using System;
using System.Drawing;
using System.Linq;

namespace CompiPascal.traductor.analizador
{
    class SintacticoTraductor
    {
        private string txtOutput = string.Empty;

        public void analizar(String cadena)
        {
            //txtOutput = string.Empty;
            /* Como ya hemos mencionado, Irony no acepta acciones 
             * entre sus producciones, se limita a devolver el 
             * AST (Abstract Syntax Tree) que arma luego de ser 
             * aceptada la cadena de entrada.
             */
            
            //cargar el arbol 
            GramaticaTraductor gramatica = new GramaticaTraductor();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);

            //AST devuelto por Irony que sera posteriormente recorrido y analizado
            ParseTree arbol = parser.Parse(cadena);
            //cada uno de los nodos del ParseTree... nos interesara el atributo .ChildNodes
            ParseTreeNode raiz = arbol.Root;

            //ChildNodes... tipo Array y contiene todas las cualidades de una lista
            //si esta lista = vacia = nodo es una hoja
            // != entonces tiene un subarbol
            //se le manda el nodo RAIZ del arbol 
            //recorrido al AST ... se le manda el nodo raiz del arbol
            if (raiz != null)
            {
                //instrucciones(raiz.ChildNodes.ElementAt(0));
                OutputMessage("Analisis exitosamente");
            }
            else
            {
                Error treeError = new Error(arbol, raiz);
                    treeError.hayErrores();
                //TODO: necesitaria saber la linea, columan, token, estructura?, informacion del error en general.
                WarningMessage("La cadena de entrada no es correcta");
            }

        }

        public void graficar(String cadena)
        {
            //txtOutput = string.Empty;

            GramaticaTraductor gramatica = new GramaticaTraductor();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);

            //AST devuelto por Irony que sera posteriormente recorrido y analizado
            ParseTree arbol = parser.Parse(cadena);
            //cada uno de los nodos del ParseTree... nos interesara el atributo .ChildNodes
            ParseTreeNode raiz = arbol.Root;

            //ChildNodes... tipo Array y contiene todas las cualidades de una lista
            //si esta lista = vacia = nodo es una hoja
            // != entonces tiene un subarbol
            //se le manda el nodo RAIZ del arbol 
            //recorrido al AST ... se le manda el nodo raiz del arbol
            if (raiz != null)
            {
                graficarAstIrony(raiz, "ast");
            }
            else
            {
                //TODO: necesitaria saber la linea, columan, token, estructura?, informacion del error en general.
                WarningMessage("La cadena de entrada no es correcta");
            }

        }

        /* 1) cuantas producciones tiene
         * 2) condiciones para saber que produccion esta reconociendo [puede basarse en la cantidad de hijos de la produccion] 
         * 3) tambien la cantidad de ChildNodes o la posicion del elemento que nos interese puede variar DEPENDIENDO de las
         * preferencias que le colocamos al AST en nuestra gramatica, por ejemplo el metodo MarkPunctuation() nos elimina nodos
         * que no nos interesa y por lo tanto nos modifica la cantidad de los ChildNodes de nuestro arbol.
         */

        /* cuantas producciones tiene este NO TERMINAL */
        private void instrucciones(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 2)
            {
                instruccion(actual.ChildNodes.ElementAt(0));
                instrucciones(actual.ChildNodes.ElementAt(1));
            }
            else
            {
                instruccion(actual.ChildNodes.ElementAt(0));
            }
        }

        /* cuantas producciones tiene este NO TERMINAL */
        private void instruccion(ParseTreeNode actual)
        {
            OutputMessage("El valor de la expresion es: " + expresion(actual.ChildNodes.ElementAt(2)));
        }

        private double expresion(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                string tokenOperador = actual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (tokenOperador)
                {
                    case "+":
                        return expresion(actual.ChildNodes.ElementAt(0)) + expresion(actual.ChildNodes.ElementAt(2));
                    case "-":
                        return expresion(actual.ChildNodes.ElementAt(0)) - expresion(actual.ChildNodes.ElementAt(2));
                    case "*":
                        return expresion(actual.ChildNodes.ElementAt(0)) * expresion(actual.ChildNodes.ElementAt(2));
                    case "/":
                        return expresion(actual.ChildNodes.ElementAt(0)) / expresion(actual.ChildNodes.ElementAt(2));
                    default:
                        return expresion(actual.ChildNodes.ElementAt(1));
                }

            }
            else if (actual.ChildNodes.Count == 2)
            {
                return -1 * expresion(actual.ChildNodes.ElementAt(1));
            }
            else
            {
                return Double.Parse(actual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
            }
        }


        private void graficarAstIrony(ParseTreeNode actual, string nombreGrafica)
        {
            string grafoDOT = AstIronyDOT.getDot(actual);
            Bitmap bm = FileDotEngine.Run(grafoDOT, nombreGrafica);
            
            if(bm != null)
            {
                OutputMessage("Imagen generada exitosamente: " + "ast");
            }
            else
            {
                WarningMessage("Error al generar la grafica para: " + "ast");
            }

            /*
             * NOTE: esta libreria saca algunas excepciones respecto a la memoria, por lo tanto no se utiliza porque
             * ya no hay versiones mas actulizadas, en lugar de ello se utiliza la clase FileDotEngine.
             * Se deja comentado a fin de conocer que existe este otro metodo muy conocido pero fallido.
             * 
             * WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
             * WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDOT);
             * img.Save("C:\\Users\\eduab\\Desktop\\Compiladores 2\\Proyecto1\\Proyecto1\\output");
             * 
             */


        }

        private void OutputMessage(string msn) => txtOutput += "[OUTPUT]  " + msn + "\r\n";
       
        private void WarningMessage(string msn) => txtOutput += "[WARNING]  " + msn + "\r\n";

        public string Message() => txtOutput;

        public string ClearMessage() => txtOutput = string.Empty;


    }
}

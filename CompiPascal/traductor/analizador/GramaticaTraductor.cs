using Irony.Parsing;

namespace CompiPascal.traductor.analizador
{
    class GramaticaTraductor : Grammar
    {
        public GramaticaTraductor() : base(caseSensitive: false)
        {
            /* Expresiones regulares de los tokens que nuestra gramatica reconocera*/
            #region ER
            //var NUMERO = new NumberLiteral("Numero");
            //RegexBasedTerminal Numero = new RegexBasedTerminal("Numero", "[0-9]+");
            //IdentifierTerminal ID = new IdentifierTerminal("Id");
            //StringLiteral CADENA = TerminalFactory.CreateCSharpString("Cadena");
            //StringLiteral CADENA_OTRA_FORMA = new StringLiteral("Cadena_Otra_Forma", "\"");
            #endregion


            /* Conjunto de terminales que serán utilizados en nuestra gramática, que no fueron aceptados por ninguna de las expresiones regulares definidas anteriormente */
            #region Terminales
            var REVALUAR = ToTerm("Evaluar");
            var PTCOMA = ToTerm(";");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIVIDIDO = ToTerm("/");                       

            //precedencia y asociatividad
            //RegisterOperators(1, MAS, MENOS);
            //RegisterOperators(2, POR, DIVIDIDO);
            //MarkPunctuation("(", ")", "[", "]");


            //RegisterOperators(1, Associativity.Left,  MAS, MENOS);
            #endregion

            /* Conjunto de no terminales que serán utilizados en nuestra gramática. */
            #region No Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal expresion = new NonTerminal("expresion");
            #endregion

            //TODO: investigar mas sobre como definir las gramaticas y sobre el epsilo
            /* Región donde se define la gramática. */
            #region Gramatica
            ini.Rule = instrucciones;

            instrucciones.Rule = instruccion + instrucciones
                | instruccion;

            //TODO: investigar mas sobre estos comportamientos
            //instrucciones.Rule = MakePlusRule(instruccion, instrucciones);
            //instrucciones.Rule = MakePlusRule(instruccion, ToTerm(","), instrucciones);

            instruccion.Rule = REVALUAR + CORIZQ + expresion + CORDER + PTCOMA;

            expresion.Rule = MENOS + expresion
                | expresion + MAS + expresion
                | expresion + MENOS + expresion
                | expresion + POR + expresion
                | expresion + DIVIDIDO + expresion
                | NUMERO
                | ID
                | PARIZQ + expresion + PARDER;
            #endregion

            #region Preferencias
            /* Configuraciones especiales necesarias para el uso de Irony. */
            //this.Root = ini;
            //MarkTransient(instruccion);
            #endregion

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
    public class InvocacionParametro
    {
        public Token tokenparametro;
        public TipoDeDato tipodatoparametro;
    }

    public class Nodo
    {
        public Nodo hijoIzquierdo;
        public Nodo hijoCentro;
        public Nodo hijoDerecho;
        public Nodo Hermano;

        public string lexema;
        public string linea;
        public string operador;
        public string clase;
        public string metodo;
        public string lineametodo;
        public string codigop;
        public List<InvocacionParametro> parametros;
        public tipoExpresion nodoTipoExpresion = tipoExpresion.Vacio;
        public tipoSentencia nodoTipoSentencia = tipoSentencia.Vacio;
        public TipoDeDato tipodedato;
    }
    
    #region ENUMS
    public enum tipoExpresion
    {
        Aritmetica,
        Comparacion,
        Identificador,
        Constante,
        Cadena,
        Vacio
    }
    public enum tipoSentencia
    {
        Asignacion,
        If,
        For,
        Invocacion,
        Incremento,
        Vacio
    }
    #endregion

    class ArbolSintactico
    {
        List<Token> tokens;
        int puntero = 0;
        Token tokenactual;
        public static Nodo Arbol = new Nodo();
        public static string claseActual = "";
        public static string metodoActual = "";
        public static string lineametodoActual = "";
        public static string codigoP = "";
        public int contadornumeroetiqueta = 1;
        public void GenerarArbol(List<Token> _listatokens)
        {
            claseActual = "";
            metodoActual = "";
            lineametodoActual = "";
            tokens = _listatokens;
            Arbol = Sentencias();
            codigoP += Arbol.codigop;
        }

        public void SiguienteToken()
        {
            puntero++;
            if (puntero < tokens.Count)
            {
                tokenactual = tokens[puntero];
            }
            else
            {
                tokenactual = null;
            }

        }

        public Nodo Sentencias()
        {
            Nodo t = new Nodo();
            if (puntero < tokens.Count && puntero+1 != tokens.Count)
            {
                if (tokens[puntero].token == -1 && tokens[puntero + 1].token < -18 && tokens[puntero + 1].token > -27)
                {
                    t = Asignacion(); 
                }
                else if (tokens[puntero].token == -102)
                {
                    t = IF();
                }
                else if (tokens[puntero].token == -125)
                {
                    t = FOR();
                }
                if (tokens[puntero].token == -1 && tokens[puntero + 1].token == -30)
                {
                    Nodo p = Invocacion();
                    if (p.lexema != null)
                    {
                        t = p;
                    }
                    else
                    {
                        SiguienteToken();
                        t = Sentencias();
                    }

                }
                else
                {
                    if (tokens[puntero].lexema != "}")
                    {
                        if (tokens[puntero].token == -127)//class
                        {
                            claseActual = tokens[puntero + 1].lexema;
                            if (tokens[puntero + 3].token == -1)
                            {
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                t = Sentencias();
                            }
                            else
                            {
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                SiguienteToken();
                                t = Sentencias();
                            }
                        }
                        else if (tokens[puntero].token == -106)//import
                        {
                            SiguienteToken();
                            SiguienteToken();
                            t = Sentencias();
                        }
                        else
                        {
                            if (tokenactual.token == -101)
                            {
                                metodoActual = tokens[puntero + 1].lexema;//saber si entro en un metodo
                                lineametodoActual = tokens[puntero + 1].linea.ToString();
                            }
                            SiguienteToken();
                            t = Sentencias();
                        }

                    }
                    else if (puntero < tokens.Count - 2 && (tokens[puntero + 1].token == -127 || tokens[puntero + 1].token == -101))
                    {
                        SiguienteToken();
                        t = Sentencias();
                    }

                }
                

            }
            return t;
        }

        #region SENTENCIA ASIGNACION
        public Nodo Asignacion()
        {
            Nodo t = NuevoNodoSentencia();
            t.lexema = tokenactual.lexema;
            t.nodoTipoSentencia = tipoSentencia.Asignacion;
            t.operador = tokens[puntero + 1].lexema;
            t.clase = claseActual;
            t.metodo = metodoActual;
            t.lineametodo = lineametodoActual;
            //codigoP += "Lda "+tokenactual.lexema+"\n";
            SiguienteToken();
            SiguienteToken();
            t.hijoIzquierdo = SimpleExpresion();
            t.codigop = "Lda " + t.lexema + "\n"+t.hijoIzquierdo.codigop+ "Sto\n";
            //codigoP += "Sto\n";
            t.Hermano = Sentencias();
            t.codigop += t.Hermano.codigop;
            return t;
        }

        public Nodo SimpleExpresion()
        {
            Nodo t = Termino();
            while (tokens[puntero].lexema == "+" || tokens[puntero].lexema == "-")
            {
                Nodo p = NuevoNodoExpresion();
                p.lexema = tokenactual.lexema;
                p.nodoTipoExpresion = tipoExpresion.Aritmetica;
                p.linea = tokenactual.linea.ToString();
                p.hijoIzquierdo = t;
                t = p;
                SiguienteToken();
                t.hijoDerecho = Termino();
                if (t.lexema == "+")
                {
                    t.codigop = t.hijoIzquierdo.codigop + t.hijoDerecho.codigop + "Adi \n";
                    //codigoP += "Adi \n";
                }
                else if (t.lexema == "-")
                {
                    t.codigop = t.hijoIzquierdo.codigop + t.hijoDerecho.codigop + "Sbi \n";
                    //codigoP += "Sbi \n";
                }
            }

            return t;
        }

        public Nodo Termino()
        {
            Nodo t = Factor();
            while (tokens[puntero].lexema == "*"|| tokens[puntero].lexema == "/")
            {
                Nodo p = NuevoNodoExpresion();
                p.lexema = tokenactual.lexema;
                p.nodoTipoExpresion = tipoExpresion.Aritmetica;
                p.linea = tokenactual.linea.ToString();
                p.hijoIzquierdo = t;
                t = p;
                SiguienteToken();
                t.hijoDerecho = Factor();
                if (t.lexema == "*")
                {
                    t.codigop = t.hijoIzquierdo.codigop + t.hijoDerecho.codigop + "Mpi \n";
                    //codigoP += "Mpi \n";
                }
                else if (t.lexema == "/")
                {
                    t.codigop = t.hijoIzquierdo.codigop + t.hijoDerecho.codigop + "Div \n";
                    //codigoP += "Div \n";
                }
            }

            return t;
        }

        public Nodo Factor()
        {
            Nodo t = new Nodo();
            if (tokenactual.token == -2 || tokenactual.token == -3 || tokenactual.token == -4)
            {//Numero entero, decimal o cadena

                if (tokenactual.token == -2)//ENTERO
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokenactual.lexema;
                    t.nodoTipoExpresion = tipoExpresion.Constante;
                    t.tipodedato = TipoDeDato.Entero;
                    t.codigop = "Ldc "+tokenactual.lexema+"\n";
                    SiguienteToken();
                }
                else if (tokenactual.token == -3)//DECIMAL
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokenactual.lexema;
                    t.nodoTipoExpresion = tipoExpresion.Constante;
                    t.tipodedato = TipoDeDato.Decimal;
                    t.codigop = "Ldc " + tokenactual.lexema + "\n";
                    SiguienteToken();
                }
                else//CADENA
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokenactual.lexema;
                    t.nodoTipoExpresion = tipoExpresion.Constante;
                    t.tipodedato = TipoDeDato.Cadena;
                    
                    SiguienteToken();
                }
            }
            else if (tokenactual.token == -1)//IDENTIFICADOR
            {
                if (tokens[puntero + 1].token != -30)
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokenactual.lexema;
                    t.nodoTipoExpresion = tipoExpresion.Identificador;
                    t.tipodedato = TipoDeDato.SinEspecificar;
                    t.linea = tokenactual.linea.ToString();
                    t.clase = claseActual;
                    t.metodo = metodoActual;
                    t.lineametodo = lineametodoActual;
                    t.codigop = "Lod " + tokenactual.lexema + "\n";
                    SiguienteToken();
                }
                else
                {//INVOCACION
                    if (tokenactual.lexema == "time")
                    {
                        t = NuevoNodoExpresion();
                        t.lexema = DateTime.Now.ToString();
                        t.nodoTipoExpresion = tipoExpresion.Constante;
                        t.tipodedato = TipoDeDato.Cadena;
                        SiguienteToken();
                    }
                   
                }
                
            }
            return t;
        }
        #endregion

        #region SENTENCIA IF/ ELIF/ ELSE
        public Nodo IF()
        {
            Nodo t = NuevoNodoSentencia();
            if (tokenactual.token == -102)// IF
            {
                t.lexema = tokenactual.lexema;
                t.nodoTipoSentencia = tipoSentencia.If;
                SiguienteToken();
                t.hijoIzquierdo = SimpleCondicion();
                t.codigop = t.hijoIzquierdo.codigop;
                int _etiquefalse = contadornumeroetiqueta;
                contadornumeroetiqueta += 2;
                int _etiquetatrue = _etiquefalse + 1;
                t.codigop += "Fjp L"+_etiquefalse+" \n";
                SiguienteToken();
                SiguienteToken();
                t.hijoCentro = TrueCondicion();
                t.codigop += t.hijoCentro.codigop;
                SiguienteToken();
                t.codigop+= "Ujp L"+ _etiquetatrue + " \n";
                t.codigop += "Lab L"+_etiquefalse+" \n";
                t.hijoDerecho = FalseCondicion();
                t.codigop += t.hijoDerecho.codigop;
                t.codigop += "Lab L"+ _etiquetatrue + " \n";
                t.Hermano = Sentencias();
                t.codigop += t.Hermano.codigop;
            }
            else if (tokenactual.token == -109)//ELIF
            {
                t.lexema = tokenactual.lexema;
                t.nodoTipoSentencia = tipoSentencia.If;
                SiguienteToken();
                t.hijoIzquierdo = SimpleCondicion();
                t.codigop = t.hijoIzquierdo.codigop;
                int _etiquefalse = contadornumeroetiqueta;
                contadornumeroetiqueta += 2;
                int _etiquetatrue = _etiquefalse + 1;
                t.codigop += "Fjp L" + _etiquefalse + " \n";
                SiguienteToken();
                SiguienteToken();
                t.hijoCentro = TrueCondicion();
                t.codigop += t.hijoCentro.codigop;
                SiguienteToken();
                t.codigop += "Ujp L" + _etiquetatrue + " \n";
                t.codigop += "Lab L" + _etiquefalse + " \n";
                t.hijoDerecho = FalseCondicion();
                t.codigop += t.hijoDerecho.codigop;
                t.codigop += "Lab L" + _etiquetatrue + " \n";
            }
           
            return t;
        }
        
        public Nodo SimpleCondicion()
        {
            Nodo t = Condicion();
            while (tokenactual.lexema == "and" || tokenactual.lexema == "or")
            {
                Nodo p = NuevoNodoExpresion();
                p.lexema = tokenactual.lexema;
                p.nodoTipoExpresion = tipoExpresion.Comparacion;
                p.hijoIzquierdo = t;
                t = p;
                SiguienteToken();
                t.hijoDerecho = Condicion();
            }
            return t;
        }

        public Nodo Condicion()
        {
            Nodo t = SimpleExpresion();
            while (tokens[puntero].token > -19 && tokens[puntero].token < -11)
            {
                Nodo p = NuevoNodoExpresion();
                p.lexema = tokenactual.lexema;
                p.nodoTipoExpresion = tipoExpresion.Comparacion;
                p.hijoIzquierdo = t;
                t = p;
                SiguienteToken();
                t.hijoDerecho = SimpleExpresion();
                if (t.lexema == "==")
                {
                   t.codigop = t.hijoIzquierdo.codigop+t.hijoDerecho.codigop+"Equ \n";
                }
                
            }
            return t;
        }

        public Nodo TrueCondicion()
        {
            Nodo t = new Nodo();
            if (tokenactual.lexema != "}")
            {
                t =  Sentencias();
            }
            return t;
        }

        public Nodo FalseCondicion()
        {
            Nodo t = new Nodo();
            if (tokenactual != null)
            {
                if (tokenactual.lexema == "else")
                {
                    SiguienteToken();
                    SiguienteToken();
                    SiguienteToken();
                    if (tokenactual.lexema != "}")
                    {
                        t = NuevoNodoSentencia();
                        t.lexema = "else";
                        t.nodoTipoSentencia = tipoSentencia.If;
                        t.hijoIzquierdo = Sentencias();
                        t.codigop = t.hijoIzquierdo.codigop;
                        SiguienteToken();
                    }
                }
                else if (tokenactual.lexema == "elif")
                {
                    t = IF();
                    
                }
            }
            

            return t;
        }
        #endregion

        #region SENTECIA FOR
        public Nodo FOR()
        {
            Nodo t = NuevoNodoSentencia();
            t.lexema = tokenactual.lexema;
            t.nodoTipoSentencia = tipoSentencia.For;
            SiguienteToken();
            t.hijoIzquierdo = DeclaracionFor();
            t.codigop = t.hijoIzquierdo.codigop;
            int _etiqueciclo = contadornumeroetiqueta;
            contadornumeroetiqueta += 2;
            int _etiquesalirciclo = _etiqueciclo + 1;
            t.codigop += "Lab L"+_etiqueciclo+"\n";
            SiguienteToken();
            t.hijoCentro = ComparacionFor();

            t.hijoDerecho = SntenciasEIncrementar();
            SiguienteToken();
            t.Hermano = Sentencias();
            return t;
        }

        private Nodo SntenciasEIncrementar()
        {
            Nodo t = NuevoNodoSentencia();
            t.lexema = "SntenciasEIncrementar";
            t.nodoTipoSentencia = tipoSentencia.Incremento;
            t.hijoDerecho = Incrementar();
            t.hijoIzquierdo = Sentencias();
            return t;
        }

        private Nodo Incrementar()
        {
            Nodo t = new Nodo();
            if (tokenactual.token == -29) // for id in range(4):
            {
                t = NuevoNodoSentencia();
                t.lexema = tokens[puntero - 6].lexema;
                t.nodoTipoSentencia = tipoSentencia.Asignacion;
                t.operador = "=";
                t.clase = claseActual;
                t.metodo = metodoActual;
                t.lineametodo = lineametodoActual;
                t.hijoIzquierdo = TerminoForIncrementar();
            }
            else if (tokenactual.token == -32) // for id in range(4,5,6):
            {
                t = NuevoNodoSentencia();
                t.lexema = tokens[puntero - 7].lexema;
                t.nodoTipoSentencia = tipoSentencia.Asignacion;
                t.operador = "=";
                t.clase = claseActual;
                t.metodo = metodoActual;
                t.lineametodo = lineametodoActual;
                t.hijoIzquierdo = TerminoForIncrementar();
            }
            else if (tokenactual.token == -34) // for id in range(4,6):
            {
                t = NuevoNodoSentencia();
                t.lexema = tokens[puntero - 7].lexema;
                t.nodoTipoSentencia = tipoSentencia.Asignacion;
                t.operador = "=";
                t.clase = claseActual;
                t.metodo = metodoActual;
                t.lineametodo = lineametodoActual;
                t.hijoIzquierdo = TerminoForIncrementar();
            }
            return t;
        }

        private Nodo TerminoForIncrementar()
        {
            Nodo t =NuevoNodoExpresion();
            t.lexema = "+";
            t.nodoTipoExpresion = tipoExpresion.Aritmetica;
            t.linea = tokenactual.linea.ToString();
            t.hijoIzquierdo = FactorForIncrementar();
            t.hijoDerecho = FactorForIncrementar();
            return t;
        }

        private Nodo FactorForIncrementar()
        {
            Nodo t = new Nodo();
            if ((tokenactual.token == -29 && tokens[puntero - 3].token != -32) || tokens[puntero - 1].token == -29)
            {//for x in range(0):
                if (tokens[puntero - 1].token == -29)
                {
                    t = NuevoNodoExpresion();
                    t.lexema = "1";
                    t.nodoTipoExpresion = tipoExpresion.Constante;
                    t.tipodedato = TipoDeDato.Entero;
                    SiguienteToken();
                }
                else
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokens[puntero - 6].lexema;
                    t.nodoTipoExpresion = tipoExpresion.Identificador;
                    t.tipodedato = TipoDeDato.SinEspecificar;
                    t.clase = claseActual;
                    t.metodo = metodoActual;
                    t.lineametodo = lineametodoActual;
                    SiguienteToken();
                }

            }
            else if (tokenactual.token == -32 || tokens[puntero - 1].token == -32)
            {//for x in range(3,4,5):
                if (tokens[puntero - 1].token == -32)
                {
                    if (tokenactual.token == -2 || tokenactual.token == -3 || tokenactual.token == -4)
                    {//Numero entero, decimal o cadena
                        if (tokenactual.token == -2)//ENTERO
                        {
                            t = NuevoNodoExpresion();
                            t.lexema = tokenactual.lexema;
                            t.nodoTipoExpresion = tipoExpresion.Constante;
                            t.tipodedato = TipoDeDato.Entero;
                        }
                        else if (tokenactual.token == -3)//DECIMAL
                        {
                            t = NuevoNodoExpresion();
                            t.lexema = tokenactual.lexema;
                            t.nodoTipoExpresion = tipoExpresion.Constante;
                            t.tipodedato = TipoDeDato.Decimal;
                        }
                        else//CADENA
                        {
                            t = NuevoNodoExpresion();
                            t.lexema = tokenactual.lexema;
                            t.nodoTipoExpresion = tipoExpresion.Constante;
                            t.tipodedato = TipoDeDato.Cadena;
                        }
                    }
                    else if (tokenactual.token == -1)//IDENTIFICADOR
                    {
                        t = NuevoNodoExpresion();
                        t.lexema = tokenactual.lexema;
                        t.nodoTipoExpresion = tipoExpresion.Identificador;
                        t.tipodedato = TipoDeDato.SinEspecificar;
                        t.clase = claseActual;
                        t.metodo = metodoActual;
                        t.lineametodo = lineametodoActual;
                    }

                   
                    SiguienteToken();
                    SiguienteToken();
                    SiguienteToken();
                    SiguienteToken();
                }
                else
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokens[puntero - 7].lexema;
                    t.nodoTipoExpresion = tipoExpresion.Identificador;
                    t.tipodedato = TipoDeDato.SinEspecificar;
                    t.clase = claseActual;
                    t.metodo = metodoActual;
                    t.lineametodo = lineametodoActual;
                    SiguienteToken();
                }
            }
            else if (tokenactual.token == -34 || tokens[puntero - 1].token == -34)
            {
                if (tokens[puntero - 1].token == -34)
                {
                    t = NuevoNodoExpresion();
                    t.lexema = "1";
                    t.nodoTipoExpresion = tipoExpresion.Constante;
                    t.tipodedato = TipoDeDato.Entero;
                    SiguienteToken();
                    SiguienteToken();
                }
                else
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokens[puntero - 7].lexema;
                    t.nodoTipoExpresion = tipoExpresion.Identificador;
                    t.tipodedato = TipoDeDato.SinEspecificar;
                    t.clase = claseActual;
                    t.metodo = metodoActual;
                    t.lineametodo = lineametodoActual;
                    SiguienteToken();
                }
            }
            return t;
        }

        private Nodo ComparacionFor()
        {
            Nodo t = new Nodo();
            if (tokenactual.token == -32)
            {
                t = NuevoNodoExpresion();
                t.nodoTipoExpresion = tipoExpresion.Comparacion;
                t.lexema = "<";
                t.hijoIzquierdo = FactorComparacionFor();
                t.hijoDerecho = FactorComparacionFor();
                t.codigop = t.hijoIzquierdo.codigop + t.hijoDerecho.codigop + "grt \n";
            }
            else if (tokenactual.token == -34)
            {
                t = NuevoNodoExpresion();
                t.nodoTipoExpresion = tipoExpresion.Comparacion;
                t.lexema = "<";
                t.hijoIzquierdo = FactorComparacionFor();
                t.hijoDerecho = FactorComparacionFor();
                t.codigop = t.hijoIzquierdo.codigop + t.hijoDerecho.codigop + "grt \n";
            }
           
            return t;
        }

        private Nodo FactorComparacionFor()
        {
            Nodo t = new Nodo();
            if (tokenactual.token == -32 || tokens[puntero - 1].token == -32)
            {
                if (tokens[puntero - 1].token == -32)
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokenactual.lexema;
                    SiguienteToken();
                }
                else
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokens[puntero - 5].lexema;
                    SiguienteToken();
                }
                
            }
            else if (tokenactual.token == -34 || tokenactual.token == -29)
            {
                if (tokenactual.token == -29)
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokens[puntero - 2].lexema;
                }
                else
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokens[puntero - 5].lexema;
                    SiguienteToken();
                }
            }
            return t;
        }

        private Nodo DeclaracionFor()
        {
            Nodo t = NuevoNodoSentencia();
            t.lexema = tokenactual.lexema;
            t.nodoTipoSentencia = tipoSentencia.Asignacion;
            t.operador = "=";
            t.clase = claseActual;
            t.metodo = metodoActual;
            t.lineametodo = lineametodoActual;
            SiguienteToken();
            SiguienteToken();
            SiguienteToken();
            SiguienteToken();
            t.hijoIzquierdo = FactorFor();
            t.codigop = "Lda " + t.lexema + "\n" + t.hijoIzquierdo.codigop + "Sto\n";
            return t;

        }

        private Nodo FactorFor()
        {
            Nodo t = new Nodo();
            if (tokens[puntero + 1].token == -32)// Parametro seguido de una coma
            {
                if (tokenactual.token == -2 || tokenactual.token == -3 || tokenactual.token == -4)//Numero entero, decimal o cadena
                {
                    if (tokenactual.token == -2)//ENTERO
                    {
                        t = NuevoNodoExpresion();
                        t.lexema = tokenactual.lexema;
                        t.nodoTipoExpresion = tipoExpresion.Constante;
                        t.tipodedato = TipoDeDato.Entero;
                        t.codigop = "Ldc " + tokenactual.lexema + "\n";
                    }
                    else if (tokenactual.token == -3)//DECIMAL
                    {
                        t = NuevoNodoExpresion();
                        t.lexema = tokenactual.lexema;
                        t.nodoTipoExpresion = tipoExpresion.Constante;
                        t.tipodedato = TipoDeDato.Decimal;
                        t.codigop = "Ldc " + tokenactual.lexema + "\n";
                    }
                    else //CADENA
                    {
                        t = NuevoNodoExpresion();
                        t.lexema = tokenactual.lexema;
                        t.nodoTipoExpresion = tipoExpresion.Constante;
                        t.tipodedato = TipoDeDato.Cadena;
                    }
                }
                else if (tokenactual.token == -1)//ID
                {
                    t = NuevoNodoExpresion();
                    t.lexema = tokenactual.lexema;
                    t.nodoTipoExpresion = tipoExpresion.Identificador;
                    t.tipodedato = TipoDeDato.SinEspecificar;
                    t.clase = claseActual;
                    t.metodo = metodoActual;
                    t.lineametodo = lineametodoActual;
                    t.codigop = "Lod " + tokenactual.lexema + "\n";
                }
            }
            else if (tokens[puntero + 1].token == -34)//Parametro seguido de un cierre de parentesis
            {
                t = NuevoNodoExpresion();
                t.lexema = "0";
                t.nodoTipoExpresion = tipoExpresion.Constante;
                t.tipodedato = TipoDeDato.Entero;
                t.codigop = "Ldc " + tokenactual.lexema + "\n";
            }
            return t;
        }


        #endregion

        #region SENTENCIA INVOCACION
        public Nodo Invocacion()
        {
            Nodo t = NuevoNodoSentencia();
            t.nodoTipoSentencia = tipoSentencia.Invocacion;
            if (tokenactual.lexema == "selff")
            {
                SiguienteToken();
                SiguienteToken();
                t.lexema = tokenactual.lexema;
                t.clase = claseActual;
                t.metodo = metodoActual;
                t.linea = tokenactual.linea.ToString();
                t.lineametodo = lineametodoActual;
                SiguienteToken();
                SiguienteToken();
                if (tokenactual.token != -34)
                {
                    while (tokenactual.token != -34)//MIENTRAS SEA DIFERENTE A )
                    {
                        if (tokenactual.token != -32)//MIENTRAS SEA DIFERENTE A ,
                        {
                            switch (tokenactual.token)
                            {
                                case -1:
                                    t.parametros.Add(new InvocacionParametro() { tokenparametro = tokenactual, tipodatoparametro = TipoDeDato.SinEspecificar });
                                    break;
                                case -2:
                                    t.parametros.Add(new InvocacionParametro() { tokenparametro = tokenactual, tipodatoparametro = TipoDeDato.Entero });
                                    break;
                                case -3:
                                    t.parametros.Add(new InvocacionParametro() { tokenparametro = tokenactual, tipodatoparametro = TipoDeDato.Decimal});
                                    break;
                                case -4:
                                    t.parametros.Add(new InvocacionParametro() { tokenparametro = tokenactual, tipodatoparametro = TipoDeDato.Cadena });
                                    break;
                            }
                           
                        }
                        SiguienteToken();
                    }
                }
                t.Hermano = Sentencias();
                t.codigop = t.Hermano.codigop;
            }
            return t;
        }
        #endregion

        #region NUEVOS NODOS 
        public Nodo NuevoNodoExpresion()
        {
            Nodo t = new Nodo();
            t.hijoDerecho = new Nodo();
            t.hijoIzquierdo = new Nodo();
            t.nodoTipoExpresion = tipoExpresion.Vacio;
            t.nodoTipoSentencia = tipoSentencia.Vacio;
            t.tipodedato = TipoDeDato.Vacio;
            return t;
        }

        public Nodo NuevoNodoSentencia()
        {
            Nodo t = new Nodo();
            t.Hermano = new Nodo();
            t.hijoIzquierdo = new Nodo();
            t.hijoCentro = new Nodo();
            t.hijoDerecho = new Nodo();
            t.parametros = new List<InvocacionParametro>();
            t.nodoTipoExpresion = tipoExpresion.Vacio;
            t.nodoTipoSentencia = tipoSentencia.Vacio;
            t.tipodedato = TipoDeDato.Vacio;
            return t;
        }


        #endregion
    }
}

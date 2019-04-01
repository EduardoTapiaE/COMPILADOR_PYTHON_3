using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
    public class ResultadoComparacionDeTipo
    {
        public TipoDeDato tipodedato;
        public string valor;
    }
    public class ComprobacionDeTipos:TablaSimbolos
    {
        public static Nodo ArbolComprobacionDeTipos = new Nodo();

        public void EjecutarComprobacionDeTipos()
        {
            try
            {
                RecorridoComprobacionDeTipos(ArbolComprobacionDeTipos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public void RecorridoComprobacionDeTipos(Nodo _ArbolRecorrer)
        {

            if (_ArbolRecorrer.hijoIzquierdo != null)
            {
                if (_ArbolRecorrer.hijoIzquierdo.lexema != string.Empty)
                {
                    RecorridoComprobacionDeTipos(_ArbolRecorrer.hijoIzquierdo);
                }
            }

            if (_ArbolRecorrer.hijoCentro != null)
            {
                if (_ArbolRecorrer.hijoCentro.lexema != string.Empty)
                {
                    RecorridoComprobacionDeTipos(_ArbolRecorrer.hijoCentro);
                }
            }

            if (_ArbolRecorrer.hijoDerecho != null)
            {
                if (_ArbolRecorrer.hijoDerecho.lexema != string.Empty)
                {
                    RecorridoComprobacionDeTipos(_ArbolRecorrer.hijoDerecho);
                }
            }


            if (_ArbolRecorrer.lexema != null)
            {
                if (_ArbolRecorrer.nodoTipoSentencia != tipoSentencia.Incremento)
                {
                    //ASIGNAR TIPO DE DATO EN TABLA DE SIMBOLOS
                    if (_ArbolRecorrer.nodoTipoSentencia == tipoSentencia.Asignacion)
                    {
                        if (_ArbolRecorrer.metodo != "")
                        {//ASIGNAR TIPO A VARIABLES DEL METODO
                            AsignarTipoDeDatoAVariable(_ArbolRecorrer);
                        }
                        else
                        {//ASIGNAR TIPO ATRIBUTOS DE LA CLASE
                            AsignarTipoDeDatoAtributo(_ArbolRecorrer);
                        }

                    }
                    else if (_ArbolRecorrer.nodoTipoSentencia == tipoSentencia.Invocacion)
                    {//ASIGNAR TIPO DE DATOS A PARAMETROS MEDIANTE INVOCACION
                        AsignarTipoDeDatoParametros(_ArbolRecorrer);
                    }

                    //OBTENER TIPO DE DATO DE TABLA DE SIMBOLOS
                    if (_ArbolRecorrer.nodoTipoExpresion == tipoExpresion.Identificador)
                    {
                        _ArbolRecorrer.tipodedato = ObtenerTipoDeDato(_ArbolRecorrer); 
                    }

                    //COMPARAR LOS TIPOS DE DATOS
                    if (_ArbolRecorrer.nodoTipoExpresion == tipoExpresion.Aritmetica || _ArbolRecorrer.nodoTipoExpresion == tipoExpresion.Comparacion)
                    {
                        ComprobarTipoDeDatos(_ArbolRecorrer);
                    }
                    
                }

                if (_ArbolRecorrer.Hermano != null)
                {
                    if (_ArbolRecorrer.Hermano.lexema != string.Empty)
                    {
                        RecorridoComprobacionDeTipos(_ArbolRecorrer.Hermano);
                    }
                }
            }


        }


        private TipoDeDato ObtenerTipoDeDato(Nodo _nodoidentificador)
        {
            if (_nodoidentificador.metodo == "")
            {//BUSCAR SOLO EN LOS ATRIBUTOS
                string _idclase = _nodoidentificador.clase;
                string _idAtributo = _nodoidentificador.lexema;
                NodoClase _clase = ObtenerNodoClase(_idclase);
                NodoAtributo _atributo = ObtenerNodoAtributo(_clase,_idAtributo);
                return _atributo.tipodato;
            }
            else
            {//BUSCAR EN VARIABLES Y PARAMETROS
                string _idclase = _nodoidentificador.clase;
                string _idmetodo = _nodoidentificador.metodo+"*"+_nodoidentificador.lineametodo;
                string _idVariable = _nodoidentificador.lexema;
                NodoClase _clase = ObtenerNodoClase(_idclase);
                NodoMetodo _metodo = ObtenerNodoMetodo(_clase,_idmetodo);
                NodoVariable _variable = ObtenerNodoVariable(_metodo,_idVariable);
                if (_variable != null)
                {
                    return _variable.tipodato;
                }
                else
                {
                    NodoParametro _parametro = ObtenerNodoParametro(_metodo,_idVariable);
                    if (_parametro != null)
                    {
                        return _parametro.tipodato;
                    }
                    else
                    {
                        throw new Exception("Error Semantico linea "+ _nodoidentificador.linea + ": No existe el nombre de la variable");
                    }
                }
            }
        }

        private void AsignarTipoDeDatoAtributo(Nodo _nodoAsignacion)
        {
            string _idclase = _nodoAsignacion.clase;
            string _idAtributo = _nodoAsignacion.lexema;
            TipoDeDato _tipodato = _nodoAsignacion.hijoIzquierdo.tipodedato;
            NodoClase _clase = ObtenerNodoClase(_idclase);
            NodoAtributo _atributo = ObtenerNodoAtributo(_clase,_idAtributo);
            _atributo.tipodato = _tipodato;
            InsertarAtributoModificado(_clase,_atributo);
        }

        private Estado InsertarAtributoModificado(NodoClase _clase, NodoAtributo _atributo)
        {
            if (tablaDeSimbolosClase.ContainsKey(_clase.lexema))
            {
                if (_clase.tablaDeSimbolosAtributos.ContainsKey(_atributo.lexema))
                {
                    _clase.tablaDeSimbolosAtributos.Remove(_atributo.lexema);
                    _clase.tablaDeSimbolosAtributos.Add(_atributo.lexema, _atributo);
                    tablaDeSimbolosClase.Remove(_clase.lexema);
                    InsertarNodoClase(_clase);
                    return Estado.Insertado;
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del atributo");
                }

            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }

        private void AsignarTipoDeDatoParametros(Nodo _nodoInvocacion)
        {
            string _idclase = _nodoInvocacion.clase;
            string _idmetodo = _nodoInvocacion.lexema;
            NodoClase _clase = ObtenerNodoClase(_idclase);
            NodoMetodo _metodobusqueda = ObtenerMetodoInvocacion(_clase,_nodoInvocacion);
            InsertarParametrosModificados(_clase,_metodobusqueda);
        }

        private Estado InsertarParametrosModificados(NodoClase _clase, NodoMetodo _metodo)
        {
            if (tablaDeSimbolosClase.ContainsKey(_clase.lexema))
            {
                if (_clase.tablaDeSimbolosMetodos.ContainsKey(_metodo.lexema))
                {
                    _clase.tablaDeSimbolosMetodos.Remove(_metodo.lexema);
                    _clase.tablaDeSimbolosMetodos.Add(_metodo.lexema, _metodo);
                    tablaDeSimbolosClase.Remove(_clase.lexema);
                    tablaDeSimbolosClase.Add(_clase.lexema, _clase);
                    return Estado.Insertado;
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del Metodo");
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }

        private void AsignarTipoDeDatoAVariable(Nodo _nodoAsignacion)
        {
            string _idclase = _nodoAsignacion.clase;
            string _idmetodo = _nodoAsignacion.metodo+"*"+_nodoAsignacion.lineametodo;
            string _idVariable = _nodoAsignacion.lexema;
            TipoDeDato _tipodato = _nodoAsignacion.hijoIzquierdo.tipodedato;
            NodoClase _clase = ObtenerNodoClase(_idclase);
            NodoMetodo _metodo = ObtenerNodoMetodo(_clase,_idmetodo);
            NodoVariable _variable = ObtenerNodoVariable(_metodo, _idVariable);
            if (_variable != null)
            {
                _variable.tipodato = _tipodato;
                InsertarVariableModificada(_clase, _metodo, _variable);
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de la variable");
            }
           
        }

        private Estado InsertarVariableModificada(NodoClase _clase, NodoMetodo _metodo, NodoVariable _variable)
        {
            if (tablaDeSimbolosClase.ContainsKey(_clase.lexema))
            {
                if (_clase.tablaDeSimbolosMetodos.ContainsKey(_metodo.lexema))
                {
                    if (_metodo.tabalaDeSimbolosVariables.ContainsKey(_variable.lexema))
                    {
                        _metodo.tabalaDeSimbolosVariables.Remove(_variable.lexema);
                        _metodo.tabalaDeSimbolosVariables.Add(_variable.lexema, _variable);
                        _clase.tablaDeSimbolosMetodos.Remove(_metodo.lexema);
                        _clase.tablaDeSimbolosMetodos.Add(_metodo.lexema, _metodo);
                        tablaDeSimbolosClase.Remove(_clase.lexema);
                        tablaDeSimbolosClase.Add(_clase.lexema, _clase);
                        return Estado.Insertado;
                    }
                    else
                    {
                        throw new Exception("Error Semantico: No existe el nombre de la variable");
                    }
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del Metodo");
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }

        private void ComprobarTipoDeDatos(Nodo _ArbolRecorrer)
        {
            if (_ArbolRecorrer.nodoTipoExpresion == tipoExpresion.Aritmetica)
            {//COMPROBAR DATOS EN OPERACIONES ARITMETICAS
                string _operadorAritmetico = _ArbolRecorrer.lexema;
                string _linea = _ArbolRecorrer.linea;
                Nodo _hijoIzquierdo = _ArbolRecorrer.hijoIzquierdo;
                Nodo _hijoDerecho = _ArbolRecorrer.hijoDerecho;
                ResultadoComparacionDeTipo _resultado = CompararTipoDeDatosOpAritmetica(_operadorAritmetico, _linea, _hijoIzquierdo, _hijoDerecho);
                if (_resultado != null)
                {
                    _ArbolRecorrer.tipodedato = _resultado.tipodedato;
                }

            }
            else if (_ArbolRecorrer.nodoTipoExpresion == tipoExpresion.Comparacion)
            {//COMPROBAR DATOS EN OPERACIONES DE COMPARACION
                string _operadorDeComparacion = _ArbolRecorrer.lexema;
                CompararTipoDeDatosOpDeComparacion(_operadorDeComparacion);
            }
        }

        #region COMPARACIONES
        private ResultadoComparacionDeTipo CompararTipoDeDatosOpAritmetica(string _operador, string _linea,Nodo _hijoIzq, Nodo _hijoDer)
        {
            ResultadoComparacionDeTipo _result = new ResultadoComparacionDeTipo();
            switch (_operador)
            {
                case "+":
                    if (_hijoIzq.tipodedato == _hijoDer.tipodedato && (_hijoIzq.tipodedato != TipoDeDato.Error))
                    {//TIPO DE DATOS IGUALES SIN TIPO ERROR
                        _result.tipodedato = _hijoIzq.tipodedato;
                    }
                    else if ((_hijoIzq.tipodedato == TipoDeDato.Decimal && _hijoDer.tipodedato == TipoDeDato.Entero) || (_hijoIzq.tipodedato == TipoDeDato.Entero && _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//DECIMAL+ENTERO O ENTERO+DECIMAL
                        _result.tipodedato = TipoDeDato.Decimal;
                    }
                    else
                    {
                        _result.tipodedato = TipoDeDato.Error;
                        MessageBox.Show("Error Semantico liena "+_linea+": No se puede sumar "+_hijoIzq.tipodedato.ToString() +" + "+_hijoDer.tipodedato.ToString());
                    }
                    
                    return _result;
                case "-":
                    if (_hijoIzq.tipodedato == _hijoDer.tipodedato && (_hijoIzq.tipodedato == TipoDeDato.Entero || _hijoIzq.tipodedato == TipoDeDato.Decimal) && (_hijoDer.tipodedato == TipoDeDato.Entero || _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//TIPO DE DATOS IGUALES y TIPO DE DATOS HIJOIZQ ES ENTERO O DECIMAL y TIPO DE DATOS HIJODER ES ENTERO O DECIMAL
                        _result.tipodedato = _hijoIzq.tipodedato;
                    }
                    else if ((_hijoIzq.tipodedato == TipoDeDato.Decimal && _hijoDer.tipodedato == TipoDeDato.Entero) || (_hijoIzq.tipodedato == TipoDeDato.Entero && _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//DECIMAL-ENTERO O ENTERO-DECIMAL
                        _result.tipodedato = TipoDeDato.Decimal;
                    }
                    else
                    {
                        _result.tipodedato = TipoDeDato.Error;
                        MessageBox.Show("Error Semantico liena " + _linea + ": No se puede restar " + _hijoIzq.tipodedato.ToString() + " - " + _hijoDer.tipodedato.ToString());
                    }

                    return _result;
                case "*":
                    if (_hijoIzq.tipodedato == _hijoDer.tipodedato && (_hijoIzq.tipodedato == TipoDeDato.Entero || _hijoIzq.tipodedato == TipoDeDato.Decimal) && (_hijoDer.tipodedato == TipoDeDato.Entero || _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//TIPO DE DATOS IGUALES y TIPO DE DATOS HIJOIZQ ES ENTERO O DECIMAL y TIPO DE DATOS HIJODER ES ENTERO O DECIMAL
                        _result.tipodedato = _hijoIzq.tipodedato;
                    }
                    else if ((_hijoIzq.tipodedato == TipoDeDato.Decimal && _hijoDer.tipodedato == TipoDeDato.Entero) || (_hijoIzq.tipodedato == TipoDeDato.Entero && _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//DECIMAL-ENTERO O ENTERO-DECIMAL
                        _result.tipodedato = TipoDeDato.Decimal;
                    }
                    else
                    {
                        _result.tipodedato = TipoDeDato.Error;
                        MessageBox.Show("Error Semantico liena " + _linea + ": No se puede multiplicar " + _hijoIzq.tipodedato.ToString() + " * " + _hijoDer.tipodedato.ToString());
                    }

                    return _result;
                case "/":
                    if (_hijoIzq.tipodedato == _hijoDer.tipodedato && (_hijoIzq.tipodedato == TipoDeDato.Entero || _hijoIzq.tipodedato == TipoDeDato.Decimal) && (_hijoDer.tipodedato == TipoDeDato.Entero || _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//TIPO DE DATOS IGUALES y TIPO DE DATOS HIJOIZQ ES ENTERO O DECIMAL y TIPO DE DATOS HIJODER ES ENTERO O DECIMAL
                        _result.tipodedato = TipoDeDato.Decimal;
                    }
                    else if ((_hijoIzq.tipodedato == TipoDeDato.Decimal && _hijoDer.tipodedato == TipoDeDato.Entero) || (_hijoIzq.tipodedato == TipoDeDato.Entero && _hijoDer.tipodedato == TipoDeDato.Decimal))
                    {//DECIMAL-ENTERO O ENTERO-DECIMAL
                        _result.tipodedato = TipoDeDato.Decimal;
                    }
                    else
                    {
                        _result.tipodedato = TipoDeDato.Error;
                        MessageBox.Show("Error Semantico liena " + _linea + ": No se puede dividir " + _hijoIzq.tipodedato.ToString() + " / " + _hijoDer.tipodedato.ToString());
                    }

                    return _result;

                default:
                    return null;

            }
        }

        private void CompararTipoDeDatosOpDeComparacion(string _operador)
        {
            ResultadoComparacionDeTipo _result = new ResultadoComparacionDeTipo();
            switch (_operador)
            {

            }
        }
        #endregion

    }
}

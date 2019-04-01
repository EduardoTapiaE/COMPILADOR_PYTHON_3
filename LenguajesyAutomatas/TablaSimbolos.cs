using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenguajesyAutomatas
{
    public class NodoClase
    {
        public string lexema;
        public NodoClase herencia;
        public Dictionary<object, NodoAtributo> tablaDeSimbolosAtributos = new Dictionary<object, NodoAtributo>();
        public Dictionary<object, NodoMetodo> tablaDeSimbolosMetodos = new Dictionary<object, NodoMetodo>();
    }

    public class NodoAtributo
    {
        public string lexema;
        public TipoDeVariable tipovariable;
        public TipoDeDato tipodato;
        public string valor;
    }

    public class NodoMetodo
    {
        public string lexema;
        public int linea;
        public Dictionary<object, NodoParametro> tablaDeSimbolosParametros = new Dictionary<object, NodoParametro>();
        public Dictionary<object, NodoVariable> tabalaDeSimbolosVariables = new Dictionary<object, NodoVariable>();
    }

    public class NodoParametro
    {
        public string lexema;
        public TipoDeVariable tipovariable;
        public TipoDeDato tipodato;
        public string valor;
    }

    public class NodoVariable
    {
        public string lexema;
        public TipoDeVariable tipovariable;
        public TipoDeDato tipodato;
        public string valor;
    }

    public enum TipoDeDato
    {
        SinEspecificar,
        Entero,
        Decimal,
        Cadena,
        Error,
        Vacio
    }

    public enum TipoDeVariable
    {
        VariableLocal,
        Parametro,
        Atributo
    }

    public enum Estado
    {
        Insertado,
        Duplicado,
        NoExisteClase
    }

    public class TablaSimbolos
    {
        public NodoClase claseActual = new NodoClase();

        public NodoMetodo metodoActual = new NodoMetodo();

        public static Dictionary<object, NodoClase> tablaDeSimbolosClase = new Dictionary<object, NodoClase>();

        public NodoClase ObtenerNodoClase(string lexema)
        {
            if (tablaDeSimbolosClase.ContainsKey(lexema))
                return tablaDeSimbolosClase.SingleOrDefault(x => x.Key.ToString() == lexema).Value;
            else
                throw new Exception("Error Semantico: No existe el nombre de Clase");
        }

        public NodoAtributo ObtenerNodoAtributo(NodoClase _clase, string _idAtributo)
        {
            if (_clase.tablaDeSimbolosAtributos.ContainsKey(_idAtributo))
                return _clase.tablaDeSimbolosAtributos.SingleOrDefault(x => x.Key.ToString() == _idAtributo).Value;
            else
                throw new Exception("Error Semantico: No existe el nombre del atributo");
        }

        public NodoMetodo ObtenerNodoMetodo(NodoClase _clase, string _idMetodo)
        {
            if (_clase.tablaDeSimbolosMetodos.ContainsKey(_idMetodo))
                return _clase.tablaDeSimbolosMetodos.SingleOrDefault(x => x.Key.ToString() == _idMetodo).Value;
            else
                throw new Exception("Error Semantico: No existe el nombre del metodo");
        }

        public NodoMetodo ObtenerMetodoInvocacion(NodoClase _clase, Nodo _metodoinvocado)
        {
            NodoMetodo _metodo = new NodoMetodo();
            var _result = _clase.tablaDeSimbolosMetodos.Select(x => x.Value).Where(x => x.lexema.StartsWith(_metodoinvocado.lexema+ "*")).ToList();
            if (_result.Count != 0)
            {
                if (_result.Count == 1)
                {
                    _metodo = _result[0];
                }
                else
                {
                    _metodo = _result[_result.Count - 1];
                }

            }
            else
            {
                throw new Exception("Error Semantico linea "+ _metodoinvocado.linea+ ": No existe el nombre del metodo invocado");
            }

            if (_metodo.tablaDeSimbolosParametros.Count == _metodoinvocado.parametros.Count)
            {
                int _cantidadparametros = _metodo.tablaDeSimbolosParametros.Count;
                var _keyparametros = _metodo.tablaDeSimbolosParametros.ToList();
                for (int i = 0; i < _cantidadparametros; i++)
                {
                    if (_metodoinvocado.parametros[i].tipodatoparametro != TipoDeDato.SinEspecificar)
                    {
                        _metodo.tablaDeSimbolosParametros[_keyparametros[i].Value.lexema].tipodato = _metodoinvocado.parametros[i].tipodatoparametro;
                    }
                    else
                    {
                        string _idmetodo = _metodoinvocado.metodo + "*" + _metodoinvocado.lineametodo;
                        NodoMetodo _metodovariable = ObtenerNodoMetodo(_clase,_idmetodo);
                        NodoVariable _varible = ObtenerNodoVariable(_metodovariable, _metodoinvocado.parametros[i].tokenparametro.lexema);
                        _metodo.tablaDeSimbolosParametros[_keyparametros[i].Value.lexema].tipodato = _varible.tipodato;
                    }
                   
                }
            }
            else
            {
                throw new Exception("Error Semantico linea " + _metodoinvocado.linea + ": Faltan parametros");
            }
            return _metodo;
        }

        public NodoVariable ObtenerNodoVariable(NodoMetodo _metodo, string _idVariable)
        {
            if (_metodo.tabalaDeSimbolosVariables.ContainsKey(_idVariable))
                return _metodo.tabalaDeSimbolosVariables.SingleOrDefault(x => x.Key.ToString() == _idVariable).Value;
            else
                return null;
                
        }

        public NodoParametro ObtenerNodoParametro(NodoMetodo _metodo, string _idParametro)
        {
            if (_metodo.tablaDeSimbolosParametros.ContainsKey(_idParametro))
                return _metodo.tablaDeSimbolosParametros.SingleOrDefault(x => x.Key.ToString() == _idParametro).Value;
            else
                return null;
        }

        public Estado InsertarNodoClase(NodoClase _nodoclase)
        {
            if (!tablaDeSimbolosClase.ContainsKey(_nodoclase.lexema))
            {
                tablaDeSimbolosClase.Add(_nodoclase.lexema, _nodoclase);
                return Estado.Insertado;
            }
            else
            {
                return Estado.Duplicado;
            }
        }

        public Estado InsertarHerencia(string _herencia)
        {
            NodoClase _herenciainsertar = ObtenerNodoClase(_herencia);
            if (_herenciainsertar.lexema != null)
            {
                claseActual.herencia.lexema = _herenciainsertar.lexema.ToString();
                claseActual.herencia.herencia = _herenciainsertar.herencia;
                claseActual.herencia.tablaDeSimbolosAtributos = _herenciainsertar.tablaDeSimbolosAtributos;
                claseActual.herencia.tablaDeSimbolosMetodos = _herenciainsertar.tablaDeSimbolosMetodos;

                if (tablaDeSimbolosClase.ContainsKey(claseActual.lexema))
                {
                    tablaDeSimbolosClase.Remove(claseActual.lexema);
                    InsertarNodoClase(claseActual);
                    return Estado.Insertado;
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre de Clase");
                }
            }
            else
            {
                return Estado.NoExisteClase;
            }
        }

        public Estado InsertarAtributo(NodoAtributo _nuevoatributo)
        {
            if (tablaDeSimbolosClase.ContainsKey(claseActual.lexema))
            {
                if (!claseActual.tablaDeSimbolosAtributos.ContainsKey(_nuevoatributo.lexema))
                {
                    claseActual.tablaDeSimbolosAtributos.Add(_nuevoatributo.lexema, _nuevoatributo);
                    tablaDeSimbolosClase.Remove(claseActual.lexema);
                    InsertarNodoClase(claseActual);
                    return Estado.Insertado;
                }
                else
                {
                    return Estado.Duplicado;
                }
              
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }

        public Estado InsertarMetodo(NodoMetodo _nuevometodo)
        {
            if (tablaDeSimbolosClase.ContainsKey(claseActual.lexema))
            {
                if (!claseActual.tablaDeSimbolosMetodos.ContainsKey(_nuevometodo.lexema))
                {
                    claseActual.tablaDeSimbolosMetodos.Add(_nuevometodo.lexema, _nuevometodo);
                    tablaDeSimbolosClase.Remove(claseActual.lexema);
                    InsertarNodoClase(claseActual);
                    return Estado.Insertado;
                }
                else
                {
                    return Estado.Duplicado;
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }

        public Estado InsertarParametro(NodoParametro _nuevoparametro)
        {
            if (tablaDeSimbolosClase.ContainsKey(claseActual.lexema))
            {
                if (claseActual.tablaDeSimbolosMetodos.ContainsKey(metodoActual.lexema))
                {
                    if (!metodoActual.tablaDeSimbolosParametros.ContainsKey(_nuevoparametro.lexema))
                    {
                        metodoActual.tablaDeSimbolosParametros.Add(_nuevoparametro.lexema,_nuevoparametro);
                        claseActual.tablaDeSimbolosMetodos.Remove(metodoActual.lexema);
                        claseActual.tablaDeSimbolosMetodos.Add(metodoActual.lexema, metodoActual);
                        tablaDeSimbolosClase.Remove(claseActual.lexema);
                        tablaDeSimbolosClase.Add(claseActual.lexema,claseActual);
                        return Estado.Insertado;
                    }
                    else
                    {
                        return Estado.Duplicado;
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

        public Estado InsertarVariable(NodoVariable _nuevavariable)
        {
            if (tablaDeSimbolosClase.ContainsKey(claseActual.lexema))
            {
                if (claseActual.tablaDeSimbolosMetodos.ContainsKey(metodoActual.lexema))
                {
                    if (!metodoActual.tabalaDeSimbolosVariables.ContainsKey(_nuevavariable.lexema))
                    {
                        metodoActual.tabalaDeSimbolosVariables.Add(_nuevavariable.lexema, _nuevavariable);
                        claseActual.tablaDeSimbolosMetodos.Remove(metodoActual.lexema);
                        claseActual.tablaDeSimbolosMetodos.Add(metodoActual.lexema, metodoActual);
                        tablaDeSimbolosClase.Remove(claseActual.lexema);
                        tablaDeSimbolosClase.Add(claseActual.lexema, claseActual);
                        return Estado.Insertado;
                    }
                    else
                    {
                        return Estado.Duplicado;
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

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
    public partial class frmEditor : Form
    {
        int contadorPaginascreadas = 1;
        public frmEditor()
        {
            InitializeComponent();
        }

        private void frmEditor_Load(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            contadorPaginascreadas ++;

            TabPage TP = new TabPage();
            tabControl1.TabPages.Add(TP);
            TP.BackColor = Color.White;
            EditorTexto et = new EditorTexto();
            et.Name = "editorTexto"+contadorPaginascreadas;
            TP.Controls.Add(et);
            TP.Name = "tabPage"+contadorPaginascreadas;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);
            
            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));
         
                foreach (var ctrleditor in controleseditor)
                {

                    ctrleditor.Guardar();

                }
            }
            
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);

            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));

                foreach (var ctrleditor in controleseditor)
                {
                    ctrleditor.Abir();

                }
            }

        }

        private void tsrEjecutarAnalizadorLexico_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);

            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));

                foreach (var ctrleditor in controleseditor)
                {
                    string codigofuente = ctrleditor.Texto();
                    Lexico _lex = new Lexico();                    
                    if (codigofuente.Length > 0)
                    {
                        List<Token> ListaDeTokens = _lex.EjecutarLexico(codigofuente);
                        dgvListaTokens.DataSource = ListaDeTokens;
                        tsrEjecutarAnalizadorSintactico_Click(null, null);
                        //tsrEjecutarAnalizadorSintactico.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Escribe algo primero :v");
                    }
                }
            }

        }

        private void tsrEjecutarAnalizadorSintactico_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);

            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));

                foreach (var ctrleditor in controleseditor)
                {
                    string codigofuente = ctrleditor.Texto();
                    Lexico _lex = new Lexico();
                    Sintactico _sin = new Sintactico();
                    if (codigofuente.Length > 0)
                    {
                        List<Token> lista = new List<Token>();
                        lista = _lex.EjecutarLexico(codigofuente);
                        int[] Tokensparasintactico = new int[500];
                        string[] Lexemaparasintactico = new string[500];
                        int[] Lineaparasaberposiciondelmetodo = new int[500];
                        int cantidaddetokens = 0, ubicacion = 0;
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].token != -41 && lista[i].token != -39)
                            {
                                Tokensparasintactico[ubicacion] = lista[i].token;
                                Lexemaparasintactico[ubicacion] = lista[i].lexema;
                                Lineaparasaberposiciondelmetodo[ubicacion] = lista[i].linea;
                                if (lista[i].token != -40)
                                {
                                    _sin.ListaDeTokensSintactico.Add(lista[i]);
                                }
                                cantidaddetokens++;
                                ubicacion++;
                            }
                        }
                        Tokensparasintactico[cantidaddetokens] = -99;
                        //MessageBox.Show(Convert.ToString(Tokensparasintactico[0])+" "+ Convert.ToString(Tokensparasintactico[1]));
                        _sin.EjecutarSintactico(Tokensparasintactico, Lexemaparasintactico, Lineaparasaberposiciondelmetodo,false);


                    }
                    else
                    {
                        MessageBox.Show("Escribe algo primero :v");
                    }
                }
            }

        }

        
        private void semanticoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArbolSintactico.codigoP = "";
            tsrEjecutarAnalizadorLexico_Click(null, null);
            //tsrEjecutarAnalizadorLexico.PerformClick();
        }

        private void recorrerArbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nodo _ArbolSin = ArbolSintactico.Arbol;
            RecorridoEnPostOrden(_ArbolSin);
        }

        public void RecorridoEnPostOrden(Nodo _ArbolRecorrer)
        {
           
            if (_ArbolRecorrer.hijoIzquierdo != null)
            {
                if (_ArbolRecorrer.hijoIzquierdo.lexema != string.Empty)
                {
                    RecorridoEnPostOrden(_ArbolRecorrer.hijoIzquierdo);
                }
            }

            if (_ArbolRecorrer.hijoCentro != null)
            {
                if (_ArbolRecorrer.hijoCentro.lexema != string.Empty)
                {
                    RecorridoEnPostOrden(_ArbolRecorrer.hijoCentro);
                }
            }

            if (_ArbolRecorrer.hijoDerecho != null)
            {
                if (_ArbolRecorrer.hijoDerecho.lexema != string.Empty)
                {
                    RecorridoEnPostOrden(_ArbolRecorrer.hijoDerecho);
                }
            }
            

            if (_ArbolRecorrer.lexema != null)
            {
                if (_ArbolRecorrer.nodoTipoSentencia != tipoSentencia.Incremento)
                {
                    if (_ArbolRecorrer.nodoTipoSentencia == tipoSentencia.Invocacion)
                    {
                        MessageBox.Show("Invocacion: "+_ArbolRecorrer.lexema);
                    }
                    else
                    {
                        MessageBox.Show(_ArbolRecorrer.lexema);
                    }
                    
                }
                
                if (_ArbolRecorrer.Hermano != null)
                {
                    if (_ArbolRecorrer.Hermano.lexema != string.Empty)
                    {
                        RecorridoEnPostOrden(_ArbolRecorrer.Hermano);
                    }
                }
            }
               

        }

        private void comprobacionDeTiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComprobacionDeTipos _comprobaciontipos = new ComprobacionDeTipos();
            ComprobacionDeTipos.ArbolComprobacionDeTipos = ArbolSintactico.Arbol;
            _comprobaciontipos.EjecutarComprobacionDeTipos();
        }

        private void codigoPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbCodigoP.Text = ArbolSintactico.codigoP;
        }
    }
}

using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace CSV___JSon_Converter
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        CSV csv;
        Json json;
       
        public MainWindow()
        {
            InitializeComponent();            
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            
        
        }        

        void ClearBoxesAndDisableButtons()
        {
            txtRichCSV.Document.Blocks.Clear();
            txtRichJson.Document.Blocks.Clear();

            btnCsvConvert.IsEnabled = false;
            btnCsvCopy.IsEnabled = false;

            btnJsonConvert.IsEnabled = false;
            btnJsonCopy.IsEnabled = false;
        }

        void PopulateBoxes()
        {
            if (json != null)
            {
                txtRichJson.AppendText(json.Print());
                btnJsonConvert.IsEnabled = true;
                btnJsonCopy.IsEnabled = true;
            }

            if (csv != null)
            {
                txtRichCSV.AppendText(csv.Print());
                btnCsvConvert.IsEnabled = true;
                btnCsvCopy.IsEnabled = true;
            }
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Arquivos de Dados(txt, csv, json) | *.txt; *.csv; *.json";
            dlg.Title = "Choose data file";
         
            if(dlg.ShowDialog(this) == true)
            {
                string targeFile = File.ReadAllText(dlg.FileName);

                if(!String.IsNullOrEmpty(targeFile))
                {
                    if(CSV.IsSrcValid(targeFile))
                    {
                        json = null;
                        csv = null;

                        csv = CSV.Load(targeFile);

                        ClearBoxesAndDisableButtons();
                        PopulateBoxes();
                    }
                    else if(Json.IsSrcValid(targeFile))
                    {
                        json = null;
                        csv = null;

                        json = Json.Load(targeFile);

                        ClearBoxesAndDisableButtons();
                        PopulateBoxes();
                    }
                    else
                    {
                        MessageBox.Show("The file is not valid.", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void menuQuit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnCsvConvert_Click(object sender, RoutedEventArgs e)
        {
            if(txtRichCSV.Document.Blocks.Count> 0)
            {
                TextRange textRange = new TextRange(txtRichCSV.Document.ContentStart, txtRichCSV.Document.ContentEnd);

                string[] lines = textRange.Text.Split('\n');

                csv.ClearLines();

                foreach(string l in lines)
                {
                    string[] values = l.Split('\t');
                    string lineSrc = "";

                    foreach(string v in values)
                    {
                        if(!String.IsNullOrEmpty(v))
                        {
                            lineSrc += v;
                        }

                        if(v != values.Last())
                        {
                            lineSrc += ",";
                        }
                    }
                    if (!String.IsNullOrEmpty(lineSrc))
                    {
                        csv.AddLine(CsvLine.Load(lineSrc));
                    }
                }                

                json = Converter.CsvToJson(csv);

                ClearBoxesAndDisableButtons();
                PopulateBoxes();
            }
        }

        private void btnJsonConvert_Click(object sender, RoutedEventArgs e)
        {
            if (txtRichJson.Document.Blocks.Count > 0)
            {
                TextRange textRange = new TextRange(txtRichJson.Document.ContentStart, txtRichJson.Document.ContentEnd);

                string jsonSrc = textRange.Text;

                if (Json.IsSrcValid(jsonSrc))
                {
                    json = Json.Load(jsonSrc);

                    csv = Converter.JsonToCsv(json);

                    ClearBoxesAndDisableButtons();
                    PopulateBoxes();
                }
            }
        }

        private void btnCsvCopy_Click(object sender, RoutedEventArgs e)
        {
            if(txtRichCSV.Document.Blocks.Count > 0)
            {
                TextRange textRange = new TextRange(txtRichCSV.Document.ContentStart, txtRichCSV.Document.ContentEnd);

                if(!String.IsNullOrEmpty(textRange.Text))
                {
                    Clipboard.SetText(textRange.Text);

                    Point location = btnCsvCopy.PointToScreen(new Point(0, 0));
                    ModalWindow modalWindow = new ModalWindow("CSV copied.", location);
                    modalWindow.Show();
                }
            }
        }

        private void btnJsonCopy_Click(object sender, RoutedEventArgs e)
        {
            if (txtRichJson.Document.Blocks.Count > 0)
            {
                TextRange textRange = new TextRange(txtRichJson.Document.ContentStart, txtRichJson.Document.ContentEnd);

                if (!String.IsNullOrEmpty(textRange.Text))
                {
                    Clipboard.SetText(textRange.Text);

                    Point location = btnJsonCopy.PointToScreen(new Point(0, 0));
                    ModalWindow modalWindow = new ModalWindow("Json copied.", location);
                    modalWindow.Show();
                }
            }
        }
    }
}

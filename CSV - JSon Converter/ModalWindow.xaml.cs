using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CSV___JSon_Converter
{
    /// <summary>
    /// Lógica interna para ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        DispatcherTimer timer;
        public ModalWindow(string message, Point position)
        {
            InitializeComponent();
            
            txtMsg.Text = message;
            Left = position.X;
            Top = position.Y - Height;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}

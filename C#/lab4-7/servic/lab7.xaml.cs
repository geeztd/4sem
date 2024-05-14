using servic.lab7;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace servic
{
    /// <summary>
    /// Логика взаимодействия для lab7.xaml
    /// </summary>
    public partial class lab7Window : Window
    {
        public lab7Window()
        {
            InitializeComponent();

        }

        public void OnDirectEvent(object sender, RoutedEventArgs e)
        {
            TextOut.Text += $"Событие: {e.RoutedEvent.Name}, Источник: {sender.GetType().Name}, OriginalSource: {e.OriginalSource.GetType().Name}\n";
        }
        public void OnTunnelingEvent(object sender, RoutedEventArgs e)
        {
            TextOut2.Text += $"Событие: {e.RoutedEvent.Name}, Источник: {sender.GetType().Name}, OriginalSource: {e.OriginalSource.GetType().Name}\n";
        }

        public static readonly RoutedEvent DirectEvent = EventManager.RegisterRoutedEvent(
            "DirectEvent", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(MainWindow));

        public static readonly RoutedEvent TunnelingEvent = EventManager.RegisterRoutedEvent(
            "TunnelingEvent", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(MainWindow));

        public static readonly RoutedEvent BubblingEvent = EventManager.RegisterRoutedEvent(
            "BubblingEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainWindow));

        private void BubblingButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BubblingEvent");
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("exit");
            this.Close();
        }
    }
}




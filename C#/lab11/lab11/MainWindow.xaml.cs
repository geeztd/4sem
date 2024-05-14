using lab11.MVVM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab11 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        DB db = new();
        public MainWindow() {
            InitializeComponent();
            db.Database.EnsureCreated();
            db.consultations.Load();
            List<Consultation> list = db.consultations.Local.ToList();
            MainViewModel viewModel = new(list);
            DataContext = viewModel;
        }



    }
}
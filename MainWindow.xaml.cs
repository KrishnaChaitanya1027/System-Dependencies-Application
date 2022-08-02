//Group 5: Sairam Ganesh Yedida Harivenkata,Nikitaben Jashvantsinh Raj,Krishna Chaitanya Potharajuusing System.Windows;
using System.Windows;

namespace System_Dependencies___Final_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }


        private void Query_Click(object sender, RoutedEventArgs e)
        {
            vm.process();
        }
    }
}

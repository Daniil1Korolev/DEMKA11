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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UchetRemont
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonVhod_Click(object sender, RoutedEventArgs e)
        {
            if (tblogin.Text != "" && tbPass.Text != "")
            {
                DataBaseClass dataBaseClass = new DataBaseClass();



                if (dataBaseClass.login(tblogin.Text, tbPass.Text))
                {
                    MessageBox.Show("Удачный вход!");
                    Zayvka Zayvka = new Zayvka();
                   
                    Zayvka.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось войти!");
                }

                tblogin.Clear(); tbPass.Clear();
            }
            else
            {
                MessageBox.Show("Не все поля заполены!");
            }
        }

       
    }
}

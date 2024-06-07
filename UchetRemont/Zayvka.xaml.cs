using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace UchetRemont
{
    /// <summary>
    /// Логика взаимодействия для Zayvka.xaml
    /// </summary>
    public partial class Zayvka : Window
    {
        public Zayvka()
        {
            InitializeComponent();
            DataContext = this;

        }



        private void TableZayavkaFill()
        {
            try
            {

                Action action = () =>
                {
                    DataBaseClass dataBaseClass = new DataBaseClass();

                    dataBaseClass.sqlExecute("select [ID_Zayavka], [Number_Zayavka], [Data_dobavlen], [Devices_ID], [Klient_ID],[Status_ID] from [dbo].[Zayavka]", DataBaseClass.act.select);

                    dataBaseClass.dependency.OnChange += DependancyOnChange_Zayavka;

                    dgZayavka.ItemsSource = dataBaseClass.resultTable.DefaultView;
                    dgZayavka.Columns[0].Visibility = Visibility.Hidden;
                    dgZayavka.Columns[1].Header = "Номер заявки";
                    dgZayavka.Columns[2].Header = "Дата добавления";
                    dgZayavka.Columns[3].Header = "Оборудование";
                    dgZayavka.Columns[4].Header = "Клиент";
                    dgZayavka.Columns[5].Header = "Заявка";
                };
                Dispatcher.Invoke(action);
            }
            catch { };
        }


        private void dgZayavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = dgZayavka.SelectedItems[0] as DataRowView;
                tbNumber.Text = dataRowView[1].ToString();
                tbDate.Text = dataRowView[2].ToString();
                tbDevice.Text = dataRowView[3].ToString();
                tbStatus.Text = dataRowView[4].ToString();
                tbKlient.Text = dataRowView[5].ToString();

            }
            catch { }
        }

    

        private void DependancyOnChange_Zayavka(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
            {
                TableZayavkaFill();
            }
        }
        private void dgZayavka_Loaded(object sender, RoutedEventArgs e)
        {
            TableZayavkaFill();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataBaseClass dataBaseClass = new DataBaseClass();
            dataBaseClass.sqlExecute(string.Format("insert into [dbo].[Zayavka] ([Number_Zayavka], [Data_dobavlen], [Devices_ID], [Klient_ID], [Status_ID])" +
                "values ('{0}', '{1}', '{2}', '{3}','{4}')", tbNumber.Text, tbDate.Text, tbDevice.Text, tbStatus.Text, tbKlient.Text), DataBaseClass.act.manipulation);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataBaseClass dataBaseClass = new DataBaseClass();
            DataRowView dataRowView = dgZayavka.SelectedItems[0] as DataRowView;
            dataBaseClass.sqlExecute(String.Format("update [dbo].[Zayavka] set " +
                "[Number_Zayavka] = '{0}'," +
                "[Data_dobavlen] = '{1}'," +
                "[Devices_ID] = '{2}'," +
                "[Klient_ID] = '{3}'," +
                "[Status_ID] = '{4}'" +
                "where [ID_Zayavka] = {5}",
                  tbNumber.Text, tbDate.Text, tbDevice.Text, tbStatus.Text, tbKlient.Text, dataRowView[0]), DataBaseClass.act.manipulation);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgZayavka.Items.Count != 0 & dgZayavka.SelectedItems.Count != 0)
            {
                DataRowView dataRowView = (DataRowView)dgZayavka.SelectedItems[0];
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute(string.Format("delete from [dbo].[Zayavka] where [ID_Zayavka] = {0}", dataRowView[0]), DataBaseClass.act.manipulation);
            }
        }
    }
}

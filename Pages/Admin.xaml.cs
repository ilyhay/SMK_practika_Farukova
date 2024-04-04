using SMK_practika.Model;
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

namespace SMK_practika.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private Claims_Report report;
        public Admin(Company_Employee employee)
        {
            InitializeComponent();
            FrmMain.Navigate(new Autho());
            LoadReporttData();
        }
        private void LoadReporttData()
        {
            try
            {
                var reportData = Helper.GetContext().Claims_Report
                    .Select(u => new
                    {
                        u.report_id,
                        u.payment_id,
                        u.Payment.Payment_Status.status,
                        u.Payment.payment_date,
                        u.Company_Employee.name,
                        u.Company_Employee.surname,
                        u.Company_Employee.patronimyc,
                        u.Company_Employee.phone
                    })
                    .ToList();

                dataGrid.ItemsSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrmMain.GoBack();
        }
        private void FrmMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrmMain.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

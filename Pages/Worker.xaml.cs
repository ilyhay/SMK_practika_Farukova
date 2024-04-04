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
    /// Логика взаимодействия для Worker.xaml
    /// </summary>
    public partial class Worker : Page
    {
        private Payment payment;
        public Worker(Company_Employee employee)
        {
            InitializeComponent();
            FrmMain.Navigate(new Autho());
            LoadPaymentData();
        }

        private void LoadPaymentData()
        {
            try
            {
                var paymentData = Helper.GetContext().Payment
                    .Select(u => new
                    {
                        u.payment_id,
                        u.payment_date,
                        u.Payment_Status.status,
                        u.Medical_Institution.name,
                        u.Medical_Institution.address,
                        u.Medical_Institution.Medical_Service.service_name,
                        u.Insurance_Claim.claim_date,
                        u.Insurance_Claim.description,
                        u.Insurance_Claim.Insurance_Contract.insurance_conditions,
                        u.Insurance_Claim.Insurance_Contract.Client.client_id
                    })
                    .ToList();

                dataGrid.ItemsSource = paymentData;
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

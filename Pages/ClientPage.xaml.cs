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
using SMK_practika.Model;


namespace SMK_practika.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        Insurance_Claim claim;
        public ClientPage(Client client)
        {
            InitializeComponent();
            FrmMain.Navigate(new Autho());
            LoadClaimData();
        }

        private void LoadClaimData()
        {
            try
            {
                var claimData = claim.Insurance_Contract.Insurance_Claim
                    .Select(i => new
                    {
                        i.Insurance_Contract.contract_id,
                        i.Insurance_Contract.insurance_conditions,
                        i.claim_id,
                        i.claim_date,
                        i.description
                    })
                    .ToList();

                dataGrid.ItemsSource = claimData;
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

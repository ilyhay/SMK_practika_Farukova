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
using System.Data.Entity;
using Npgsql;

namespace SMK_practika.Pages
{
    /// <summary>
    /// Логика взаимодействия для Director.xaml
    /// </summary>
    public partial class Director : Page
    {
        public Director(Company_Employee employee, string role)
        {
            InitializeComponent();
            FrmMain.Navigate(new Autho());
            LoadEmployeeData();
        }
        private void LoadEmployeeData()
        {
            try
            {
                var employeeData = Helper.GetContext().Company_Employee
                    .Select(u => new
                    {
                        u.employee_id,
                        u.name,
                        u.surname,
                        u.patronimyc,
                        u.Role.role1,
                        u.phone,
                        u.login,
                        u.password
                    })
                    .ToList();

                dataGrid.ItemsSource = employeeData;
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

        private void FrmMain_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Добавить новую пустую запись в источник данных DataGrid
                var newEmployee = new Company_Employee();
                var context = Helper.GetContext();
                context.Company_Employee.Add(newEmployee);
                LoadEmployeeData();

                // Найти новую строку в DataGrid
                var index = dataGrid.Items.Count - 1;

                // Если новая строка существует, выделить ее
                if (index >= 0)
                {
                    dataGrid.SelectedIndex = index;
                    dataGrid.Focus(); // Установить фокус на DataGrid для редактирования
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления новой записи: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = Helper.GetContext();
                context.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены.");
                LoadEmployeeData(); // Перезагрузить данные после сохранения
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения изменений: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранного работника
            dynamic selectedEmployee = dataGrid.SelectedItem;
            if (selectedEmployee != null)
            {
                try
                {
                    var context = Helper.GetContext();
                    int employeeId = selectedEmployee.employee_id;
                    var employeeToDelete = context.Company_Employee.FirstOrDefault(emp => emp.employee_id == employeeId);
                    if (employeeToDelete != null)
                    {
                        context.Company_Employee.Remove(employeeToDelete);
                        context.SaveChanges();
                        LoadEmployeeData();
                        MessageBox.Show("Работник успешно удален.");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти выбранного работника.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления работника: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите работника для удаления.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Data.Entity;
using Npgsql;
using System.Windows.Threading;

namespace SMK_practika.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private Client client;
        private Company_Employee employee;
        public Autho()
        {
            InitializeComponent();
        }

        // Метод для загрузки форм в зависимости от роли пользователя
        private void LoadForm(string role, Company_Employee employee)
        {
            //Загрузка соответствующей страницы
            switch (role)
            {
                case "Директор":
                    NavigationService.Navigate(new Director(employee, role));
                    break;
                case "Сотрудник отдела":
                    NavigationService.Navigate(new Worker(employee));
                    break;
                case "Администратор":
                    NavigationService.Navigate(new Admin(employee));
                    break;

            }
        }
        private void LoadClientPage(Client client)
        {
            NavigationService.Navigate(new ClientPage(client));
        }

        // Обработчик нажатия кнопки входа для пользователя
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtbLogin.Text.Trim();
            string password = pswdPassword.Password.Trim();

            if (login.Length > 0 && password.Length > 0)
            {
                // Проверяем пользователя среди сотрудников компании
                employee = Helper.GetContext().Company_Employee.Where(u => u.login == login && u.password == password).FirstOrDefault();

                if (employee != null)
                {
                    MessageBox.Show("Вы вошли под ролью: " + employee.Role.role1.ToString());
                    LoadForm(employee.Role.role1.ToString(), employee);
                }
                else
                {
                    // Если сотрудник не найден, проверяем пользователя среди клиентов
                    client = Helper.GetContext().Client.Where(c => c.login == login && c.password == password).FirstOrDefault();

                    if (client != null)
                    {
                        MessageBox.Show("Вы вошли как клиент.");
                        LoadClientPage(client);
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Warning", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}

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

namespace _3ISIP_121_Gushchin_Moroz_Dictionary.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTermPage.xaml
    /// </summary>
    public partial class AddTermPage : Page
    {
        private Dictionary _currentTerm = new Dictionary();
        public AddTermPage()
        {
            InitializeComponent();

            DataContext = _currentTerm;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentTerm.Term))
                errors.AppendLine("Укажите термин!");
            if (string.IsNullOrWhiteSpace(_currentTerm.Definition))
                errors.AppendLine("Укажите определение!");
            if(string.IsNullOrWhiteSpace(_currentTerm.Definition))
                errors.AppendLine("Укажите определение!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            
            try
            {
                Entities.GetContext().Database.ExecuteSqlCommand($"INSERT INTO dbo.Dictionary VALUES ('{_currentTerm.Term}','{_currentTerm.Definition}','{_currentTerm.Source}')");
                MessageBox.Show("Данные успешно сохранены!");
                NavigationService.Navigate(new Uri("Pages/TermsPage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/TermsPage.xaml", UriKind.Relative));
        }
    }
}

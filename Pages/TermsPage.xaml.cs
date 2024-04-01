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
    /// Логика взаимодействия для TermsPage.xaml
    /// </summary>
    public partial class TermsPage : Page
    {
        public TermsPage()
        {
            InitializeComponent();
            //DataGridDictionary.ItemsSource = Entities.GetContext().Dictionary.ToList();
            using (var db = new Entities())
            {
                var data = db.Dictionary.SqlQuery("SELECT * FROM dbo.Dictionary").ToList();
                DataGridDictionary.ItemsSource = data;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/AddTermPage.xaml", UriKind.Relative));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                using (var db = new Entities())
                {
                    var data = db.Dictionary.SqlQuery("SELECT * FROM dbo.Dictionary").ToList();
                    DataGridDictionary.ItemsSource = data;
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DataGridDictionary.SelectedItems.Cast<Dictionary>().ToList();
            if (MessageBox.Show($" Вы точно хотите удалить записи в количестве { usersForRemoving.Count()} элементов ? "," Внимание ",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var user in usersForRemoving)
                    {
                        Entities.GetContext().Database.ExecuteSqlCommand($"DELETE dbo.Dictionary WHERE Term='{user.Term}' AND Definition='{user.Definition}' AND Source='{user.Source}'");
                    }
                    /*Entities.GetContext().Dictionary.RemoveRange(usersForRemoving);
                    Entities.GetContext().SaveChanges();*/

                    MessageBox.Show("Данные успешно удалены!");
                    DataGridDictionary.ItemsSource = Entities.GetContext().Dictionary.SqlQuery("SELECT * FROM dbo.Dictionary").ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}

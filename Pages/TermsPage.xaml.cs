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
    }
}

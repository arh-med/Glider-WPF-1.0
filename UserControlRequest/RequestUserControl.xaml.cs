using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Glider_WPF_1._0.UserControlRequest
{
    /// <summary>
    /// Логика взаимодействия для RequestUserControl.xaml
    /// </summary>
    public partial class RequestUserControl : UserControl
    {
        public ObservableCollection<Request> Requests { get; set; } // Колекция катороя оповещяет об изменение....
        public RequestUserControl()
        {
            InitializeComponent();
            Requests = new ObservableCollection<Request>(GliderDataContext.Instance.Requests.ToList());
            DataContext = this;
        }

        private void AddButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            request.Nomination = Nomination_txt.Text;
            request.Quantity = Quanti_txt.Text;
            request.CustomerPhone = CustomerPhone_txt.Text;
            GliderDataContext gliderDataContext = GliderDataContext.Instance; 
            gliderDataContext.Requests.Add(request);
            gliderDataContext.SaveChanges();

            Requests.Add(request);
        }

        private void RemoveButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            Request requestDataGridSelect = DataGridRequest.SelectedCells[3].Item as Request;
            GliderDataContext gliderDataContext = GliderDataContext.Instance;
            gliderDataContext.Requests.Remove(requestDataGridSelect);
            gliderDataContext.SaveChanges();

            Requests.Remove(requestDataGridSelect);
        }

        private void RenameButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            RemoveButtonClickEventHandler(this, null);
            AddButtonClickEventHandler(this, null);
        }
    }
}

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
    
    public partial class RequestUserControl : UserControl
    {
        public ObservableCollection<Request> Requests { get; set; } // Колекция катороя оповещяет об изменение....
        string Login { get; set; }
        public RequestUserControl(string Login)
        {
            InitializeComponent();
            Requests = new ObservableCollection<Request>();
            this.Login = Login;
            ObservableCollection<Request> RequestsSort = new ObservableCollection<Request>(GliderDataContext.Instance.Requests.ToList());
            foreach (Request req in RequestsSort)
            { if (req.Login ==Login)
                    Requests.Add(req);  }        
            DataContext = this;
            
        }

        private void AddButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            if (Nomination_txt.Text != "")
            {
                Request request = new Request();
                request.Nomination = Nomination_txt.Text;
                request.Quantity = Quanti_txt.Text;
                request.CustomerPhone = CustomerPhone_txt.Text;
                request.Login = Login;
                GliderDataContext gliderDataContext = GliderDataContext.Instance;
                gliderDataContext.Requests.Add(request);
                gliderDataContext.SaveChanges();

                Requests.Add(request);
            }
            else
                MessageBox.Show("Fill in the Nomination");

            Nomination_txt.Clear();
            Quanti_txt.Clear();
            CustomerPhone_txt.Clear();
        }

        private void RemoveButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                Request requestDataGridSelect = DataGridRequest.SelectedCells[3].Item as Request;
                GliderDataContext gliderDataContext = GliderDataContext.Instance;
                gliderDataContext.Requests.Remove(requestDataGridSelect);
                gliderDataContext.SaveChanges();

                Requests.Remove(requestDataGridSelect);
            }
            catch
            { MessageBox.Show("Select the row to delete"); }
        }

        private void RenameButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            if (Nomination_txt.Text != "")
            {
                RemoveButtonClickEventHandler(this, null);
                AddButtonClickEventHandler(this, null);
            }
            else
                MessageBox.Show("Fill in the Nomination");
        }

        private void ReminderButtonClickEventHandler(object sender, RoutedEventArgs e)
        {

            

        }

       

        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRequest.ItemsSource = null;
            List<Request> RequestsSort = new List<Request>(GliderDataContext.Instance.Requests.ToList());
            Requests.Clear();
            foreach (Request req in RequestsSort)
            {
                if (req.Login == Login)
                    Requests.Add(req);
            }
            //Requests = new ObservableCollection<Request>(GliderDataContext.Instance.Requests.ToList());
            DataGridRequest.ItemsSource = Requests;
        }

        private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txt_Search.Text;
            for (int i = 0; i < Requests.Count; i++)
            {
                if (true == Requests[i].Nomination.ToUpper().StartsWith(filter.ToUpper()) && txt_Search.Text!="" )
                {
                    var row = (DataGridRequest.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow);
                    row.Foreground = Brushes.Red;
                    var test = DataGridRequest.Items[i];
                    DataGridRequest.ScrollIntoView(test);
                }
                else
                {
                    (DataGridRequest.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Foreground = Brushes.Black;
                }               
            }
        }
    }
}

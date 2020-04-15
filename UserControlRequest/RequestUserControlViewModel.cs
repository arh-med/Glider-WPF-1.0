using Glider_WPF_1._0.MVVM;
using Glider_WPF_1._0.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Glider_WPF_1._0.UserControlRequest
{
    class RequestUserControlViewModel : ViewModel
    {
        private string nomination;
        private string quantity;
        private string customerPhone;
        public string Nomination
        {
            get
            {
                return nomination;
            }
            set
            {
                Set(ref nomination, value);
            }
        }
        public string Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                Set(ref quantity, value);
            }
        }
        public string CustomerPhone
        {
            get
            {
                return customerPhone;
            }
            set
            {
                Set(ref customerPhone, value);
            }
        }
        private string company;
        RequestUserControl requestUserControl;
        public EventHandler eventTimer;

        private ICommand addRequest;
        public ICommand AddRequest
        {
            get
            {
                return addRequest ?? (addRequest = new CommandExecutor(() => 
                {
                    if (Nomination != "")
                    {
                        Request request = new Request(Nomination,Quantity,CustomerPhone, company);
                        GliderDataContext gliderDataContext = GliderDataContext.Instance;
                        gliderDataContext.Requests.Add(request);
                        gliderDataContext.SaveChanges();

                        requests.Add(request);
                    }
                    else
                        MessageBox.Show("Заполните поля");

                    Nomination = "";
                    Quantity = "";
                    CustomerPhone = "";
                }));
            }
        }
        private ICommand removeRequest;
        public ICommand RemoveRequest
        {
            get
            {
                return removeRequest ?? (removeRequest = new CommandExecutor(() => 
                {
                    try
                    {
                        Request requestDataGridSelect = requestUserControl.DataGridRequest.SelectedCells[3].Item as Request;
                        GliderDataContext gliderDataContext = GliderDataContext.Instance;
                        gliderDataContext.Requests.Remove(requestDataGridSelect);
                        gliderDataContext.SaveChanges();
                        requests.Remove(requestDataGridSelect);
                    }
                    catch
                    { MessageBox.Show("Выберите строку для удаления"); }
                }));
            }
        }
        private ICommand renameRequest;
        public ICommand RenameRequest
        {
            get
            {
                return renameRequest ?? (renameRequest = new CommandExecutor(() => 
                {
                    if (Nomination != "")
                    {
                        try
                        {
                            Request requestDataGridSelect = requestUserControl.DataGridRequest.SelectedCells[3].Item as Request;
                            GliderDataContext gliderDataContext = GliderDataContext.Instance;
                            gliderDataContext.Requests.Remove(requestDataGridSelect);
                            gliderDataContext.SaveChanges();
                            requests.Remove(requestDataGridSelect);
                            try
                            {
                                Request request = new Request(Nomination, Quantity, CustomerPhone, company);
                                gliderDataContext.Requests.Add(request);
                                gliderDataContext.SaveChanges();

                                requests.Add(request);
                            }
                            catch
                            { MessageBox.Show("Заполните поля"); }

                            Nomination = "";
                            Quantity = "";
                            CustomerPhone = "";
                        }
                        catch
                        { MessageBox.Show("Выберите строку для удаления"); }
                        
                    }
                    else
                        MessageBox.Show("Заполните поля");
                }));
            }
        }
        private ObservableCollection<Request> requests = new ObservableCollection<Request>();

        public ObservableCollection<Request> Requests
        {
            get
            {
                return requests;
            }
        }
        public RequestUserControlViewModel(string Company, RequestUserControl requestUserControl)
        {
            company = Company;
            this.requestUserControl = requestUserControl;
            ObservableCollection<Request> RequestsSort = new ObservableCollection<Request>(GliderDataContext.Instance.Requests.ToList());
            foreach (Request request in RequestsSort)
            {
                if (request.Company == Company)
                    requests.Add(request);
            }
            eventTimer = (sender, e) =>
            {
                requestUserControl.DataGridRequest.ItemsSource = null;
                RequestsSort.Clear();
                RequestsSort = new ObservableCollection<Request>(GliderDataContext.Instance.Requests.ToList());
                requests.Clear();
                foreach (Request request in RequestsSort)
                {
                    if (request.Company == Company)
                        requests.Add(request);
                }
                requestUserControl.DataGridRequest.ItemsSource = Requests;
            };
        }

    }
}

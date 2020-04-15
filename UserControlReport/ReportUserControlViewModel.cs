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

namespace Glider_WPF_1._0.UserControlReport
{
    class ReportUserControlViewModel : ViewModel
    {
        private string revenue;
        private string order;
        private string checks;
        private string checksAmount;
        private string notes;
        private string company;
        public string Revenue
        {
            get
            {
                return revenue;
            }
            set
            {
                Set(ref revenue, value);
            }
        }
        public string Order
        {
            get
            {
                return order;
            }
            set
            {
                Set(ref order, value);
            }
        }
        public string Checks
        {
            get
            {
                return checks;
            }
            set
            {
                Set(ref checks, value);
            }
        }
        public string ChecksAmount
        {
            get
            {
                return checksAmount;
            }
            set
            {
                Set(ref checksAmount, value);
            }
        }
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                Set(ref notes, value);
            }
        }
        private ObservableCollection<Report> reports = new ObservableCollection<Report>();
        public ObservableCollection<Report> Reports
        {
            get
            {
                return reports;
            }
        }
        ReportUserControl reportUserControl;
        private ICommand addReport;
        public ICommand AddReport
        {
            get
            {
                return addReport ?? (addReport = new CommandExecutor(() => 
                {
                    if (Notes != "")
                    {
                        Report report = new Report(Revenue, Order, Checks, ChecksAmount, Notes, company);
                        GliderDataContext gliderDataContext = GliderDataContext.Instance;
                        gliderDataContext.Report.Add(report);
                        gliderDataContext.SaveChanges();
                        reports.Add(report);
                    }
                    else
                    {
                        MessageBox.Show("Заполните поля");
                    }
                    Revenue = "";
                    Order = "";
                    Checks = "";
                    ChecksAmount = "";
                    Notes = "";
                }));
            }
        }
        private ICommand removeReport;
        public ICommand RemoveReport
        {
            get
            {
                return removeReport ?? (removeReport = new CommandExecutor(() =>
                {
                    foreach (Report rep in reportUserControl.ItemsControlReports.Items.SourceCollection)
                    {
                        if (rep.Done == true)
                        {
                            GliderDataContext gliderDataContext = GliderDataContext.Instance;
                            gliderDataContext.Report.Remove(rep);
                            gliderDataContext.SaveChanges();
                        }
                    }
                    reportUserControl.ItemsControlReports.ItemsSource = null;
                    reports.Clear();
                    ObservableCollection<Report> ReportsSort = new ObservableCollection<Report>(GliderDataContext.Instance.Report.ToList());
                    foreach (Report report in ReportsSort)
                    {
                        if (report.Company == company)
                            reports.Add(report);
                    }
                    reportUserControl.ItemsControlReports.ItemsSource = Reports;
                }));
            }
        }

        public ReportUserControlViewModel(string Company, ReportUserControl reportUserControl)
        {
            company = Company;
            this.reportUserControl = reportUserControl;
            ObservableCollection<Report> ReportsSort = new ObservableCollection<Report>(GliderDataContext.Instance.Report.ToList());
            foreach (Report report in ReportsSort)
            {
                if (report.Company == Company)
                    reports.Add(report);
            }
        }
    }
}

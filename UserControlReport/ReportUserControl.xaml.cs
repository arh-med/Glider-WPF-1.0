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

namespace Glider_WPF_1._0.UserControlReport
{
    /// <summary>
    /// Логика взаимодействия для ReportUserControl.xaml
    /// </summary>
    public partial class ReportUserControl : UserControl
    {

        ReportUserControlViewModel reportUserControl;
        public ReportUserControl(string company )
        {
            InitializeComponent();
            reportUserControl = new ReportUserControlViewModel(company, this);
            DataContext = reportUserControl;
        }
    }
}

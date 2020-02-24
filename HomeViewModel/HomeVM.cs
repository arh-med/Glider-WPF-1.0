using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glider_WPF_1._0.HomeViewModel
{
    class HomeVM : ObservableObject
    {
        private bool openMenu;
        public bool OpenMenu
        {
            get
            {
                return openMenu;
            }
            set
            {
                openMenu = value;
                NotifyPropertyChanged(nameof(OpenMenu));
               
            }
        }

    }
}

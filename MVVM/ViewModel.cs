using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Glider_WPF_1._0.MVVM
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            //[CallerMemberName] указали чтобы не передаем параметр в метод OnPropertyChanged теперь его можно вызавать без параметра!!!
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T> (ref T filed, T value,[CallerMemberName] string PropertyName = null)
        // проверка если значение являеться одинаковым( чтобы лишний раз не вызывать метод OnPropertyChanget)
        {
            if (Equals(filed, value)) return false;
            filed = value;
            // Вызываем метод OnPropertyChanged чтобы лишний раз не вызвать его в ViewModel
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}

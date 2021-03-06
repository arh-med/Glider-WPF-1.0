﻿using Glider_WPF_1._0.Sql;
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
using Glider_WPF_1._0.Properties;
using Glider_WPF_1._0.UserControlSaver;

namespace Glider_WPF_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = new MainWindowViewModel();
            DataContext = _mainWindowViewModel;
            txt_Password.Password = _mainWindowViewModel.PassworText;
            _mainWindowViewModel.OnLoginFailed += Login_Failed;
            _mainWindowViewModel.OnLoginSucceded += Login_Succeded;
            
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }
        private void ToggleButton_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txt_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel.PassworText = ((PasswordBox)sender).Password;
        }
        private void Login_Failed(object sender, EventArgs e)
        {
            lable_txt.Content = "Неправильное имя или пароль";
        }
        private void Login_Succeded(object sender, EventArgs e)
        {
            Home home = new Home( _mainWindowViewModel.LoginText);
            home.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Support support = new Support();
            support.Show();
        }
    }
}

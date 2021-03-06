﻿using AutotaskQueryExplorer.Login;
using AutotaskQueryService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AutotaskQueryExplorer.Results;

namespace AutotaskQueryExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public LoginViewModel LoginViewModel { get; private set; }
        public QueryToolViewModel QueryToolViewModel { get; private set; }
        public bool IsLoginVisible { get { return !LoginViewModel.IsUserLoggedIn; } }
        public bool IsMainVisibile { get { return !IsLoginVisible; } }

        public MainWindow()
        {
            var queryService = new BasicQueryService(webService: new AutotaskWebService());

            LoginViewModel = new LoginViewModel(queryService);
            QueryToolViewModel = new QueryToolViewModel(queryService); 
            
            LoginViewModel.PropertyChanged += HandleLoginPropertyChanged;
            DataContext = this;

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void HandleLoginPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsUserLoggedIn")
            {
                RaisePropertyChanged("IsLoginVisible");
                RaisePropertyChanged("IsMainVisibile");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

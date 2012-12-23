﻿using AutotaskQueryExplorer.Infrastructure;
using AutotaskQueryService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutotaskQueryExplorer.Login
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool _isUserLoggedIn;
        private readonly IQueryService _service;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsUserLoggedIn
        {
            get
            {
                return _isUserLoggedIn;
            }
            private set
            {
                _isUserLoggedIn = value;
                RaisePropertyChanged("IsUserLoggedIn");
            }
        }

        public string Username { get; set; }

        public ICommand Login { get; private set; }

        public LoginViewModel(IQueryService service)
        {
            _service = service;
            Login = new ParameterCommand<PasswordBox>(execute: ExecuteLogin, canExecute: IsLoginInformationComplete);
        }

        private void ExecuteLogin(PasswordBox box)
        {
            if(box == null)
                throw new InvalidOperationException("You must specify a password box to execute login.");
            try
            {
                _service.Login(Username, box.Password);
                IsUserLoggedIn = true;
            }
            catch { IsUserLoggedIn = false; }
        }

        private bool IsLoginInformationComplete(PasswordBox box)
        {
            return !string.IsNullOrEmpty(Username) && box != null && !string.IsNullOrEmpty(box.Password);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if(propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

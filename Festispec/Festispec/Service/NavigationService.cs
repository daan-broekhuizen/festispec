﻿using Festispec.Model;
using Festispec.Utility;
using Festispec.Utility.Builders;
using Festispec.ViewModel;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Festispec.Service
{
    public class NavigationService : INavigationService, INotifyPropertyChanged
    {

        private readonly Dictionary<string, Uri> _pagesByKey;
        private readonly List<string> _historic;
        private string _currentPageKey;
        public AppSettings AppSettings { get; private set; }

        public string CurrentPageKey
        {
            get => _currentPageKey;
            private set
            {
                if (_currentPageKey == value)
                    return;

                _currentPageKey = value;
                OnPropertyChanged("CurrentPageKey");
            }
        }
        public object Parameter { get; private set; }
        public AccountViewModel Account { get; set; }

        public NavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
            _historic = new List<string>();

            AppSettings = (new SettingsBuilder()).Build();
        }
        public void GoBack()
        {
            if (_historic.Count > 1)
            {
                _historic.RemoveAt(_historic.Count - 1);
                NavigateTo(_historic.Last(), Parameter);
            }
        }
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                SetPageKey(pageKey, "MainFrame");
                Parameter = parameter;
            }
        }

        public virtual void ApplicationNavigateTo(string pageKey, AccountViewModel account)
        {
            lock (_pagesByKey)
            {
                SetPageKey(pageKey, "ApplicationFrame");
                Account = account;
            }
        }

        public virtual void OfflineNavigation(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                SetPageKey(pageKey, "ApplicationFrame");
                Parameter = parameter;
            }
        }

        private void SetPageKey(string pageKey, string frameName)
        {
            if (!_pagesByKey.ContainsKey(pageKey))
                throw new ArgumentException(string.Format("No such page: {0} ", pageKey), "pageKey");

            Frame frame = GetDescendantFromName(Application.Current.MainWindow, frameName) as Frame;

            if (frame != null)
                frame.Source = _pagesByKey[pageKey];

            _historic.Add(pageKey);
            CurrentPageKey = pageKey;
        }

        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                    _pagesByKey[key] = pageType;
                else
                    _pagesByKey.Add(key, pageType);
            }
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1) return null;

            for (int i = 0; i < count; i++)
            {
                FrameworkElement frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                        return frameworkElement;

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                        return frameworkElement;
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

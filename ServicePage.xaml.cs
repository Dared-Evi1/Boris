﻿using System;
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

namespace Borisin_автосервис
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var currentServices = BorisinAutoserviceEntities.GetContext().Service.ToList();
            ServiceListView.ItemsSource = currentServices;
            ComboType.SelectedIndex = 0;
            UpdateSerices();
        }
        private void UpdateSerices()
        {
            var currentServices = BorisinAutoserviceEntities.GetContext().Service.ToList();
            if (ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (Convert.ToDouble(p.Discount) >= 0 && Convert.ToDouble(p.Discount) <= 1)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (Convert.ToDouble(p.Discount) >= 0 && Convert.ToDouble(p.Discount) < 0.05)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (Convert.ToDouble(p.Discount) >= 0.05 && Convert.ToDouble(p.Discount) < 0.15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (Convert.ToDouble(p.Discount) >= 0.15 && Convert.ToDouble(p.Discount) < 0.30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (Convert.ToDouble(p.Discount) >= 0.30 && Convert.ToDouble(p.Discount) < 0.70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (Convert.ToDouble(p.Discount) >= 0.70 && Convert.ToDouble(p.Discount) < 1)).ToList();
            }
            currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            ServiceListView.ItemsSource = currentServices.ToList();
            if (RButtonDown.IsChecked.Value)
            {
                ServiceListView.ItemsSource= currentServices.OrderByDescending(p => p.Cost).ToList();
            }
            if (RButtonUp.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentServices.OrderBy(p => p.Cost).ToList();
            }
        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSerices();

        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSerices();

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSerices();

        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSerices();

        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Service));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BorisinAutoserviceEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ServiceListView.ItemsSource = BorisinAutoserviceEntities.GetContext().Service.ToList();
            }
        }
    }
}

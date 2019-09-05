/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Kelary.Infrastructure.Showcase"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Kelary.Infrastructure.Services;
using Kelary.Infrastructure.Showcase.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;

namespace Kelary.Infrastructure.Showcase.Services
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                //SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                //SimpleIoc.Default.Register<IDataService, DataService>();
                SetupNavigation();

                SimpleIoc.Default.Register<MainViewModel>();
                SimpleIoc.Default.Register<IFileDialogService, FileDialogService>();
                SimpleIoc.Default.Register<IDialogService, DialogService>();
            }

        }

        private void SetupNavigation()
        {
            var navigationService = new PageNavigationService();
            navigationService.Configure("first", new Uri("/View/FirstView.xaml", UriKind.Relative));
            navigationService.Configure("second", new Uri("/View/SecondView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            var windowNavigationService = new WindowNavigationService();
            windowNavigationService.Configure("dialog", new Uri("/View/DialogView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IWindowNavigationService>(() => windowNavigationService);
        }

        public INavigationService Navigation
        {
            get => ServiceLocator.Current.GetInstance<INavigationService>();
        }

        public MainViewModel Main
        {
            get => ServiceLocator.Current.GetInstance<MainViewModel>();
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
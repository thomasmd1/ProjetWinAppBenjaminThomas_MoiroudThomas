using DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WinAppBJ.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinAppBJ
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private User user;

        public GamePage()
        {
            this.InitializeComponent();
            this.DataContext = user;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as User;
            RoomViewModel rvm = new RoomViewModel(parameter);
            user = parameter;
        }

        private void OnclickDeconnect(object sender, RoutedEventArgs e)
        {
            RoomViewModel rvm = new RoomViewModel(user);
            rvm.DeconnectUser();
        }
    }
}

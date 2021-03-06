﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WinAppBJ.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void OnClickAddUser(object sender, RoutedEventArgs e)
        {

            string username = txbAddUsername.Text;
            string firstname = txbAddFirstname.Text;
            string lastname = txbAddLastname.Text;
            string email = txbAddEmail.Text;
            string password = txbAddPassword.Password;

            // Création d'un nouvel utilisateur avec en paramètre les champs renseigné par l'utilisateur
            User user = new User(username, firstname, lastname, email, password);

            UserViewModel vm = new UserViewModel();

            //Appel de la méthode addUser
            vm.addNewUser(user);


        }

        private void OnClickConnectUser(object sender, RoutedEventArgs e)
        {
            string email = txbEmail.Text;
            string password = passwordConnection.Password;
            //Conversion du mot de passe en MD5 
            string secret = Utils.ConvertToMD5.EncodeToMD5AndB64(password);

            User user = new User(email, password, secret);

            UserViewModel vm = new UserViewModel();
            //Appel de la méthode permettant à l'utilisateur de se connecter
            vm.connectUser(user);


        }
    }
}

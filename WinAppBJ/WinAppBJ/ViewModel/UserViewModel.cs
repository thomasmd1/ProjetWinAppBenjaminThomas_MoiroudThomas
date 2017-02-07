using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using Windows.UI.Popups;

namespace WinAppBJ.ViewModels
{
    class UserViewModel
    {

        public async void addNewUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(new { user = user });
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/register", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("l'utilisateur a bien été ajouté");
                    await dialog.ShowAsync();

                }
                else
                {
                    var dialog = new MessageDialog("Sa marche pas !!", json);
                    await dialog.ShowAsync();

                }

                ////    var dialog = new MessageDialog(json);
                ////    await dialog.ShowAsync();


            }


        }
        public async void connectUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(user);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/login", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("l'utilisateur est connecté");
                    await dialog.ShowAsync();

                }
                else
                {
                    var dialog = new MessageDialog("l'utilisateur n'existe pas");
                    await dialog.ShowAsync();

                }


            }
        }
    }
}

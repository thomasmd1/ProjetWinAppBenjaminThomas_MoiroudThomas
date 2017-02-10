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
using Windows.Data.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace WinAppBJ.ViewModels
{
    

    class UserViewModel
    {

        Frame actualFrame { get { return Window.Current.Content as Frame; } }
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
                    var res = await response.Content.ReadAsStringAsync();

                    var jobject = JObject.Parse(res);
                    var resultsToken = jobject["tokens"];
                    var resultsUser = jobject["user"];

                    user.tokens = resultsToken["access_token"].ToString();


                    user.firstname = resultsUser["firstname"].ToString();
                    user.lastname = resultsUser["lastname"].ToString();
                    user.email = resultsUser["email"].ToString();
                    user.stack = int.Parse(resultsUser["stack"].ToString());


                    actualFrame.Navigate(typeof(RoomPage));
                    var dialog = new MessageDialog(user.tokens);
                    await dialog.ShowAsync();
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog("Connexion refuse",res);
                    await dialog.ShowAsync();

                }


            }
            
        }
    }
}

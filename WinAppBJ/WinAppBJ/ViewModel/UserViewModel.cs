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

               var json = JsonConvert.SerializeObject(user);
               var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
               HttpResponseMessage response = await client.PostAsync("/api/auth/register", itemJson);
               if (response.IsSuccessStatusCode)
               {
                    var dialog = new MessageDialog("Sa marche !!");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Sa marche pas !!");
                    await dialog.ShowAsync();
                }

                ////    var dialog = new MessageDialog(json);
                ////    await dialog.ShowAsync();


            }
        }
    }
}

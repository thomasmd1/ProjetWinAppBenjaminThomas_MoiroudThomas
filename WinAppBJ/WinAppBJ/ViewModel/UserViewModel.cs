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
            var json = JsonConvert.SerializeObject(user);
            var dialog = new MessageDialog(json);
            await dialog.ShowAsync();

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://demo.comte.re//api/auth/register");

                
            //    //var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
            //    //HttpResponseMessage response = await client.PostAsync("uri/to/call", itemJson);
            //    //if (response.IsSuccessStatusCode)
            //    //{
            //    //}

            //    var dialog = new MessageDialog(json);
            //    await dialog.ShowAsync();


            //}
        }
    }
}

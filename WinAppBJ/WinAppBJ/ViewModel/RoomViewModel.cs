﻿using DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinAppBJ.ViewModel
{
    class RoomViewModel
    {
        Frame actualFrame { get { return Window.Current.Content as Frame; } }

        public async void GetPlayerOnline()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/user/connected");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();

                    var jobject = JObject.Parse(res);

                }
            }
        }

        public async void GetOpenedTable()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/table/opened");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async void DeconnectUser(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(user.email);
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/auth/logout", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var dialog = new MessageDialog("Vous avez été déconnecté");
                    await dialog.ShowAsync();

                    actualFrame.Navigate(typeof(LoginPage));
                    

                }
            }
        }
    }
}

using DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WinAppBJ.ViewModel
{
    class RoomViewModel
    {
        Frame actualFrame { get { return Window.Current.Content as Frame; } }
        public Room Room { get; set; }
        public List<User> Onlines { get; set; }
        public Table Tables { get; set; }
        public List<Table> Free { get; set; }

        private User user;

        //private Api _api;

        //public Api Api
        //{
        //    get { return _api; }
        //    set
        //    {
        //        SetProperty(ref this._api, value);
        //    }
        //}

        //private void SetProperty(ref Api _api, Api value)
        //{
        //    throw new NotImplementedException();
        //}

        public RoomViewModel(User u)
        {
            this.user = new User();
            this.user = u;

            Onlines = new List<User>();
            Free = new List<Table>();
            Room = new Room();
            getPlayerOnline();
            //GetOpenedTable();
        }

        public async void getPlayerOnline()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("authorization", "Bearer "+ this.user.tokens);
                
                HttpResponseMessage response = await client.GetAsync("/api/user/connected");
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(res);
                    var resultsUser = jobject["users"];

                    //Parcourt du résultat obtenus apres le call Api.
                    for (int i = 0; i < resultsUser.Count(); i++)
                    {
                        var item = resultsUser.ElementAt(i);
                        User u = new User()
                        {
                            firstname = item["firstname"].ToString(),
                            lastname = item["lastname"].ToString()
                        };
                        //On récupère le firstname et lastname puis on les ajoutes à la liste Online
                        Onlines.Add(u);
                    }

                    //var dialog = new MessageDialog(resultsUser.ToString(), "Passed");
                    //await dialog.ShowAsync();
                    Room.Users = Onlines;
                    //Redirection vers la page Room
                    actualFrame.DataContext = Room;
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog(res, "Failed");
                    await dialog.ShowAsync();

                }
            }
        }

        public async void GetOpenedTable()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("authorization", "Bearer " + this.user.tokens);

                HttpResponseMessage response = await client.GetAsync("/api/table/opened");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(res);
                    var resultsTables = jobject["tables"];

                    //Parcourt du résultat obtenus apres le call Api.
                    for (int i = 0; i < resultsTables.Count(); i++)
                    {
                        var item = resultsTables.ElementAt(i);
                        Table t = new Table();
                        {
                            var id = item["id"];
                        };
                        //On récupère le firstname et lastname puis on les ajoutes à la liste Online
                        Free.Add(t);
                        
                    }
                    //var dialog = new MessageDialog(res, "Passed");
                    //await dialog.ShowAsync();
                    Room.Tables = Free;
                    actualFrame.DataContext = Room;
                } else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog(res, "Failed");
                    await dialog.ShowAsync();
                }
            }
        }
        //Call Api permettant la déconnexion de l'utilisateur
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

                    //Redirection vers la page "LoginPage"
                    actualFrame.Navigate(typeof(LoginPage));


                }
            }
        }

        
    }
}


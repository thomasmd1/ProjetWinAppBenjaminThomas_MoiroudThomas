using DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WinAppBJ.ViewModel
{
    class RoomViewModel
    {
        public async void getPlayerOnline(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");

                var json = JsonConvert.SerializeObject(new { user = user });
                var itemJson = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/api/user/connected", itemJson);
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
    }
}

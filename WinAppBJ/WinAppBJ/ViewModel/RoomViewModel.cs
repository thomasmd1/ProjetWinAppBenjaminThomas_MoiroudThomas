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

namespace WinAppBJ.ViewModel
{
    class RoomViewModel
    {
        Frame actualFrame { get { return Window.Current.Content as Frame; } }
        public Room Room { get; set; }
        public List<User> Onlines { get; set; }
        public Table Tables { get; set; }
        public List<Table> Free { get; set; }

        public RoomViewModel()
        {
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
                client.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6ImJiNWE0ZDNiYjljZGRlOTI0Mjc1MWUwNTRhOTJkNWQ4Y2NkZGMwMmYxODhlMTZkMGExMDBjNjAzMWVhN2QzZTAxNTA2NGE1NTRiOTc0ZmI4In0.eyJhdWQiOiJ0ZXN0QHlub3YuY29tIiwianRpIjoiYmI1YTRkM2JiOWNkZGU5MjQyNzUxZTA1NGE5MmQ1ZDhjY2RkYzAyZjE4OGUxNmQwYTEwMGM2MDMxZWE3ZDNlMDE1MDY0YTU1NGI5NzRmYjgiLCJpYXQiOjE0ODY4MjYyMjYsIm5iZiI6MTQ4NjgyNjIyNiwiZXhwIjoxNTE4MzYyMjI2LCJzdWIiOiIyNCIsInNjb3BlcyI6WyIqIl19.Tn2YvRYID2WikVK8BpA2dMb8iqUndDmFpNfJ4_oWJmhmY-ztNM6Fx8hnzJgKiVRB9WTq2P5ohtlb-FjHq5W9rnqcm8PyuujIvGvw-yhXdDqRkI6IPg8IYnYUhSiQ0vBjWnAzahZWWhTY5AnqAAp-6W1b2qCkmnSttqCdJObaC8i12o8BmLYjx5jaP4Odh2VPt4MY5XP7l1Rq4svHBc3OQ6MPgd6d2Kvx3I-Ox-7LEiksKgDnsjQsVdznNxzObQ3F4ebVwFvhchcctRstEJGiy6hZo2UkAuRL0zNabwWE8Qnut-rXys59O6WzkLsHJ_p3B9ZR5SK79HIeFZlSBd1-XfFtc1fRVY_wOXZXhONoBgQKzWRB-FQj3Ij0dxRNUR4P1t4RyRK1PNKijHfFuohTGm4DEEyJNS3BPSJE9McdmQ8pZxk_O4s4cM2RYfwpZQMiCj5w_MAdl6wLqPlz3WTVlo4pofOT05tiR_U-seNaUXU1aGu5vgNP4ByadF80irqb-sDaO7FTzJZU2vgBGa1QvYuw9fpVEP8HHlWsdn63HWU8lmhAC7Rqj__t7vo_rno0ptiENhy9nTBOlUB-nezWaYibs1UcTkv9TYjj5IoxTaRXmTwv5Fm4wl-gg26nzCbUkkmPbUK6GB1fHe1gUv6ByKn00jNvIAr0FaEcxB3d0Gg");
                
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
                client.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6ImJiNWE0ZDNiYjljZGRlOTI0Mjc1MWUwNTRhOTJkNWQ4Y2NkZGMwMmYxODhlMTZkMGExMDBjNjAzMWVhN2QzZTAxNTA2NGE1NTRiOTc0ZmI4In0.eyJhdWQiOiJ0ZXN0QHlub3YuY29tIiwianRpIjoiYmI1YTRkM2JiOWNkZGU5MjQyNzUxZTA1NGE5MmQ1ZDhjY2RkYzAyZjE4OGUxNmQwYTEwMGM2MDMxZWE3ZDNlMDE1MDY0YTU1NGI5NzRmYjgiLCJpYXQiOjE0ODY4MjYyMjYsIm5iZiI6MTQ4NjgyNjIyNiwiZXhwIjoxNTE4MzYyMjI2LCJzdWIiOiIyNCIsInNjb3BlcyI6WyIqIl19.Tn2YvRYID2WikVK8BpA2dMb8iqUndDmFpNfJ4_oWJmhmY-ztNM6Fx8hnzJgKiVRB9WTq2P5ohtlb-FjHq5W9rnqcm8PyuujIvGvw-yhXdDqRkI6IPg8IYnYUhSiQ0vBjWnAzahZWWhTY5AnqAAp-6W1b2qCkmnSttqCdJObaC8i12o8BmLYjx5jaP4Odh2VPt4MY5XP7l1Rq4svHBc3OQ6MPgd6d2Kvx3I-Ox-7LEiksKgDnsjQsVdznNxzObQ3F4ebVwFvhchcctRstEJGiy6hZo2UkAuRL0zNabwWE8Qnut-rXys59O6WzkLsHJ_p3B9ZR5SK79HIeFZlSBd1-XfFtc1fRVY_wOXZXhONoBgQKzWRB-FQj3Ij0dxRNUR4P1t4RyRK1PNKijHfFuohTGm4DEEyJNS3BPSJE9McdmQ8pZxk_O4s4cM2RYfwpZQMiCj5w_MAdl6wLqPlz3WTVlo4pofOT05tiR_U-seNaUXU1aGu5vgNP4ByadF80irqb-sDaO7FTzJZU2vgBGa1QvYuw9fpVEP8HHlWsdn63HWU8lmhAC7Rqj__t7vo_rno0ptiENhy9nTBOlUB-nezWaYibs1UcTkv9TYjj5IoxTaRXmTwv5Fm4wl-gg26nzCbUkkmPbUK6GB1fHe1gUv6ByKn00jNvIAr0FaEcxB3d0Gg");

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


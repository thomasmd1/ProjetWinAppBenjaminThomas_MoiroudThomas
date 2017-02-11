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
        public Room Rooms { get; set; }
        public List<User> Onlines { get; set; }
        public Table Tables { get; set; }
        public List<Table> Free { get; set; }

        public RoomViewModel()
        {
            Onlines = new List<User>();
            Free = new List<Table>();
            getPlayerOnline();

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

                    for (int i = 0; i < resultsUser.Count(); i++)
                    {
                        var item = resultsUser.ElementAt(i);
                        User u = new User()
                        {
                            firstname = item["firstname"].ToString(),
                            lastname = item["lastname"].ToString()
                        };
                        Onlines.Add(u);
                    }

                    //var dialog = new MessageDialog(resultsUser.ToString(), "Passed");
                    //await dialog.ShowAsync();

                    actualFrame.DataContext = Onlines;

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
                client.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjRmNzU2ZmJmYTc2ZWVjZjRkMTdhMjcxNjgwZTEzZGNmODBiYTEwMWQ4MzI3YjczMmQyNzMyNjdkODMxNzA1ZmY2OTM2ZTE4ZDVmOTg1MmU0In0.eyJhdWQiOiJ0ZXN0QHlub3YuY29tIiwianRpIjoiNGY3NTZmYmZhNzZlZWNmNGQxN2EyNzE2ODBlMTNkY2Y4MGJhMTAxZDgzMjdiNzMyZDI3MzI2N2Q4MzE3MDVmZjY5MzZlMThkNWY5ODUyZTQiLCJpYXQiOjE0ODY3NDUyNjgsIm5iZiI6MTQ4Njc0NTI2OCwiZXhwIjoxNTE4MjgxMjY4LCJzdWIiOiIyNCIsInNjb3BlcyI6WyIqIl19.BhFaaCMNCRGMMHaEshKzQSkRIPkz5dS0XjeLqZlegSxTmB2unTqurHR_ODA6HWw1bwlBq7f4DoUGmJAq3YCDU88GMj3Fnxtinb4Q0EDlIHqwqKnMWOeSS4WUccvtaO4QIIpWa2ynrxajd49w8OAjaJTjWqQJ-Y2N8-zsSkQ_LMtjtkTEt0pjMckXIVkMjGF_MDGWA87clP0g_bp1WnxTuLB7HcqPZzcGMWi6CkPGlkRmYrnJpAxbBzlrDOKDi-1u7N5eR9WIOHF0E-jHktQTbl-FnJa3Fq8blZmEPTZ0l5alLUcaGnCo6P6YgDOGoamdnrUVsIfCc-x4A7wrBvmc7iMSGPLao9wyDUsticYVVRHGBKOHVzHW1PXb506vtu7FYTazYLgEtq9CPfFTI3k8vFqmj51k7ayCTHwGoWQIV0Ztk09Tk1bpSMQMDiwvzbN59Uxm07m0TTCgWLHBbHq3kvyYJJmUNahasGsV3pBv2K9UF0obzyPZP2cLtgvvrJd9h9El7-1EZZdWQLxwj52hVhcEFRgDT1HxbQxLWtJjuVI4ccHsubFaJxBoDyFaqzBRkwFfxcnwTInahErzZo8-qok6BM9sbOMkWYu2VtL18e7AZcFuFV1OpqkG2rIhRUoKQqOFiMJ9tlKHitpTucPjv7bcg_1wt5ElVgIKLa3DbX4");

                HttpResponseMessage response = await client.GetAsync("/api/table/opened");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(res);
                    var resultsTables = jobject["tables"];

                    for (int i = 0; i < resultsTables.Count(); i++)
                    {
                        var item = resultsTables.ElementAt(i);
                        Table t = new Table();
                        {
                            id = item["id"],
                        };
                        Free.Add(t);
                    }
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


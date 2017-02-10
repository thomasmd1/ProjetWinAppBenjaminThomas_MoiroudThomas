using DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WinAppBJ.ViewModel
{
    class RoomViewModel
    {
        public Room Rooms { get; set; }
        public List<User> Onlines { get; set; }

        public RoomViewModel()
        {
            Onlines = new List<User>();
            getPlayerOnline();

        }

        public async void getPlayerOnline()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://demo.comte.re");
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjRmNzU2ZmJmYTc2ZWVjZjRkMTdhMjcxNjgwZTEzZGNmODBiYTEwMWQ4MzI3YjczMmQyNzMyNjdkODMxNzA1ZmY2OTM2ZTE4ZDVmOTg1MmU0In0.eyJhdWQiOiJ0ZXN0QHlub3YuY29tIiwianRpIjoiNGY3NTZmYmZhNzZlZWNmNGQxN2EyNzE2ODBlMTNkY2Y4MGJhMTAxZDgzMjdiNzMyZDI3MzI2N2Q4MzE3MDVmZjY5MzZlMThkNWY5ODUyZTQiLCJpYXQiOjE0ODY3NDUyNjgsIm5iZiI6MTQ4Njc0NTI2OCwiZXhwIjoxNTE4MjgxMjY4LCJzdWIiOiIyNCIsInNjb3BlcyI6WyIqIl19.BhFaaCMNCRGMMHaEshKzQSkRIPkz5dS0XjeLqZlegSxTmB2unTqurHR_ODA6HWw1bwlBq7f4DoUGmJAq3YCDU88GMj3Fnxtinb4Q0EDlIHqwqKnMWOeSS4WUccvtaO4QIIpWa2ynrxajd49w8OAjaJTjWqQJ-Y2N8-zsSkQ_LMtjtkTEt0pjMckXIVkMjGF_MDGWA87clP0g_bp1WnxTuLB7HcqPZzcGMWi6CkPGlkRmYrnJpAxbBzlrDOKDi-1u7N5eR9WIOHF0E-jHktQTbl-FnJa3Fq8blZmEPTZ0l5alLUcaGnCo6P6YgDOGoamdnrUVsIfCc-x4A7wrBvmc7iMSGPLao9wyDUsticYVVRHGBKOHVzHW1PXb506vtu7FYTazYLgEtq9CPfFTI3k8vFqmj51k7ayCTHwGoWQIV0Ztk09Tk1bpSMQMDiwvzbN59Uxm07m0TTCgWLHBbHq3kvyYJJmUNahasGsV3pBv2K9UF0obzyPZP2cLtgvvrJd9h9El7-1EZZdWQLxwj52hVhcEFRgDT1HxbQxLWtJjuVI4ccHsubFaJxBoDyFaqzBRkwFfxcnwTInahErzZo8-qok6BM9sbOMkWYu2VtL18e7AZcFuFV1OpqkG2rIhRUoKQqOFiMJ9tlKHitpTucPjv7bcg_1wt5ElVgIKLa3DbX4");
                
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


                    var dialog = new MessageDialog(resultsUser.ToString(), "Passed");
                    await dialog.ShowAsync();

                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var dialog = new MessageDialog(res, "Failed");
                    await dialog.ShowAsync();

                }
            }
        }
    }
}

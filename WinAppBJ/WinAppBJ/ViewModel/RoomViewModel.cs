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
                client.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6Ijg5YWNiOTA3MWExNWM0NzIyOGE0ZjMxNTdlYzhlYzJhOTU2NWY3NmFlOTNmNDdhN2M0OWU0ODY5OWExYTc3NGE2NjdlNjRkOTk5Y2EwYTY5In0.eyJhdWQiOiJ0ZXN0QHlub3YuY29tIiwianRpIjoiODlhY2I5MDcxYTE1YzQ3MjI4YTRmMzE1N2VjOGVjMmE5NTY1Zjc2YWU5M2Y0N2E3YzQ5ZTQ4Njk5YTFhNzc0YTY2N2U2NGQ5OTljYTBhNjkiLCJpYXQiOjE0ODY2Njc5ODAsIm5iZiI6MTQ4NjY2Nzk4MCwiZXhwIjoxNTE4MjAzOTgwLCJzdWIiOiIyNCIsInNjb3BlcyI6WyIqIl19.b2aWKTiyCCTbAqzF6-TsNLUGywfp1T_79vnGLzkKj_y7MRPzb1V1tPMVXlm4M4uvILeK4T5QDfs8JW7tQXmXrgQ2jUvfo7wiqcV7BFq4xOIHAJTeaEzCJ81HdpAIHvv-LGKvugz_J7bUBXqMtDTBVdFhKnm5mkP9J7CDzoq_xWbqVL64n_MWm44UwvOsFsyEilX7do5pVvO09eaMkTD7Z6_Juak0QHdQsqXCezca0bPuKhiVf8UK6ujcjEqxdXmIfoHODX2CtvtM6tyHQ_wtYKTNE3m6aKwlUmxnou9E9zu-W7B4Sy2m0ZJqLikUmwxaJM5vhIFd08DY-SqaU5KP16-AhzEMi_BGQ4DFnT_qBjKaGx5aqEa2FnnuO2UomybrCm9Fo2SDsRzFoLT7SEJR6rI4cOC3NiGXfLgX8fmzFBjySwKGq-ihleKhMSrPuQoq6UAHitdHGfCpmNwq7Aur_By30IoyHwVP_DPs_veSj8uACVa4cJ9jlGcPMw-nOOC6O1DV_f1-JupI-dvTi7DKpRBxmeZtaHpJQtOTI1bE9JGhszDDJDLfMRhh-PjqnA3Ouie4MW-79ai4kVyKGVIf0-0S9iW6jwiYdQJ7RwkKwqD7_bfOGLvuGGjnbCyWjIEC-wHxLCNT7Q1OCl4njVBJiRpDGd8AMUBCzqz5BdNo97M");
                
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

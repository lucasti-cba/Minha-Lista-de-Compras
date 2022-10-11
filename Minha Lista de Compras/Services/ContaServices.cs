
using Flurl;
using Flurl.Http;
using Minha_Lista_de_Compras.Data;
using Minha_Lista_de_Compras.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Minha_Lista_de_Compras.Services
{





    public struct structCadastro
    {

        public string username { get; set; }
        public string password { get; set; }
        public string password2 { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }

    public struct structLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }



    public struct structLoginReturn
    {
        public string access { get; set; }
        public string refresh { get; set; }
    }


    internal class ContaServices
    {







        public async Task<bool> login(Conta acc)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://barber-auth.herokuapp.com/api/token/");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new structLogin() { username = acc.Usuario, password = acc.Senha });
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                structLoginReturn accret = JsonConvert.DeserializeObject<structLoginReturn>(response.Content.ReadAsStringAsync().Result);
                acc.Token = accret.access;
                acc.Refresh = accret.refresh;
                ContaDataBase database = await ContaDataBase.Instance;
                await database.SaveItemAsync(acc);
            }

            return response.IsSuccessStatusCode;
        }
    }
}
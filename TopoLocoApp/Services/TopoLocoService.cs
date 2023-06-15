using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopoLocoApp.Models;

namespace TopoLocoApp.Services
{
    public class TopoLocoService
    {
        HttpClient client;
        public TopoLocoService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://topoloco.sistemas19.com/")
            };
        }

        public async Task<List<Usuario>> Get()
        {
            List<Usuario> salas = null;

            var response = await client.GetAsync("api/Usuarios");

            if (response.IsSuccessStatusCode) //status= 200 ok
            {
                var json = await response.Content.ReadAsStringAsync();
                salas = JsonConvert.DeserializeObject<List<Usuario>>(json);
            }

            if (salas != null)
            {
                return salas;
            }
            else
            {
                return new List<Usuario>();
            }
        }

        public async Task<bool> Insert(Usuario u)
         {
            var json = JsonConvert.SerializeObject(u);
            StringContent scontent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/Usuarios", scontent);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Update(Usuario u)
        {
            var json = JsonConvert.SerializeObject(u);
            StringContent scontent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PutAsync("api/Usuarios", scontent);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task NuevaPuntuacion()
        {
            await client.GetAsync("api/Usuarios/NuevaPuntuacion");
        }
    }
}

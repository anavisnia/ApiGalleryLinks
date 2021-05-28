using ApiGalleryLinks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGalleryLinks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
             1. Sukurti Konsolinę Applikaciją, kuri atspausdina visas nuotraukų nuorodas paimtas iš RestAPI
            https://jsonplaceholder.typicode.com/ kurios priklauso 'Mrs. Dennis Schulist'. Patarimas, naudokite HttpClient klasę.
            Extra: programoje panaudokite Linq Selectus.

            2. Parašyti WebScraper konsolinę programą, kuri išspausdina darbo skelbimų pavadinimus (pavyzdžiui: 'Technical Writer')
            iš https://www.cvonline.lt/darbo-skelbimai/informacines-technologijos pirmojo puslapio.
            Patarimas: Instaliuokite, panaudokite 'HTMLAgilityPack Nuget package' savo programoje.
             */

            var httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

            if(httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentSrting = await httpResponse.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<List<User>>(contentSrting);


                var specificUser = users.Where(u => u.Name == "Mrs. Dennis Schulist").FirstOrDefault();

                

            }

        }
    }
}

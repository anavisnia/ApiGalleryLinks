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

            // to get user
            if(httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentSrting = await httpResponse.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<List<User>>(contentSrting);

                var specificUser = users.Where(u => u.Name == "Mrs. Dennis Schulist").FirstOrDefault();

                httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");

                // to get a particualr user's album
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    contentSrting = await httpResponse.Content.ReadAsStringAsync();

                    var albums = JsonConvert.DeserializeObject<List<Album>>(contentSrting);

                    var specificAlbumId = albums.Where(u => u.UserId == specificUser.Id).FirstOrDefault();

                    httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/photos");

                    // to get from the album urls of photos
                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        contentSrting = await httpResponse.Content.ReadAsStringAsync();

                        var photos = JsonConvert.DeserializeObject<List<Photo>>(contentSrting);

                        var photosURL = photos.Where(a => a.AlbumId == specificAlbumId.UserId);

                        foreach (var oneByOneUrl in photosURL)
                        {
                            Console.WriteLine(oneByOneUrl.Url);
                        }

                    }

                }

            }

            

        }
    }
}

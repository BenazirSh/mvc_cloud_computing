using DSCC.MVC._7924.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC.MVC._7924.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            //Hosted web API REST Service base url
            string Baseurl = "https://localhost:44336/";
            List<Client> ProdInfo = new List<Client>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new
               MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Clients using HttpClient
                 HttpResponseMessage Res = await client.GetAsync("api/Product");
                //Checking the response is successful or not which is sent using HttpClient
            if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                     ProdInfo = JsonConvert.DeserializeObject<List<Client>>(PrResponse);
                }
                //returning the Product list to view
                return View(ProdInfo);
            }
        }
        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string Baseurl = "https://localhost:44336/";
            Client student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Client/" + id);
             
   
            if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into
                     student = JsonConvert.DeserializeObject<Client>(PrResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(student);
        }
        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Client prod)
        {
            try
            {
                // TODO: Add update logic here
                string Baseurl = "https://localhost:44336/";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.GetAsync("api/Client/" + id);
                   Client student = null;
                    //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Client list
                        student = JsonConvert.DeserializeObject<Client>(PrResponse);
                    }
                    prod.Plant= student.Plant;
                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Client>("api/Client/" + prod.Id,   prod);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            //return View(student);
 return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
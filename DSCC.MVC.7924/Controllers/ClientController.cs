using DSCC.MVC._7924.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC.MVC._7924.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        string Baseurl = "http://dsccmicroservicesapi7924-dev.us-east-2.elasticbeanstalk.com";
        private string urlStarter = "/api/Client";
        public async Task<ActionResult> Index()
        {
            var clientList = new List<Client>();
            string bodyResponse = null;

            var client = new HttpClient();
            var response = await client.GetAsync(Baseurl + urlStarter);
            if (response.IsSuccessStatusCode)
            {
                bodyResponse = await response.Content.ReadAsStringAsync();
                clientList = JsonConvert.DeserializeObject<List<Client>>(bodyResponse);
            }

            return View(clientList);
        }

        // GET: ClientsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Client item = await GetClient(id);

            return View(item);
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Client collection)
        {
            try
            {
                var client = new HttpClient();
                var jsonSerializer = JsonConvert.SerializeObject(collection);
                var data = new StringContent(jsonSerializer, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Baseurl + urlStarter, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Client item = await GetClient(id);
            return View(item);
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Client collection)
        {
            try
            {
                var client = new HttpClient();
                var jsonSerializer = JsonConvert.SerializeObject(collection);
                var data = new StringContent(jsonSerializer, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(Baseurl + urlStarter + "/" + id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
             Client item = await GetClient(id);
             return View(item); 
        }

        // POST: ClientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Client collection)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.DeleteAsync(Baseurl + urlStarter + "/" + id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<Client> GetClient(int id)
        {
            Client item = null;
            var client = new HttpClient();
            var response = await client.GetAsync(Baseurl + urlStarter + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<Client>(content);
            }
            return item;
        }
    }
}

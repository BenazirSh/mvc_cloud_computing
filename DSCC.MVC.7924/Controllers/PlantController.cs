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
    public class PlantController : Controller
    {
        // GET: Plant
        string Baseurl = "https://localhost:44336/";
        private string urlStarter = "api/Plant";
        public async Task<ActionResult> Index()
        {
            var list = new List<Plant>();
            string content = null;

            var client = new HttpClient();
            var response = await client.GetAsync(Baseurl + urlStarter);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Plant>>(content);
            }

            return View(list);
        }

        // GET: PlantsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Plant item = await GetPlant(id);

            return View(item);
        }

        // GET: PlantsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Plant collection)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(collection);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Baseurl + urlStarter, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Plant item = await GetPlant(id);
            return View(item);
        }

        // POST: PlantsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Plant collection)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(collection);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(Baseurl + urlStarter + "/" + id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Plant item = await GetPlant(id);
            return View(item);
        }

        // POST: PlantsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Plant collection)
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

        public async Task<Plant> GetPlant(int id)
        {
            Plant item = null;
            var client = new HttpClient();
            var response = await client.GetAsync(Baseurl + urlStarter + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<Plant>(content);
            }
            return item;
        }

    }
}

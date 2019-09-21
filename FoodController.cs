using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class FoodController : ApiController
    {
        public FoodDAOMSSQL foodDao = new FoodDAOMSSQL();
        public FoodController()
        {
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            if (foodDao.GetAll().Count == 0) //No Content..
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, foodDao.GetAll());
        }

        // GET api/values/5
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Food food = foodDao.GetById(id);
            if (food == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, food);
        }

        // POST api/values
        [System.Web.Http.HttpPost]
        public void Post([FromBody]Food value)
        {
            foodDao.AddFood(value);
            Request.CreateResponse(HttpStatusCode.Created, value);
        }

        // PUT api/values/5
        [System.Web.Http.HttpPut]
        public void Put(int id, [FromBody]Food value)
        {
            Food food = foodDao.GetById(id);
            if (food != null)
            {
                foodDao.UpdateFoods(food);
            }
        }
        [System.Web.Http.Route("api/Food/byname/{name}")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFoodByName([FromUri]string name)
        {
            List<Food> foods = foodDao.GetFoodByName(name);
            if (foods == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "There is no food with this name");
            }
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }
        [System.Web.Http.Route("api/Food/bycal/{cal}")]
        public HttpResponseMessage GetFoodByCal([FromUri]int cal)
        {
            List<Food> foods = foodDao.GetFoodByCal(cal);
            if (foods == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }
        [System.Web.Http.Route("api/Food/search")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetByFilter(string name="" , int grade=0, int minCal=0, int maxCal=int.MaxValue)
        {
            List<Food> foods = foodDao.GetByCriteria(name, grade, minCal, maxCal);
            if(foods==null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }
        // DELETE api/values/5
        public void Delete(int id)
        {
            Food food = foodDao.GetById(id);
            if(food!=null)
            {
                foodDao.DeleteFood(id);
            }
        }
        // GET: Food
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}
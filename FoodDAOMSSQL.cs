using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace WebApplication2.Models
{
    public class FoodDAOMSSQL
    {
        public List<Food> GetAll()
        {
            List<Food> foodList;
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                foodList = connection.Foods.ToList();
            }
            return foodList;
        }

        public Food GetById(int id)
        {
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                return connection.Foods.FirstOrDefault(fod => fod.ID == id);
            }
        }
        public void AddFood(Food food)
        {
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                connection.Foods.Add(food);
                connection.SaveChanges();
            }
        }
        public List<Food> GetFoodByName(string foodName)
        {
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                return connection.Foods.Where(fod => fod.Name.ToUpper().Contains(foodName)).ToList();
            }
        }
        public List<Food> GetFoodByCal(int cal)
        {
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                return connection.Foods.Where(fod => fod.Calories > cal).ToList();
            }
        }
        public List<Food> GetByCriteria(string name , int grade, int minCal, int maxCal)
        {
            List<Food> foods = new List<Food>();
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                return connection.Foods.Where(fod => fod.Grade==grade &&fod.Calories>minCal &&fod.Calories<maxCal || fod.Name.ToUpper().Contains(name)).ToList();
            }
        }
        public void DeleteFood(int id)
        {
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                connection.Foods.Remove(connection.Foods.FirstOrDefault(fod => fod.ID == id));
                connection.SaveChanges();
            }
        }
        public void UpdateFoods(Food food)
        {
            Food foodToUpdate = new Food();
            using (FoodDBEntities connection = new FoodDBEntities())
            {
                foodToUpdate = connection.Foods.FirstOrDefault(fod => fod.ID == food.ID);
                foodToUpdate.Name = food.Name;
                foodToUpdate.Ingridients = food.Ingridients;
                foodToUpdate.Calories = food.Calories;
                foodToUpdate.Grade = food.Grade;
                connection.SaveChanges();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant {Id=1,Name="Here"},
                new Restaurant {Id=2, Name ="There"}
            };
        }  

        public IEnumerable<Restaurant> GetAll()=> _restaurants.OrderBy(r=> r.Name);
        
        List<Restaurant> _restaurants;

    }
}

using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Model
{
    public class ResponseRepository
    {
        private DishModel? _dish;

       public void Add(DishModel? dish)
        {
            _dish = dish;
        }

        public DishModel? Get()
        {
            return _dish;
        }
        
    }
}

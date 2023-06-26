using Microsoft.EntityFrameworkCore;
using SaidalyTechBackEnd.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaidalyTechBackEnd
{
    public class Class1
    {

        public async void GetData()
        {
            using (var _context = new PharmacyContext())
            {
               var data =   _context.TbCategories.ToList();
            }
        }
    }
}

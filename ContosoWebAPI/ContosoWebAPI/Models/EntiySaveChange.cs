using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoWebAPI.Models
{
    public partial class ContosouniversityContext
    {
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();
            foreach (var entry in entities) 
            {
                if (entry.State == EntityState.Modified) 
                {
                    entry.CurrentValues.SetValues(new { DateModified = DateTime.Now });
                }
            }
            return base.SaveChanges();
        }
    }
}

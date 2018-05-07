using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GummiBearKingdom.Models
{
    public interface IGummiRepository
    {
        IQueryable<Product> Products { get; }
        Product Save(Product product);
        Product Edit(Product product);
        void Remove(Product product);
        void RemoveAll();
        IQueryable<Category> Categories { get; }
        Category Save(Category category);
        Category Edit(Category category);
        IQueryable<Review> Reviews { get; }
        Review Save(Review review);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJZ.SearchHelper.Client.Model
{
    public class Category
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; }

        public static List<Category> getMenu()
        {
            List<Category> list = new List<Category>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Category()
                {
                    Uid = "user-guid-1",
                    Name = "分类" + i,
                    Guid = "category-guid-" + i
                });
            }
            return list;
        }

        public static Category getCategoryByGuid()
        {
            return new Category();
        }

        internal static void save(Category tCategory)
        {
            throw new NotImplementedException();
        }

        internal static void delete(Category SelectedCategory)
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

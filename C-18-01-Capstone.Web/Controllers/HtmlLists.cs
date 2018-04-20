using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using System.Collections.Generic;
using System.Linq;

namespace C_18_01_Capstone.Web.Controllers
{
    public static class HtmlLists
    {
        public static IEnumerable<Country> Countries =
                            new EfDataAccess<Country>()
                                .GetEntities()
                                .OrderBy(_ => _.Name)
                                .ToList();
    }
}
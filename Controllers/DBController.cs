using Microsoft.AspNetCore.Mvc;
using esabzi.DB;

namespace esabzi.Controllers
{
    public class DBController : Controller
    {
        private readonly EsabziContext _context;

        public DBController()
        {
            _context = new EsabziContext();
        }

        
        public string Index()
        {
            return "DB CONTROLLER";
        }
    }
}

using System.Runtime.CompilerServices;
using System.ServiceProcess;
using Cube4solo.Datas;
using Cube4solo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cube4solo.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            List<Services> list = _context.Services.ToList();
            Response.ContentType = "text/html";
            return View(list);
        }
        
        public IActionResult Edit(int id)
        {
            Services Services = GetServiceById(id);
            return View(Services);
        }

        public IActionResult Details(int id)
        {
            Services Services = GetServiceById(id);
            return View(Services);
        }

        [HttpGet("Services/add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("Services")]
        public IActionResult GetServices()
        {
            List<Services> myService = _context.Services.ToList();

            if (myService.Count > 0)
            {
                List<Services> list = _context.Services.ToList();
                return View("Index", list);
            }
            else
            {
                return View("Create");
            }
        }

        [HttpGet("Services/{serviceId}")]
        public Services GetServiceById(int serviceId)
        {
            return _context.Services.FirstOrDefault(x => x.Id == serviceId);
        }

        [HttpPost("Services/edit")]
        public IActionResult EditService(ServicesDTO newInfos)
        {
            Services? findService = _context.Services.FirstOrDefault(x => x.Id == newInfos.Id);

            if (findService != null)
            {
                findService.Name = newInfos.Name;

                _context.Services.Update(findService);
                if (_context.SaveChanges() > 0)
                {
                    List<Services> Services = _context.Services.ToList();
                    return View("Index", Services);
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = "Une erreur a eu lieu durant la modification..."
                    });
                }
            }
            else
            {
                return NotFound(new
                {
                    Message = "Aucun site n'a ??t?? trouv?? avec cet ID !"
                });
            }
        }

        [HttpPost("Services/add")]
        public IActionResult AddService(ServicesDTO newService)
        {
            Services addService = new Services()
            {
                Name = newService.Name
            };
            _context.Services.Add(addService);
            if (_context.SaveChanges() > 0)
            {
                List<Services> list = _context.Services.ToList();
                return View("Index", list);
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Une erreur est survenu"
                });
            }
        }
        
        public IActionResult DeleteService(int id)
        {
            Services? findService = _context.Services.FirstOrDefault(x => x.Id == id);

            if (findService == null)
            {
                return NotFound(new
                {
                    Message = "Aucun service trouv??"
                });
            }
            else
            {
                _context.Services.Remove(findService);
                if (_context.SaveChanges() > 0)
                {
                    List<Services> list = _context.Services.ToList();
                    return View("Index", list);
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = "Une erreur est survenu"
                    });
                }
            }
        }
    }
}
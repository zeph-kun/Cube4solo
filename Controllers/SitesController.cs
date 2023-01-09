using Cube4solo.Datas;
using Cube4solo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Cube4solo.Controllers
{
    public class SitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SitesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            List<Sites> list = _context.Sites.ToList();
            Response.ContentType = "text/html";
            return View(list);
        }
        
        public IActionResult Details(int id)
        {
            Sites Sites = GetSiteById(id);
            return View(Sites);
        }

        [HttpGet("sites/add")]
        public IActionResult Create()
        {
            return View();
        }
        
        public IActionResult Edit(int id)
        {
            Sites Sites = GetSiteById(id);
            return View(Sites);
        }

        [HttpGet("sites")]
        public IActionResult GetSites()
        {
            List<Sites> mySites = _context.Sites.ToList();

            if (mySites.Count > 0)
            {
                List<Sites> list = _context.Sites.ToList();
                return View("Index", list);
            }
            else
            {
                return View("Create");
            }
        }

        [HttpGet("sites/{siteId}")]
        public Sites GetSiteById(int siteId)
        {
            return _context.Sites.FirstOrDefault(x => x.Id == siteId);
        }

        [HttpPost("sites/edit")]
        public IActionResult EditSite(SitesDTO newInfos)
        {
            Sites? findSites = _context.Sites.FirstOrDefault(x => x.Id == newInfos.Id);
            
            if (findSites != null)
            {
                findSites.City = newInfos.City;

                _context.Sites.Update(findSites);
                if (_context.SaveChanges() > 0)
                {
                    List<Sites> list = _context.Sites.ToList();
                    return View("Index", list);
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
                    Message = "Aucun site n'a été trouvé avec cet ID !"
                });
            }
        }

        [HttpPost("sites")]
        public IActionResult AddSites(SitesDTO newSites)
        {
            Sites addSite = new Sites()
            {
                City = newSites.City
            };
            _context.Sites.Add(addSite);
            if (_context.SaveChanges() > 0)
            {
                List<Sites> list = _context.Sites.ToList();
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

        public IActionResult DeleteSite(int id)
        {
            Sites? findSite = _context.Sites.FirstOrDefault(x => x.Id == id);

            if (findSite == null)
            {
                return NotFound(new
                {
                    Message = "Aucun site trouvé"
                });
            }
            else
            {
                _context.Sites.Remove(findSite);
                if (_context.SaveChanges() > 0)
                {
                    List<Sites> list = _context.Sites.ToList();
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


using Cube4solo.Datas;
using Cube4solo.models;
using Cube4solo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Cube4solo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SitesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("sites")]
        public IActionResult GetSites()
        {
            List<Sites> mySites = _context.Sites.ToList();

            if (mySites.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici vos Sites !",
                    Sites = mySites
                });
            }
            else
            {
                return NotFound(new
                {
                    Message = "Aucun sites dans la base de donnée"
                });
            }
        }

        [HttpGet("sites/{siteId}")]
        public IActionResult GetSiteById(int siteId)
        {
            Sites? findSites = _context.Sites.FirstOrDefault(x => x.Id == siteId);

            if (findSites == null)
            {
                return NotFound(new
                {
                    Message = "Aucun site trouvé"
                });
            }
            else
            {
                return Ok(new
                {
                    Message = "Site trouvé",
                    Sites = new SitesDTO() { City = findSites.City }
                });
            }
        }

        [HttpPatch("sites")]
        public IActionResult EditSite(SitesDTO newInfos)
        {
            Sites? findSites = _context.Sites.FirstOrDefault(x => x.Id == newInfos.Id);
            
            if (findSites != null)
            {
                findSites.City = newInfos.City;

                _context.Sites.Update(findSites);
                if (_context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "Le sites a bien été modifié !"
                    });
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
                return Ok(new
                {
                    Message = "Le sites a été ajouté",
                    SiteId = addSite.Id
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Une erreur est survenu"
                });
            }
        }

        [HttpDelete("sites/{siteId}")]
        public IActionResult DeleteSite(int siteId)
        {
            Sites? findSite = _context.Sites.FirstOrDefault(x => x.Id == siteId);

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
                    return Ok(new
                    {
                        Message = "Le site a été supprimé"
                    });
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


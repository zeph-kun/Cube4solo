using System.Drawing;
using System.ServiceProcess;
using Cube4solo.Datas;
using Cube4solo.models;
using Cube4solo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Cube4solo.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            this._context = context;
        }
        
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            List<Users> myUsers = _context.Users.ToList();
            
            if (myUsers.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici vos Users:",
                    Article = myUsers
                });

            } else
            {
                return NotFound(new
                {
                    Message = "Aucun Users dans la base de données !"
                });
            }
        }
        
        [HttpGet("users/{userId}")] 
        public IActionResult GetUserById(int userId)
        {
            Users? findUser = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (findUser == null)
            {
                return NotFound(new
                {
                    Message = "Aucun user trouvé avec cet ID !"
                });
            } else
            {
                return Ok(new
                {
                    Message = "User trouvé !",
                    Article = new UsersDTO() { Firstname = findUser.Firstname, Lastname = findUser.Lastname, Email = findUser.Email, Cellphone = findUser.Cellphone, LandlinePhone = findUser.LandlinePhone, Services = findUser.Services, Sites = findUser.Sites, IsAdmin = findUser.IsAdmin}
                });
            }
        }
        
        [HttpPost("users")]
        public IActionResult AddUser(UsersDTO newUser)
        {
            Users addUser = new Users()
            {
                Firstname = newUser.Firstname, Lastname = newUser.Lastname, Email = newUser.Email,
                Cellphone = newUser.Cellphone, LandlinePhone = newUser.LandlinePhone, Services = newUser.Services,
                Sites = newUser.Sites, IsAdmin = newUser.IsAdmin
            };
            Services? findService = _context.Services.FirstOrDefault(x => x.Id == newUser.Services.Id);

            if (findService == null)
            {
                return NotFound(new
                {
                    Message = "Aucun service trouvé avec cet Id"
                });
            }
            else
            {
                addUser.Services = findService;
            }
            Sites? findSites = _context.Sites.FirstOrDefault(x => x.Id == newUser.Sites.Id);

            if (findSites == null)
            {
                return NotFound(new
                {
                    Message = "Aucun site trouvé avec cet Id"
                });
            }
            else
            {
                addUser.Sites = findSites;
            }

            _context.Users.Add(addUser);
            if (_context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Service ajouté"
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Erreur"
                });
            }
        }
        
        [HttpPatch("users")]
        public IActionResult EditUser(UsersDTO newInfos)
        {
            Users? findUser = _context.Users.FirstOrDefault(x => x.Id == newInfos.Id);

            if (findUser != null)
            {
                findUser.Firstname = newInfos.Firstname;
                findUser.Lastname = newInfos.Lastname;
                findUser.Email = newInfos.Email;
                findUser.Cellphone = newInfos.Cellphone;
                findUser.LandlinePhone = newInfos.LandlinePhone;
                findUser.Services = newInfos.Services;
                findUser.Sites = newInfos.Sites;

                Services? findService = _context.Services.FirstOrDefault(x => x.Id == newInfos.Services.Id);

                if (findService == null)
                {
                    return NotFound(new
                    {
                        Message = "Aucun service trouvé avec cet ID !"
                    });
                }
                else
                {
                    findUser.Services = findService;
                }

                Sites? findSite = _context.Sites.FirstOrDefault(x => x.Id == newInfos.Sites.Id);

                if (findSite == null)
                {
                    return NotFound(new
                    {
                        Message = "Aucun site trouvée avec cet ID !"
                    });
                }
                else
                {
                    findUser.Sites = findSite;
                }

                _context.Users.Update(findUser);
                if (_context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "User modifié"
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
                    Message = "Aucun user n'a été trouvé avec cet ID !"
                });
            }
        }
        
        [HttpDelete("users/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            Users? findUser = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (findUser == null)
            {
                return NotFound(new
                {
                    Message = "Aucun User trouvé avec cet ID !"
                });
            }
            else
            {
                _context.Users.Remove(findUser);
                if (_context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "L'utilisateur a bien été supprimé",
                    });
                } else
                {
                    return BadRequest(new
                    {
                        Message = "Une erreur est survenue..."
                    });
                }
            }
        }
    }   
}
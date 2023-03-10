using System.Drawing;
using System.ServiceProcess;
using Cube4solo.Datas;
using Cube4solo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cube4solo.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        // {
        //     using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //     {
        //         passwordSalt = hmac.Key;
        //         passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //     }
        // }
        
         // [HttpGet("Register")]
         // public IActionResult Register()
         // {           
         //     ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
         //     ViewBag.Sites = new SelectList(_context.Sites.ToList(), "Id", "City");
         //     return View();
         // }
        
        //  [HttpPost("Register")]
        //  //[ValidateAntiForgeryToken]
        //  public IActionResult Register(UsersDTO newUser)
        //  {
        //      Users user = new Users
        //      { 
        //          Firstname = newUser.Firstname,
        //          Lastname = newUser.Lastname,
        //          Email = newUser.Email,
        //          Cellphone = newUser.Cellphone,
        //          LandlinePhone = newUser.LandlinePhone,
        //          Services = newUser.Services,
        //          Sites = newUser.Sites,
        //          Password = newUser.Password,
        //          IsAdmin = newUser.IsAdmin
        //      };
        //      Services? findService = _context.Services.FirstOrDefault(x => x.Id == newUser.Services.Id);
        //
        //      if (findService == null)
        //      {
        //          return NotFound(new
        //          {
        //              Message = "Aucun service trouv?? avec cet Id"
        //          });
        //      }
        //      else
        //      {
        //          user.Services = findService;
        //      }
        //      Sites? findSites = _context.Sites.FirstOrDefault(x => x.Id == newUser.Sites.Id);
        //
        //      if (findSites == null)
        //      {
        //          return NotFound(new
        //          {
        //              Message = "Aucun site trouv?? avec cet Id"
        //          });
        //      }
        //      else
        //      {
        //          user.Sites = findSites;
        //      }
        //      _context.Users.Add(user); 
        //      _context.SaveChanges(); 
        //      return RedirectToAction("Index", "Users");
        //  }
        //
        // [HttpGet("Login")]
        //  public IActionResult Login()
        //  {
        //      return View();
        //  }
        //
        //  [HttpPost("Login")]
        //  [ValidateAntiForgeryToken]
        //  public IActionResult Login(Users userDto)
        //  {
        //      if (ModelState.IsValid)
        //      {
        //          Users user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email && u.Password == userDto.Password && u.IsAdmin == userDto.IsAdmin);
        //          if (user != null) 
        //          { 
        //              return RedirectToAction("Index", "Users");
        //          }
        //      }
        //      return View();
        //  }
        
        //[Authorize]
        public IActionResult Index()
        {
            List<Users> list = _context.Users.Include(x => x.Services).Include(x => x.Sites).ToList();
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Sites = new SelectList(_context.Sites.ToList(), "Id", "City");
            return View(list);
        }
        
        public IActionResult Edit(int id)
        {
            Users Users = GetUserById(id);
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Sites = new SelectList(_context.Sites.ToList(), "Id", "City");
            return View(Users);
        }

        [HttpGet("Users/add")]
        public IActionResult Create()
        {
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Sites = new SelectList(_context.Sites.ToList(), "Id", "City");
            return View();
        }
        
        public IActionResult Details(int id)
        {
            Users Users = GetUserById(id);
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Sites = new SelectList(_context.Sites.ToList(), "Id", "City");
            return View(Users);
        }
        
        public IActionResult GetUsers()
        {
            List<Users> myUsers = _context.Users.ToList();
            
            if (myUsers.Count > 0)
            {
                List<Users> list = _context.Users.ToList();
                return View("Index", list);
            } else
            {
                return NotFound(new
                {
                    Message = "Aucun Users dans la base de donn??es !"
                });
            }
        }

        [HttpGet("users/{userId}")]
        public Users GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
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
                    Message = "Aucun service trouv?? avec cet Id"
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
                    Message = "Aucun site trouv?? avec cet Id"
                });
            }
            else
            {
                addUser.Sites = findSites;
            }

            _context.Users.Add(addUser);
            if (_context.SaveChanges() > 0)
            {
                List<Users> list = _context.Users.Include(x => x.Services).Include(x => x.Sites).ToList();
                return View("Index", list);
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Erreur"
                });
            }
        }
        
        [HttpPost("users/edit")]
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
                findUser.IsAdmin = newInfos.IsAdmin;

                Services? findService = _context.Services.FirstOrDefault(x => x.Id == newInfos.Services.Id);

                if (findService == null)
                {
                    return NotFound(new
                    {
                        Message = "Aucun service trouv?? avec cet ID !"
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
                        Message = "Aucun site trouv??e avec cet ID !"
                    });
                }
                else
                {
                    findUser.Sites = findSite;
                }

                _context.Users.Update(findUser);
                if (_context.SaveChanges() > 0)
                {
                    List<Users> list = _context.Users.Include(x => x.Services).Include(x => x.Sites).ToList();
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
                    Message = "Aucun user n'a ??t?? trouv?? avec cet ID !"
                });
            }
        }
        
        public IActionResult DeleteUser(int id)
        {
            Users? findUser = _context.Users.FirstOrDefault(x => x.Id == id);

            if (findUser == null)
            {
                List<Users> list = _context.Users.ToList();
                return View("Index", list);
            }
            else
            {
                _context.Users.Remove(findUser);
                if (_context.SaveChanges() > 0)
                {
                    List<Users> list = _context.Users.Include(x => x.Services).Include(x => x.Sites).ToList();
                    return View("Index", list);
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
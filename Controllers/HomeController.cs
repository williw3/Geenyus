using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using belt2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace belt2.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;
        public HomeController(YourContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null){
                return RedirectToAction("Index");
            }
            else{
                ViewBag.UserId = UserId;
                ViewBag.username = HttpContext.Session.GetString("Username");
                List<Idea> GeniusIdeas = _context.Ideas.Include(i => i.User).Include(l => l.IdeaLikes).OrderByDescending(c => c.IdeaLikes.Count).ToList();
                int GeniusIdeasCount = GeniusIdeas.Count;
                ViewBag.GeniusIdeasCount = GeniusIdeasCount;
                ViewBag.GeniusIdeas = GeniusIdeas;
                
                return View();
            } 
        }

        [HttpPost]
        [Route("addidea")]
        public IActionResult AddIdea(RegisterIdeaModel idea)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(ModelState.IsValid)
            {
                Idea newIdea = new Idea
                {
                Description = idea.Description,
                UserId = (int)UserId
                };
                _context.Ideas.Add(newIdea);
                _context.SaveChanges();
                return RedirectToAction("Home");
            }
            List<Idea> GeniusIdeas = _context.Ideas.Include(i => i.User).Include(l => l.IdeaLikes).OrderByDescending(c => c.IdeaLikes.Count).ToList();
            ViewBag.GeniusIdeas = GeniusIdeas;
            return View("Home");
        }

        [HttpGet]
        [Route("addlike/{ideaid}")]
        public IActionResult AddLike(int ideaid)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            Like newLike = new Like
            {
                IdeaId = ideaid,
                UserId = (int)UserId

            };
            _context.Likes.Add(newLike);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("showuser/{userid}")]
        public IActionResult ShowUser(int userid)
        {
            List<User> ThisUser = _context.Users.Where(u => u.UserId == userid).Include(l => l.UserLikes).Include(i => i.UserIdeas).ToList();
            ViewBag.ThisUser = ThisUser;
            return View("ShowUser");
        }

        [HttpGet]
        [Route("showlikers/{ideaid}")]
        public IActionResult ShowLikers(int ideaid)
        {
            List<Idea> ThisIdea = _context.Ideas.Where(i => i.IdeaId == ideaid).Include(u => u.User).Include(l => l.IdeaLikes).ThenInclude(m => m.User).ToList();
            ViewBag.ThisIdea = ThisIdea;
            return View("ShowLikers");
        }

        [HttpGet]
        [Route("deletepost/{postid}")]
        public IActionResult DeletePost(int postid)
        {
            Idea ThisIdea = _context.Ideas.SingleOrDefault(i => i.IdeaId == postid);
            _context.Ideas.Remove(ThisIdea);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }













        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUserModel user)
        {
            if(ModelState.IsValid)
            {
                var existing = _context.Users.Where(u => u.EmailAddress == user.EmailAddress).SingleOrDefault();
                if (existing == null)
                {
                    PasswordHasher<RegisterUserModel> Hashish = new PasswordHasher<RegisterUserModel>();
                    string hashish = Hashish.HashPassword(user, user.Password);
                    User NewUser = new User
                    {
                        Name = user.Name,
                        Alias = user.Alias,
                        EmailAddress = user.EmailAddress,
                        Password = hashish
                    };
                    _context.Users.Add(NewUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", NewUser.UserId);
                    HttpContext.Session.SetString("Username", NewUser.Name);
                    return RedirectToAction("Home");
                }
                else
                {
                    return View("Index");
                }  
            }
            return View("Index");
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login(string EmailAddress, string Password)
        {
            if (EmailAddress == null || Password == null)
            {
                ViewBag.pwerror = "Must enter an email and password";
                return View("Index");
            }
            //find user mathcing email provided, then check pw
            User existing = _context.Users.Where(u => u.EmailAddress == EmailAddress).SingleOrDefault();
            if (existing != null)
            { 
                var Hashish = new PasswordHasher<User>();
                if (0 != Hashish.VerifyHashedPassword(existing, existing.Password, Password))
                {
                    HttpContext.Session.SetInt32("UserId", existing.UserId);
                    HttpContext.Session.SetString("Username", existing.Name);
                    return RedirectToAction("Home");
                } 
                else
                {   
                    ViewBag.pwerror = "Password Incorrect";
                }    return View("Index");
                
            }
            else
            {
                ViewBag.emailerror = "Email Incorrect";
                return View("Index");
            }     
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
} 

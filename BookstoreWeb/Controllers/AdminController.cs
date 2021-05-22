using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBookRepository repository;

        public AdminController(IBookRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult BooksList()
        {
            return View(repository.Books);
        }
    }
}

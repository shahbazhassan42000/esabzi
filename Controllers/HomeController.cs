﻿using esabzi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace esabzi.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.title = "E-Sabzi";
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Messaia.Net.Repository;
using Messaia.Net.Identity;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes an instance of the <see cref="TestController"/> class.
        /// </summary>
        public TestController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            var users = this.unitOfWork.FromSql<User>($@"SELECT * FROM ""Users""").ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
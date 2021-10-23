using AutoMapper;
using ClientMVC.Dtos;
using ClientMVC.Models;
using ClientMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var model = await _usersService.GetAllUsersAsync().ConfigureAwait(false);
            return View(_mapper.Map<IEnumerable<UserViewModel>>(model));
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _usersService.GetUserByIdAsync(id).ConfigureAwait(false);
            if (model == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<UserViewModel>(model));
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _usersService.GetUserByIdAsync(id).ConfigureAwait(false);
            if (model == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<UserViewModel>(model));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Surname,Age,Sex,IsActive,Phones")]UserViewModel userViewModel)
        {
            try
            {
                var result = await _usersService.UpdateUserAsync(_mapper.Map<User>(userViewModel));
                //to do
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _usersService.GetUserByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<UserViewModel>(model));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            bool isSuccess = await _usersService.DeleteUserAsync(id).ConfigureAwait(false);
            
            if (isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}

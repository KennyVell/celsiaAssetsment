using Microsoft.AspNetCore.Mvc;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Controllers.Clients
{
    public class ClientsController : Controller
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientsController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var clients = await _clientsRepository.GetAll();
                if (clients.Any()) return View(clients);
                return View(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var client = await _clientsRepository.GetById(id);
                if (client != null) return View(client);
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientDTO client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _clientsRepository.Add(client);
                    return RedirectToAction("Index", "Clients");
                }
                return View(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = await _clientsRepository.GetById(id);
                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _clientsRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar el error, por ejemplo, mostrando un mensaje
                ViewData["Message"] = ex.Message;
                return View("Error"); // O una vista personalizada de error
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = await _clientsRepository.GetById(id);
                if (client != null) return View(client);
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClientDTO client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _clientsRepository.Update(id, client);
                    return RedirectToAction("Index", "Clients");
                }
                return View(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View(client);
        }
    }
}
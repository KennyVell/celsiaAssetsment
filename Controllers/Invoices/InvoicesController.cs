using Microsoft.AspNetCore.Mvc;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Controllers.Invoices
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesController(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var invoices = await _invoicesRepository.GetAll();
                if (invoices.Any()) return View(invoices);
                return View(invoices);
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
                var invoice = await _invoicesRepository.GetById(id);
                if (invoice != null) return View(invoice);
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
        public async Task<IActionResult> Create(InvoiceDTO invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _invoicesRepository.Add(invoice);
                    return RedirectToAction("Index", "Invoices");
                }
                return View(invoice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var invoice = await _invoicesRepository.GetById(id);
                if (invoice == null)
                {
                    return NotFound();
                }
                return View(invoice);
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
                await _invoicesRepository.Delete(id);
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
                var invoice = await _invoicesRepository.GetById(id);
                if (invoice != null) return View(invoice);
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
        public async Task<IActionResult> Edit(int id, InvoiceDTO invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _invoicesRepository.Update(id, invoice);
                    return RedirectToAction("Index", "Invoices");
                }
                return View(invoice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View(invoice);
        }
    }
}
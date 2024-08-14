using Microsoft.AspNetCore.Mvc;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Controllers.Transactions
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsController(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var transactions = await _transactionsRepository.GetAll();
                if (transactions.Any()) return View(transactions);
                return View(transactions);
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
                var transaction = await _transactionsRepository.GetById(id);
                if (transaction != null) return View(transaction);
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
        public async Task<IActionResult> Create(TransactionDTO transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _transactionsRepository.Add(transaction);
                    return RedirectToAction("Index", "Transactions");
                }
                return View(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var transaction = await _transactionsRepository.GetById(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return View(transaction);
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
                await _transactionsRepository.Delete(id);
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
                var transaction = await _transactionsRepository.GetById(id);
                if (transaction != null) return View(transaction);
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
        public async Task<IActionResult> Edit(int id, TransactionDTO transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _transactionsRepository.Update(id, transaction);
                    return RedirectToAction("Index", "Transactions");
                }
                return View(transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Message"] = ex.Message;
            }
            return View(transaction);
        }
    }
}
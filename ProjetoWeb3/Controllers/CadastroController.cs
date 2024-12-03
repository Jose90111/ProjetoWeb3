using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoImg.Context;
using ProjetoImg.Models;


namespace TrabalhoFotoIgor.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppCont _appCont;

        public CadastroController(AppCont appCont)
        {
            _appCont = appCont;
        }
        public IActionResult Index()
        {
            var allTasks = _appCont.Clientes.ToList();
            return View(allTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _appCont.Clientes
                .FirstOrDefaultAsync(x => x.ID == id);

            if (Cliente == null)
            {
                return BadRequest();
            }

            return View(Cliente);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cad_cliente cliente, IFormFile foto)
        {


            if (foto != null && foto.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                foto.CopyTo(memoryStream);
                cliente.Foto = memoryStream.ToArray(); // Converte imagem para byte[]
            }

            _appCont.Clientes.Add(cliente); // Adiciona o cliente ao banco
            _appCont.SaveChanges(); // Salva as alterações no banco
            return RedirectToAction(nameof(Index)); // Redireciona para a listagem


            return View(cliente); // Retorna a view com erros de validação
        }



        // GET: Cliente/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || !_appCont.Clientes.Any(c => c.ID == id))
            {
                return NotFound();
            }

            var cliente = _appCont.Clientes.Find(id);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cad_cliente cliente, IFormFile foto)
        {
            if (id != cliente.ID)
            {
                return NotFound();
            }


            try
            {
                if (foto != null && foto.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    foto.CopyTo(memoryStream);
                    cliente.Foto = memoryStream.ToArray();
                }
                else
                {
                    // Mantém a foto existente se nenhuma nova for enviada
                    cliente.Foto = _appCont.Clientes.AsNoTracking()
                        .FirstOrDefault(c => c.ID == cliente.ID)?.Foto;
                }

                _appCont.Update(cliente);
                _appCont.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appCont.Clientes.Any(e => e.ID == cliente.ID))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));

            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _appCont.Clientes
                .FirstOrDefault(m => m.ID == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cliente = _appCont.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _appCont.Clientes.Remove(cliente);
            _appCont.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

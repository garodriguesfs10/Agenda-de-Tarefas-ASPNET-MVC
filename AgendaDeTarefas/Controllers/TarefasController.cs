using AgendaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTarefas.Controllers
{
    public class TarefasController : Controller
    {

        private readonly Contexto _contexto;

        public TarefasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var data = PegarDatas();
            return View(data);
        }

        private List<DatasViewModel> PegarDatas()
        {
            DateTime dataAtual = DateTime.Now;
            DateTime dataLimite = DateTime.Now.AddDays(10);
        
            DatasViewModel data;
            List<DatasViewModel> listaDatas = new List<DatasViewModel>();

            while(dataAtual < dataLimite)
            {
                data = new DatasViewModel();
                data.Datas = dataAtual.ToShortDateString();
                data.Identificadores = "collapse" + dataAtual.ToShortDateString().Replace("/", "");
                listaDatas.Add(data);               
                dataAtual = dataAtual.AddDays(1);
            }

            return listaDatas;
        }
           
        [HttpGet]

        public IActionResult CriarTarefa(string dataTarefa)
        {
            Tarefa tarefa = new Tarefa
            {
                Data = dataTarefa
            };

            return View(tarefa);
        }

        [HttpPost]

        public async Task<ActionResult> CriarTarefa (Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Tarefas.Add(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpGet]
        public async Task<ActionResult> AtualizarTarefa(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);
            if(tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost]

        public async Task<ActionResult> AtualizarTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(tarefa);
        }

        [HttpPost]
        public async Task<JsonResult> DeletarTarefa(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);
            _contexto.Tarefas.Remove(tarefa);
            await _contexto.SaveChangesAsync();
            return Json(true);


        }

    }
}

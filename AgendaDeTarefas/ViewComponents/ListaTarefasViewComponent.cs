using AgendaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaDeTarefas.ViewComponents
{
    public class ListaTarefasViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public ListaTarefasViewComponent(Contexto contexto) {
            _contexto = contexto; 
        }

        public async Task<IViewComponentResult> InvokeAsync(string data)
        {
            return View(await _contexto.Tarefas
                .OrderBy(t => t.Horario).Where(t => t.Data == data).ToListAsync());
        }
    }
}

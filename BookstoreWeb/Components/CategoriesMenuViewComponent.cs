using BookstoreWeb.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookstoreWeb.Components
{
    // Класс навигационного компонента представления
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository repository;

        public CategoriesMenuViewComponent(ICategoryRepository repository) => this.repository = repository;

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(repository.Categories.OrderBy(x => x.Name));
        }
    }
}

using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using BookstoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace BookstoreWeb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IOrderRepository orderRepository;

        public AdminController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IOrderRepository orderRepository) =>
            (this.bookRepository, this.categoryRepository, this.orderRepository) = (bookRepository, categoryRepository, orderRepository);

        // Отображение основной страницы админа, с данными о сервере
        public ViewResult Index() => View();

        // Отображение страницы со списком книг
        public ViewResult BooksList()
        {
            ViewBag.Count = bookRepository.Books.Count();
            return View(bookRepository.Books);
        }

        // Отображение страницы с выбранной книгой, для редактирования или создания новой
        public ViewResult EditBook(int Id) => View(new CategoriesAndBooksViewModel
        {
            Categories = categoryRepository.Categories,
            Book = bookRepository.Books.FirstOrDefault(b => b.Id == Id)
        });

        // Сохранение отредактированных данных по книге в БД
        [HttpPost]
        public IActionResult EditBook(CategoriesAndBooksViewModel bookvm)
        {
            // Если загрузили новую картинку или оставили старую картинку при редактировании
            if (bookvm.Image != null || bookvm.Book.Image != null)
            {
                if (bookvm.Image != null)
                    bookvm.Book.Image = ConvertImage(bookvm.Image);
                bookRepository.SaveBook(bookvm.Book);
                TempData["message"] = $"Книга \"{bookvm.Book.Name}\" с ID:{bookvm.Book.Id} успешно сохранена";
                return RedirectToAction(nameof(BooksList));
            }
            else // Что то не так
                return View(bookvm); 
        }

        // Считываем переданную картинку в массив байтов
        private byte[] ConvertImage(IFormFile formFile)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                imageData = binaryReader.ReadBytes((int)formFile.Length);
            return imageData;
        }

        // Создание новой книги
        public ViewResult CreateBook() => View(nameof(EditBook),
            new CategoriesAndBooksViewModel 
            { 
                Book = new Book(),
                Categories = categoryRepository.Categories 
            });

        // Удаление книги
        public IActionResult DeleteBook(int Id)
        {
            Book deletedBook = bookRepository.DeleteBook(Id);
            if(deletedBook != null)
                TempData["message"] = $"Книга \"{deletedBook.Name}\" с ID:{deletedBook.Id} была удалена";
            return RedirectToAction(nameof(BooksList));
        }

        // Отображение страницы с полным описанием данных книги
        public ViewResult BookDescription(int Id) => View(bookRepository.Books.FirstOrDefault(b => b.Id == Id));

        // Отображение страницы со всеми заказами
        public ViewResult OrdersList() => View(orderRepository.Orders);

        // Маркируем заказ как отправленный
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            MarkOrder(orderId, true);
            return RedirectToAction(nameof(OrdersList));
        }

        // Маркируем заказ как неотправленный
        [HttpPost]
        public IActionResult MarkUnshipped(int orderId)
        {
            MarkOrder(orderId, false);
            return RedirectToAction(nameof(OrdersList));
        }

        // Метод маркировки заказа
        private void MarkOrder(int orderId, bool mark)
        {
            Order order = orderRepository.Orders
               .FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                order.Shipped = mark;
                orderRepository.SaveOrder(order);
            }
        }

        // Отображение страницы со списком категорий
        public ViewResult CategoriesList() => View(categoryRepository.Categories);

        // Отображение страницы с выбранной категорией, для редактирования или создания новой
        public ViewResult EditCategory(int Id) => View(categoryRepository.Categories
                .FirstOrDefault(c => c.Id == Id));

        // Сохранение отредактированных данных по категории в БД
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            categoryRepository.SaveCategory(category);
            TempData["message"] = $"Категория \"{category.Name}\" с ID:{category.Id} успешно сохранена";
            return RedirectToAction(nameof(CategoriesList));
        }

        // Создание новой категории
        public ViewResult CreateCategory() => View(nameof(EditCategory), new Category());

        // Удаление книги
        public IActionResult DeleteCategory(int Id)
        {
            Category deletedCategory = categoryRepository.DeleteCategory(Id);
            if (deletedCategory != null)
                TempData["message"] = $"Категория \"{deletedCategory.Name}\" с ID:{deletedCategory.Id} была удалена";
            return RedirectToAction(nameof(CategoriesList));
        }

        /* Управление БД */

        // Заполнение БД искусственными данными
        //public IActionResult SeedData()
        //{    
        //    BookstoreInitializer initializer = new BookstoreInitializer();
        //    initializer.Initialize(app);
        //}
    }
}

using BookstoreWeb.Models.Entities;
using BookstoreWeb.Models.Interfaces;
using BookstoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace BookstoreWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;

        public AdminController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
        }

        // Отображение основной страницы админа, с данными о сервере
        public ViewResult Index()
        {
            return View();
        }

        // Отображение страницы со списком книг
        public ViewResult BooksList()
        {
            return View(bookRepository.Books);
        }

        // Отображение страницы с выбранной книгой, для редактирования или создания новой
        public ViewResult Edit(int Id)
        {
            return View(new CategoriesAndBooksViewModel
            {
                Categories = categoryRepository.Categories,
                Book = bookRepository.Books.FirstOrDefault(b => b.Id == Id)
            });
        }

        // Сохранение измененных данных по книге в БД
        [HttpPost]
        public IActionResult Edit(CategoriesAndBooksViewModel bookvm)
        {
            // Если загрузили новую картинку или оставили старую картинку при редактировании
            if (bookvm.Image != null || bookvm.Book.Image != null)
            {
                if (bookvm.Image != null)
                    bookvm.Book.Image = ConvertImage(bookvm.Image);
                bookRepository.SaveBook(bookvm.Book);
                TempData["message"] = $"Книга \"{bookvm.Book.Name}\" с ID:{bookvm.Book.Id} успешно сохранена";
                return RedirectToAction("BooksList");
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

        public ViewResult Create() => View("Edit", 
            new CategoriesAndBooksViewModel 
            { 
                Book = new Book(),
                Categories = categoryRepository.Categories 
            });

        public IActionResult Delete(int Id)
        {
            Book deletedBook = bookRepository.DeleteBook(Id);
            if(deletedBook != null)
                TempData["message"] = $"Книга \"{deletedBook.Name}\" с ID:{deletedBook.Id} была удалена";
            return RedirectToAction("BooksList");
        }
    }
}

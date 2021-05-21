using Microsoft.AspNetCore.Http;

namespace BookstoreWeb.Infrastructure.Extentions
{
    public static class UrlExtensions
    {
        // Расширяющий метод генерирует URL, по которому браузер будет возвращаться после обновления корзины
        public static string PathAndQuery(this HttpRequest request) => request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
    }
}

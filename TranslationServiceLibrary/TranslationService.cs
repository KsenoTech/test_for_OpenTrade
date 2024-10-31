using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TranslationServiceCore;

namespace TranslationServiceLibrary
{
    public class TranslationService : ITranslationService
    {
        private readonly TranslationCache _cache;
        private readonly HttpClient _httpClient;

        public TranslationService(TranslationCache cache)
        {
            _cache = cache;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Переводит список текстов с одного языка на другой.
        /// </summary>
        public async Task<List<string>> TranslateAsync(List<string> texts, string fromLang, string toLang)
        {
            var translations = new List<string>();
            foreach (var text in texts)
            {
                // Проверка кэша перед запросом к API
                var cachedTranslation = _cache.GetCachedTranslation(text, fromLang, toLang);
                if (cachedTranslation != null)
                {
                    translations.Add(cachedTranslation);
                    continue;
                }

                // Пример вызова внешнего API для перевода
                var translation = await TranslateTextAsync(text, fromLang, toLang);

                // Сохранение результата в кэш
                _cache.AddTranslationToCache(text, fromLang, toLang, translation);
                translations.Add(translation);
            }
            return translations;
        }

        /// <summary>
        /// Возвращает информацию о сервисе и типе кэша.
        /// </summary>
        public Task<string> GetServiceInfoAsync()
        {
            return Task.FromResult("Translation Service using Google Translate API, with in-memory caching.");
        }

        /// <summary>
        /// Вспомогательный метод для вызова внешнего API перевода (упрощённый пример).
        /// </summary>
        private async Task<string> TranslateTextAsync(string text, string fromLang, string toLang)
        {
            // Примерный URL и параметры для вызова API (замените реальным)
            string url = $"https://api.example.com/translate?text={Uri.EscapeDataString(text)}&from={fromLang}&to={toLang}";

            // Отправка запроса
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Парсинг ответа (зависит от формата, возвращаемого API)
            var json = await response.Content.ReadAsStringAsync();
            var translationResult = JsonSerializer.Deserialize<TranslationApiResponse>(json);

            return translationResult?.TranslatedText ?? "Ошибка перевода";
        }
    }

    /// <summary>
    /// Модель ответа API перевода (настройте под конкретный API).
    /// </summary>
    public class TranslationApiResponse
    {
        public string TranslatedText { get; set; }
    }
}

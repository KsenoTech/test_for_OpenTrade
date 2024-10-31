using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationServiceCore
{
    public class TranslationCache
    {
        // Кэш словарь: ключ - комбинация текста и языков, значение - переведённый текст
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        /// <summary>
        /// Проверяет, есть ли в кэше перевод для заданного текста и языков.
        /// </summary>
        /// <param name="text">Текст для перевода.</param>
        /// <param name="fromLang">Исходный язык.</param>
        /// <param name="toLang">Целевой язык.</param>
        /// <returns>Переведённый текст, если он уже в кэше; иначе - null.</returns>
        public string GetCachedTranslation(string text, string fromLang, string toLang)
        {
            string key = GenerateKey(text, fromLang, toLang);
            return _cache.TryGetValue(key, out string translation) ? translation : null;
        }

        /// <summary>
        /// Добавляет перевод в кэш.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <param name="fromLang">Исходный язык.</param>
        /// <param name="toLang">Целевой язык.</param>
        /// <param name="translation">Переведённый текст.</param>
        public void AddTranslationToCache(string text, string fromLang, string toLang, string translation)
        {
            string key = GenerateKey(text, fromLang, toLang);
            if (!_cache.ContainsKey(key))
            {
                _cache[key] = translation;
            }
        }

        /// <summary>
        /// Генерирует ключ для кэша, комбинируя текст и языки.
        /// </summary>
        /// <param name="text">Текст для перевода.</param>
        /// <param name="fromLang">Исходный язык.</param>
        /// <param name="toLang">Целевой язык.</param>
        /// <returns>Ключ для словаря кэша.</returns>
        private string GenerateKey(string text, string fromLang, string toLang)
        {
            return $"{fromLang}-{toLang}:{text}";
        }
    }
}

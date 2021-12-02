namespace XSDValidationTest2.Models
{
    /// <summary>
    /// Модель настроек загружаемого файла
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// Лимит проверяемого файла в байтах
        /// </summary>
        public readonly int? limite = 3145728;

        /// <summary>
        /// Расширение xml
        /// </summary>
        public readonly string xmlExtension = "xml";

        /// <summary>
        /// Имя с которым файл сохранился на сервер
        /// </summary>
        public string FileSavedName { get; set; }

        /// <summary>
        /// Флаг, показывающий успешно ли загрузился файл на сервер
        /// </summary>
        public bool IsUploadSuccessed { get; set; }
    }
}
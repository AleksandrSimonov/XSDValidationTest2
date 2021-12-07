using System;

namespace XSDValidationTest2.Validator
{
    /// <summary>
    /// Результат валидации
    /// </summary>
    [Serializable]
    public class ValidationResult
    {
        /// <summary>
        /// Заголовок успешной валидации
        /// </summary>
        public const string validSuccessMessage = "Xml схема валидна";

        /// <summary>
        /// Заголовок неуспешной валидации
        /// </summary>
        public const string validErrorMessage = "Xml схема не валидна!";

        /// <summary>
        /// текст результата валидации
        /// </summary>
        private string _message;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ValidationResult()
        {
            _message = string.Empty;
        }

        /// <summary>
        /// флаг, указывающий валидна ли схема
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// сообщение результата валидации
        /// </summary>
        public string Message
        {
            get
            {
                return IsValid ? validSuccessMessage : $"{validErrorMessage}\n{_message}";
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// Полное имя проверяемого файла
        /// </summary>
        public string XmlFileFullName { get; set; }

        /// <summary>
        /// Полное имя xsd схемы
        /// </summary>
        public string XsdFileFullName { get; set; }
    }
}
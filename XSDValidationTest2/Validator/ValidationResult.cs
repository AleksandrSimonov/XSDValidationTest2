namespace XSDValidationTest2.Validator
{
    /// <summary>
    /// Результат валидации
    /// </summary>
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
        /// <param name="isValid">флаг, указывающий валидна ли схема</param>
        /// <param name="message">сообщение результата валидации</param>
        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            _message = message;
        }

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
    }
}
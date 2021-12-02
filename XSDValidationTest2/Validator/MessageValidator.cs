using System;
using System.Configuration;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace XSDValidationTest2.Validator
{
    /// <summary>
    /// Валидация отправляемых и принимаемых сообщений по xsd схеме
    /// </summary>
    public class MessageValidator
    {
        private readonly XmlSchemaSet _schemas;
        private readonly StringBuilder _errors;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MessageValidator()
        {
            _schemas = GetXmlSchemaSetForValidation();
            _errors = new StringBuilder();
        }

        /// <summary>
        /// Проверить xml файл по xsd схеме
        /// </summary>
        /// <param name="fileFullName">полный путь к проверяемому файлу</param>
        /// <returns>результат проверки</returns>
        public ValidationResult ValidateFile(string fileFullName)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(_schemas);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += ValidationBySchemaHandler;
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            using (XmlReader r = XmlReader.Create(fileFullName, settings))
            {
                try
                {
                    while (r.Read()) { }
                }
                catch (Exception ex)
                {
                    _errors.AppendLine($"Ошибка валидации по xsd схеме {ex.Message}");
                }
            }

            return new ValidationResult()
            {
                IsValid = _errors.Length == 0,
                Message = _errors.ToString()
            };
        }

        /// <summary>
        /// Получает схему для валидации
        /// </summary>
        /// <returns>xml схема валидации</returns>
        private XmlSchemaSet GetXmlSchemaSetForValidation()
        {
            string xsdName = ConfigurationManager.AppSettings["XsdSchemaForValidation"];

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            Uri baseSchema = new Uri(AppDomain.CurrentDomain.BaseDirectory);
            string mySchema = new Uri(baseSchema, xsdName).ToString();
            XmlSchema schema = XmlSchema.Read(new XmlTextReader(mySchema), null);
            schemaSet.Add(schema);

            return schemaSet;
        }

        /// <summary>
        /// Обработчик события при валидации 
        /// </summary>
        /// <param name="sender">источник</param>
        /// <param name="e">событие</param>
        private void ValidationBySchemaHandler(object sender, ValidationEventArgs e)
        {
            _errors.AppendLine(e.Message);
        }
    }
}

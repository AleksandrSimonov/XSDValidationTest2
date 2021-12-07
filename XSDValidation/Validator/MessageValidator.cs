using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XSDValidationTest2.Validator
{
    /// <summary>
    /// Валидация отправляемых и принимаемых сообщений по xsd схеме
    /// </summary>
    public class MessageValidator
    {
        private readonly XmlSchemaSet _schemas;
        private readonly StringBuilder _errors;
        private readonly string _xsdFullName;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MessageValidator()
        {
            _xsdFullName = ConfigurationManager.AppSettings["XsdSchemaForValidation"];
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

            ValidationResult result = new ValidationResult()
            {
                XmlFileFullName = fileFullName,
                XsdFileFullName = _xsdFullName,
                IsValid = _errors.Length == 0,
                Message = _errors.ToString()
            };

            string fileName = Path.GetFileName(fileFullName);

            string serializedFileFullName = Serialize(result, fileName);
            ValidationResult deserializeResult = Deserialize(serializedFileFullName);

            XDocument xdoc = XDocument.Load(serializedFileFullName);
            bool isValid = Convert.ToBoolean(xdoc.Element("ValidationResult").Element("IsValid").Value);

            return deserializeResult;
        }

        private string Serialize(ValidationResult result, string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ValidationResult));

            Uri basePath = new Uri(AppDomain.CurrentDomain.BaseDirectory);
            string serializedFileFullName = Path.Combine(basePath.AbsolutePath, ConfigurationManager.AppSettings["SerializedXmlPath"], fileName);

            using (FileStream fs = new FileStream(serializedFileFullName, FileMode.OpenOrCreate))
            {

                formatter.Serialize(fs, result);
            }
            return serializedFileFullName;
        }

        private ValidationResult Deserialize(string serializedFileFullName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ValidationResult));
            ValidationResult result = null;

            using (FileStream fs = new FileStream(serializedFileFullName, FileMode.Open))
            {
                result = (ValidationResult)formatter.Deserialize(fs);
            }
            return result;
        }

        /// <summary>
        /// Получает схему для валидации
        /// </summary>
        /// <returns>xml схема валидации</returns>
        private XmlSchemaSet GetXmlSchemaSetForValidation()
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            Uri baseSchema = new Uri(AppDomain.CurrentDomain.BaseDirectory);
            string mySchema = new Uri(baseSchema, ConfigurationManager.AppSettings["XsdSchemaForValidation"]).ToString();
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

using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Serialization;
using AutoMapper;
using CsvHelper;
using FileConverter.Service.Models;
using Newtonsoft.Json;

namespace FileConverter.Service
{
    public class CsvService : ICsvService
    {
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(m =>
            m.AddProfile(new PersonProfile())));

        public string ParseFile(ApplicationArguments applicationArguments)
        {

            GetChildObjects(applicationArguments);




            var people = GetInputFile(applicationArguments.Filename);
            var file = applicationArguments.Filename.Split('.');
            var fileType = file[1];

            if (fileType == string.Empty) return string.Empty;

            switch (fileType.ToLower())
            {
                case "csv":
                    switch (applicationArguments.To.ToLower())
                    {
                        case "json":
                            return JsonConvert.SerializeObject(people, Formatting.Indented);
                        case "xml":
                            using (var sw = new StringWriter())
                            {
                                var xs = new XmlSerializer(typeof(List<Person>));

                                xs.Serialize(sw, people);

                                return sw.ToString();
                            }
                    }
                    break;
                case "json":
                    switch (applicationArguments.To.ToLower())
                    {
                        case "csv":
                            //TODO: Convert JSON to CSV 
                            break;
                        case "xml":
                            //TODO: Convert JSON to XML
                            break;
                    }
                    break;
                case "xml":
                    switch (applicationArguments.To.ToLower())
                    {
                        case "csv":
                            //TODO: Convert XML to CSV
                            break;
                        case "json":
                            //TODO: Convert XML to JSON
                            break;
                    }
                    break;
            }
            return string.Empty;
        }

        private List<Person> GetInputFile(string filename)
        {
            var pathToFile = $"../../../{filename}";
            var records = new List<PersonAddress>();

            if (!File.Exists(pathToFile))
            {
                return new List<Person>();
            }

            using (var reader = new StreamReader(pathToFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<PersonAddress>().ToList();
            }

            return _mapper.Map<List<PersonAddress>, List<Person>>(records);
        }

        //TODO: CSV underscored Columns to be parsed as a separate routine.
        //      A Method to explicitly look for Column names that have an underscore within the Column name (not at either end)
        //      This might be dynamic classes where we don't know the contents of the CSV file and need to build out classes for
        //      non-underscored columns and individual classes with a unique prefix?
        //
        public void GetChildObjects(ApplicationArguments applicationArguments)
        {
            var pathToFile = $"../../../{applicationArguments.Filename}";
            var records = new List<dynamic>();

            if (!File.Exists(pathToFile))
            {
                //return
            }

            using (var reader = new StreamReader(pathToFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<dynamic>().ToList();
            }

            //TODO: Use Expando Object to create dynamic classes
            //

            //TODO: Parse Contents looking for Underscored items
            //

            //TODO: Write out Separate PArsed Underscored objects.
            //
        }

        public static ExpandoObject BuildDynamicObject(string className, Dictionary<string, object> fields)
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.ClassName = className;
            //AssignProperties(expandoObject, fields);

            return expandoObject;
        }
    }
}





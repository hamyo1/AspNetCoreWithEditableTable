using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RazorPageTableProssesor.Interfaces;
using RazorPageTableProssesor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace RazorPageTableProssesor.Services
{
    public class CreateClipsService:ICreateClipsService
    {
        readonly ILogger<CreateClipsService> _logger;

        public CreateClipsService( ILogger<CreateClipsService> logger, IConfiguration config)
        {
            _logger = logger;
        }

        public void CreateOrUpdateCsv(List<DataModel> dataModel)
        {
            _logger.LogInformation("CreateOrUpdateCsv run");
            using (var stream = new FileStream($"..\\D.csv", FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(dataModel);
                }
            }

        }        
        
        public List<DataModel> GetCsvData()
        {
            List<DataModel> dataModelList = new List<DataModel>();

            _logger.LogInformation("GetCsvData run");
            using (var reader = new StreamReader($"..\\D.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var res = csv.GetRecords<DataModel>();

                foreach (var item in res)
                {
                    dataModelList.Add(item);
                }
            }
            return dataModelList;
        }
    }
}

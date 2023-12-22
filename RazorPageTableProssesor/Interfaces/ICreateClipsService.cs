using RazorPageTableProssesor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageTableProssesor.Interfaces
{
    public interface ICreateClipsService
    {
        void CreateOrUpdateCsv(List<DataModel> dataModel);
        List<DataModel> GetCsvData();

    }
}

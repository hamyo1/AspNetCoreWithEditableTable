using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageTableProssesor.Interfaces;
using RazorPageTableProssesor.Models;
using RazorPageTableProssesor.Services;

namespace AspNetCoreWithReactApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        readonly ICreateClipsService _createClipsService;

        public IndexModel(ILogger<IndexModel> logger, ICreateClipsService createClipsService)
        {
            _logger = logger;
            newLayer = new NewLayer();
            _createClipsService = createClipsService;
        }


        [BindProperty(SupportsGet = true)]
        public NewLayer newLayer { get; set; }        
                
        [BindProperty(SupportsGet = true)]
        public List<DataModel> dataModelList { get; set; }        
        
        public void OnGet()
        {
            dataModelList = _createClipsService.GetCsvData();




            //for (var i = 0; i < 3; i++)
            //{
            //    DataModel dataModel = new DataModel();

            //    dataModel.customerId = $"{i}{i}{i}{i}";
            //    dataModel.customerEmail = $"{i + 1}{i + 1}{i + 1}{i + 1}";
            //    dataModel.customerPhone = $"{i}{i}@{i}{i}";
            //    dataModel.employerPhone = $"{i}-{i}{i}{i}11";
            //    dataModel.employerName = $"{i}{i}{i}{i}dd";
            //    dataModel.employerId = $"{i}{i}{i}{i}22";
            //    dataModelList.Add(dataModel);
            //}
            //_createClipsService.CreateOrUpdateCsv(dataModelList);

        }

        public void OnPost()
        {
            _createClipsService.CreateOrUpdateCsv(this.dataModelList);
        }
    }
}

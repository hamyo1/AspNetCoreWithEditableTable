namespace RazorPageTableProssesor.Models
{
    public class DataModel
    {
        public string customerPhone {  get; set; }
        public string customerEmail { get; set; }
        public string customerId { get; set; }
        public string employerName { get; set; }
        public string employerId { get; set; }
        public string employerPhone { get; set; }
    }

    public class NewLayer
    {
        public List<string> ColumnNames { get; set; } = new List<string>();
        public List<string> ColumnDataTypes { get; set; } = new List<string>();
        public List<string> LayerNames { get; set; } = new List<string>();
        public List<string> ImportColumn { get; set; } = new List<string>();
        public string SelectedLayer { get; set; } = null!;
        public string SelectedNameField { get; set; } = null!;
    }

    public class ListOfDataModel
    {
        public string index { get; set; }
        public List<DataModel> dataModelList { get; set;}
    }

}

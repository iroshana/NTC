using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class ComplainViewModel
    {
        public int id { get; set; }
        public string complainNo { get; set; }
        public BusViewModel bus { get; set; }
        public RouteViewModel route { get; set; }
        public string place { get; set; }
        public string time { get; set; }
        public string method { get; set; }
        public string complainCode { get; set; }
        public string description { get; set; }
        public int userId { get; set; }
        public int? evidenceId { get; set; }
        public bool isEvidenceHave { get; set; }
        public bool isInqueryParticipation { get; set; }
        public List<CategoryViewModel> Category { get; set; }
        public EvidenceViewModel evidence { get; set; }
        //public List<ComplainCategoryViewModel> complainCategory { get; set; }

    }
    public class ComplainCategoryViewModel
    {
        public int complainId { get; set; }
        public int categoryId { get; set; }
        public string description { get; set; }
    }

    public class CategoryViewModel
    {
        public int id { get; set; }
        public string categoryNo { get; set; }
        public string description { get; set; }
        public bool isSelected { get; set; }
    }
    public class ComplainDropDownViewModel
    {
        public int id { get; set; }
        public string complainNo { get; set; }
    }
    public class BusViewModel
    {
        public int id { get; set; }
        public string busNo { get; set; }
        public RouteViewModel route { get; set; }
    }
    public class RouteViewModel
    {
        public int id { get; set; }
        public string routeNo { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }
    public class EvidenceViewModel
    {
        public int id { get; set; }
        public string evidenceNo { get; set; }
        public string fileName { get; set; }
        public string extension { get; set; }
        public string filePath { get; set; }
    }
}

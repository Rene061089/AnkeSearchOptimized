namespace WebApplication1.BusinessLogic
    {
    //public class postClass
    //{
    //}

    public class PostSearch
        {
        public string words { get; set; }
        public string? orderby { get; set; }
        public string? searchby { get; set; }
        public bool? principal { get; set; }
        public bool? complaintsUpheld { get; set; }
        public bool? companyComplaintsUpheld { get; set; }
        public bool? complaintsPartlyUpheld { get; set; }
        public bool? paragraph4 { get; set; }
        public int pages { get; set; }
        public string[] company { get; set; }
        public string[] insuranceType { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }


    }

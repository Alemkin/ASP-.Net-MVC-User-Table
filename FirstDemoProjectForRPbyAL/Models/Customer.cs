namespace FirstDemoProjectForRPbyAL.Models
{
    using System;
    
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public bool Inactive { get; set; }
        public System.DateTimeOffset AddedOn { get; set; }
        public Nullable<System.DateTimeOffset> ChangedOn { get; set; }
        public string FirmName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> PartnerID { get; set; }
    }
}

namespace FirstDemoProjectForRPbyAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Partner
    {
        [DisplayFormat(NullDisplayText = "N/A")]
        public int PartnerID { get; set; }

        public System.Guid PartnerGUID { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public System.DateTime CreatedOn { get; set; }

        public Nullable<System.DateTime> ChangedOn { get; set; }
    }
}

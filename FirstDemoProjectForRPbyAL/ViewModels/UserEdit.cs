using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstDemoProjectForRPbyAL.Models;

namespace FirstDemoProjectForRPbyAL.ViewModels
{
    public class UserEdit
    {
        public User User { get; set; }
        public int PageNumber { get; set; }
        public string SortOrder { get; set; }
    }
}
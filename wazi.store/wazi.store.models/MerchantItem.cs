using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wazi.data.models;

namespace wazi.store.models {
    public class MerchantItem : DataObject {
        public MerchantItem() {

        }

        public string MerchantCode { get; set; }
        public string MerchantEmail { get; set; }
        public string MerchantMobile { get; set; }
        public string RegionCode { get; set; }
        public EmployeeCount EmployeeCount { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public enum EmployeeCount {
        OneToFive = 0,
        FiveToFifty = 1,
        FiftyAndMore = 2
    }
}

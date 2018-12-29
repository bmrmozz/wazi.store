using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wazi.data.core;
using wazi.store.models;

namespace wazi.store.common {
    public class MerchantCollection : ObjectRepositoryBase<MerchantItem> {
        public MerchantCollection(IRepository repository) : base(repository) {
            this.Name = "wazimerchantlist";
            this.Prefix = "dat";
        }

        public override void SaveItem(MerchantItem item) {
            item.SetDefaults();
            base.SaveItem(item);
        }
    }
}

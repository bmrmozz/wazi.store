using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wazi.data.core;
using wazi.data.core.common.data;
using wazi.store.models;

namespace wazi.store.common {
    public sealed class MerchantService {
        IRepository repository = null;

        public MerchantService(IRepository repository) {
            this.repository = repository;
            setup();
        }

        private void setup() {
            if (this.repository != null)
                this.repository.AddObjectRepository<MerchantItem>(new MerchantCollection(repository));
        }

        public void RegisterMerchant(MerchantItem thismerchant) {
            if (null == thismerchant)
                throw new ArgumentNullException("store item provided is null, please send valid data");

            var collection = this.repository.GetObjectRepository<MerchantItem>() as MerchantCollection;
            if (null == collection)
                throw new ArgumentNullException("Store collection is not available and null");

            var existingMerchant = collection.Get(new List<FilterItem>() {
                new FilterItem("Name", thismerchant.Name, FilterOperator.equals)
            });

            if (null == existingMerchant) {
                collection.SaveItem(thismerchant);
            }
        }

        public void CloseMerchant(MerchantItem thismerchant) {
            if (null == thismerchant)
                throw new ArgumentNullException("store item provided is null, please send valid data");

            thismerchant.State = data.models.ConfigState.disabled;
            this.UpdateMerchant(thismerchant);
        }

        public void UpdateMerchant(MerchantItem thismerchant) {
            if (null == thismerchant)
                throw new ArgumentNullException("store item provided is null, please send valid data");

            var collection = this.repository.GetObjectRepository<MerchantItem>() as MerchantCollection;
            if (null == collection)
                throw new ArgumentNullException("Store collection is not available and null");

            collection.Update(new List<FilterItem>() {
                new FilterItem("ID", thismerchant.ID, FilterOperator.equals)
            }, thismerchant);
        }
    }
}
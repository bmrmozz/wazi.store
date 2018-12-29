using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wazi.store.models;
using Microsoft.Extensions.Options;
using wazi.data.core.master;
using wazi.store.common;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wazi.store.Controllers
{
    [Route("api/[controller]")]
    public class MerchantController : Controller
    {
        MerchantService merchantService;
        public MerchantController(IOptions<MasterRepository> repository) {
            this.merchantService = new MerchantService(repository.Value);
        }

        [HttpGet]
        public JsonResult Get() {
            var merchant = new MerchantItem() {
                Name = "Brandon Merchant"
            };

            merchant.SetDefaults();
            return Json(merchant);
        }

        [HttpPost]
        [Route("AddMerchant")]
        public void AddMerchant([FromBody]MerchantItem value)
        {
            if (null == value) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            try {
                this.merchantService.RegisterMerchant(value);
                Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        [HttpPost]
        [Route("CloseMerchant")]
        public void CloseMerchant([FromBody]MerchantItem value) {
            if (null == value) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            try {
                this.merchantService.CloseMerchant(value);
                Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        [HttpPost]
        [Route("UpdateMerchant")]
        public void UpdateMerchant([FromBody]MerchantItem value) {
            if (null == value) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            try {
                this.merchantService.UpdateMerchant(value);
                Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}

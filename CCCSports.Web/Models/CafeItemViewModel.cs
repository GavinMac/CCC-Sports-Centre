using CCCSports.Data.FacilitiesClasses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CCCSports.Web.Models
{
    public class CafeItemViewModel
    {

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public double ItemCost { get; set; }

        public int ItemStock { get; set; }

        public IEnumerable<CafeItemViewModel> CafeItems { get; set; }


        public static Expression<Func<CafeItem, CafeItemViewModel>> ViewModel
        {
            get
            {
                return e => new CafeItemViewModel()
                {
                    ItemId = e.ItemId,
                    ItemName = e.ItemName,
                    ItemCost = e.ItemCost,
                    ItemStock = e.ItemStock

                };
            }

        }


    }
}
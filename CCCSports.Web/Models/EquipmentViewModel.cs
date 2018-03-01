using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CCCSports.Web.Models
{
    public class EquipmentViewModel
    {

        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int AmountInStock { get; set; }

        public string EquipmentCondition { get; set; }

        public double EquipmentCost { get; set; }


        public IEnumerable<EquipmentViewModel> EquipmentList { get; set; }


        public static Expression<Func<Equipment, EquipmentViewModel>> ViewModel
        {
            get
            {
                return e => new EquipmentViewModel()
                {
                    EquipmentId = e.EquipmentId,
                    EquipmentName = e.EquipmentName,
                    AmountInStock = e.AmountInStock,
                    EquipmentCondition = e.EquipmentCondition,
                    EquipmentCost = e.EquipmentCost                
                };
            }

        }

    }
}
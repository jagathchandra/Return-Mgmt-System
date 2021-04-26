using PackagingAndDeliveringService.Models;
using PackagingAndDeliveringService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagingAndDeliveringService.Repository
{
    public class PackageDelivery : IPackageDelivery
    {
        private readonly testContext _context;

        public PackageDelivery()
        { }
        public PackageDelivery(testContext context)
        {
            _context = context;

        }
        
        public string PackageDeliver(string ComponentType,int count)
        {
            string deliveryCharge="";
            ProcessRequest processRequest = new ProcessRequest();
            processRequest.ComponentType = ComponentType;
            processRequest.Quantity = count;
            
            if(ComponentType == "Integral")
            {
                int amount = 100;
                count = Convert.ToInt32(count);
                int delcharge = 200;
                deliveryCharge = Convert.ToString((amount * count) + delcharge);
            }
            if (ComponentType == "Accessory")
            {
                int amount = 50;
                count = Convert.ToInt32(count);
                int delcharge = 100;
                deliveryCharge = Convert.ToString((amount * count) + delcharge);
            }
            _context.SaveChanges();
            return deliveryCharge;        
            

        }

    }
}

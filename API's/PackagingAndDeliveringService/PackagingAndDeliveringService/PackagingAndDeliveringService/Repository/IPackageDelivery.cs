using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagingAndDeliveringService.Repository
{
    public  interface IPackageDelivery
    {
        string PackageDeliver(string ComponentType, int count);
    }
}

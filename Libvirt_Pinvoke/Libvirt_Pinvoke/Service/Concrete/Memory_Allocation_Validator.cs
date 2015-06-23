﻿using Libvirt.Models.Interface;
using Libvirt.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libvirt.Service.Concrete
{
    public class Memory_Allocation_Validator : IService_Validator<Models.Concrete.Memory_Allocation, CS_Objects.Host>
    {
        public void Validate(IValdiator v, Models.Concrete.Memory_Allocation obj, CS_Objects.Host obj2)
        {
            Libvirt._virNodeInfo info;
            obj2.virNodeGetInfo(out info);
            var maxmemory = (info.memory / 1024);

            if (obj.maxMemory > maxmemory)
            {
                v.AddError("Memory_Allocation.maxMemory", "Cannot exceed the Maximum Available!");
            }
            if (obj.memory > obj.maxMemory)
            {
                v.AddError("Memory_Allocation.memory", "Cannot exceed the Maximum Available!");
            }
        }

    }
}

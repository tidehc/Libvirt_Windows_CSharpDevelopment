﻿using Libvirt.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libvirt.Models.Concrete
{

    public class Device : ITo_XML, IValidation
    {
        public enum Device_Types { file, block, dir, network, volume };
        public enum Device_Device_Types { floppy, disk, cdrom, lun };//default is disk
        public enum Snapshot_Types { _internal, external, _default };//Store the snapshots WITH the underlying storage, or seperate?
        public enum Driver_Types { raw, qcow2 };
        public enum Driver_Cache_Types { _default, none, writethrough, writeback, directsync, _unsafe };
        public enum Source_Startup_Policies { mandatory, requisite, optional };
        public enum Device_Bus_Types { ide, scsi, virtio, usb, sata, sd };//default is disk
        public Device()
        {
            Device_Type = Device_Types.dir;
            Device_Device_Type = Device_Device_Types.disk;
            Snapshot_Type = Snapshot_Types._default;
            Driver_Type = Driver_Types.raw;
            Driver_Cache_Type = Driver_Cache_Types._default;
            Device_Bus_Type = Device_Bus_Types.virtio;
            ReadOnly = false;
        }
        public Device_Types Device_Type { get; set; }
        public Device_Device_Types Device_Device_Type { get; set; }
        public Snapshot_Types Snapshot_Type { get; set; }
        public Driver_Types Driver_Type { get; set; }
        public Driver_Cache_Types Driver_Cache_Type { get; set; }
        public Device_Bus_Types Device_Bus_Type { get; set; }
        public IDevice_Source Source { get; set; }
        public bool ReadOnly { get; set; }
        public string To_XML()
        {
            throw new NotImplementedException("Use the overloaded version of To_XML and pass an hd");
        }
        public string To_XML(char hd_letter)
        {
            var ret = "<disk type='" + Device_Type.ToString() + "' device='" + Device_Device_Type.ToString() + "' ";
            if (Snapshot_Type != Snapshot_Types._default) ret += "snapshot='" + Snapshot_Type.ToString().Replace("_", "") + "'";
            ret += ">";
            ret += "<driver type='" + Driver_Type.ToString() + "' cache='" + Driver_Cache_Type.ToString().Replace("_", "") + "' />";
            ret += Source.To_XML();

            ret += "<target dev='";
            if (Device_Bus_Type == Device_Bus_Types.virtio) ret += "vd";
            else if (Device_Bus_Type == Device_Bus_Types.scsi) ret += "sd";
            else ret += "sd";
            ret += hd_letter + "' bus='" + Device_Bus_Type.ToString() + "' />";
            if (ReadOnly) ret += "<readonly/>";
            ret += "</disk>";
            return ret;
        }
        public void Validate(IValdiator v)
        {
            if (Device_Device_Type == Device_Device_Types.cdrom)
            {
                ReadOnly = true;// force this here
            }
            if (Device_Type == Device_Types.file && Device_Device_Type == Device_Device_Types.cdrom)
            {
                var src = Source as Device_Source_File;
                if (!string.IsNullOrWhiteSpace(src.file_path))
                {
                    if (!src.file_path.EndsWith(".iso"))
                    {
                        v.AddError("Source.file_path", "You must select an ISO!");
                    }
                }
                else
                {
                    v.AddError("Source.file_path", "You must select an ISO!");
                }
            }

        }
    }
}

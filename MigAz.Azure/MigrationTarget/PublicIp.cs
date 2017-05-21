﻿using MigAz.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigAz.Azure.MigrationTarget
{
    public class PublicIp : IMigrationTarget
    {
        private String _SourceName = String.Empty;
        private String _Name = String.Empty;


        public String SourceName
        {
            get { return _SourceName; }
            set { _SourceName = value; }
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value.Trim().Replace(" ", String.Empty); }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

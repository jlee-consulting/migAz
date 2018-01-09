﻿using MigAz.Core.ArmTemplate;
using MigAz.Core.Interface;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MigAz.Azure.Arm
{
    public class Subnet : ISubnet, IMigrationSubnet
    {
        private JToken _Subnet;
        private VirtualNetwork _Parent;
        private NetworkSecurityGroup _NetworkSecurityGroup;
        private RouteTable _RouteTable;

        private Subnet() { }

        public Subnet(VirtualNetwork parent, JToken subnet)
        {
            _Parent = parent;
            _Subnet = subnet;
        }

        public async Task InitializeChildrenAsync(AzureContext azureContext)
        {
            if (this.NetworkSecurityGroupId != string.Empty)
            {
                _NetworkSecurityGroup = await azureContext.AzureRetriever.GetAzureARMNetworkSecurityGroup(this.NetworkSecurityGroupId);
            }
            if (this.RouteTableId != string.Empty)
            {
                _RouteTable = await azureContext.AzureRetriever.GetAzureARMRouteTable(this.RouteTableId);
            }
        }

        public string Name
        {
            get { return (string)_Subnet["name"]; }
        }

        public string Id
        {
            get { return (string)_Subnet["id"]; }
        }

        public string TargetId
        {
            get { return this.Id; }
        }

        public string AddressPrefix
        {
            get { return (string)_Subnet["properties"]["addressPrefix"]; }
        }
        private string NetworkSecurityGroupId
        {
            get
            {
                if (_Subnet["properties"]["networkSecurityGroup"] == null)
                    return string.Empty;

                return (string)_Subnet["properties"]["networkSecurityGroup"]["id"];
            }
        }

        private string RouteTableId
        {
            get
            {
                if (_Subnet["properties"]["routeTable"] == null)
                    return string.Empty;

                return (string)_Subnet["properties"]["routeTable"]["id"];
            }
        }

        public VirtualNetwork Parent
        {
            get { return _Parent; }
        }

        public NetworkSecurityGroup NetworkSecurityGroup
        {
            get
            {
                return _NetworkSecurityGroup;
            }
        }
        public RouteTable RouteTable
        {
            get
            {
                return _RouteTable;
            }
        }

        public bool IsGatewaySubnet
        {
            get { return this.Name == ArmConst.GatewaySubnetName; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static bool operator ==(Subnet lhs, Subnet rhs)
        {
            bool status = false;
            if (((object)lhs == null && (object)rhs == null) ||
                    ((object)lhs != null && (object)rhs != null && lhs.Id == rhs.Id))
            {
                status = true;
            }
            return status;
        }

        public static bool operator !=(Subnet lhs, Subnet rhs)
        {
            return !(lhs == rhs);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus_Processing_Service
{
    public class Drone
    {
        // Private attributes
        private string clientName;
        private string droneModel;
        private string serviceProblem;
        private float serviceCost;
        private int serviceTag;

        // Public getters and setters for clientName
        public string GetClientName()
        {
            return clientName;
        }

        public void SetClientName(string ClientName)
        {
            ClientName = clientName;
        }

        // Public getters and setters for droneModel
        public string GetDroneModel()
        {
            return droneModel;
        }

        public void SetDroneModel(string DroneModel)
        {
            DroneModel = droneModel;
        }

        // Public getters and setters for serviceProblem
        public string GetServiceProblem()
        {
            return serviceProblem;
        }

        public void SetServiceProblem(string ServiceProblem)
        {
            ServiceProblem = serviceProblem;
        }

        // Public getters and setters for serviceCost
        public float GetServiceCost()
        {
            return serviceCost;
        }

        public void SetServiceCost(float ServiceCost)
        {
            ServiceCost = serviceCost;
        }

        // Public getters and setters for serviceTag
        public int GetServiceTag()
        {
            return serviceTag;
        }

        public void SetServiceTag(int ServiceTag)
        {
            ServiceTag = serviceTag;
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus_Processing_Service
{
    /*
    6.1	Create a separate class file to hold the data items of the Drone. Use separate getter and setter methods, 
    ensure the attributes are private and the accessor methods are public. Add a display method that returns a 
    string for Client Name and Service Cost. Add suitable code to the Client Name and Service Problem accessor 
    methods so the data is formatted as Title case or Sentence case. Save the class as “Drone.cs”.
    */
    public class Drone
    {
        // Private attributes
        private string clientName;
        private string droneModel;
        private string serviceProblem;
        private double serviceCost;
        private int serviceTag;

        // Public getters and setters for clientName
        public string GetClientName()
        {
            return clientName;
        }

        public void SetClientName(string ClientName)
        {
            clientName = FormatClientName(ClientName);
        }

        // Public getters and setters for droneModel
        public string GetDroneModel()
        {
            return droneModel;
        }

        public void SetDroneModel(string DroneModel)
        {
            droneModel = DroneModel;
        }

        // Public getters and setters for serviceProblem
        public string GetServiceProblem()
        {
            return serviceProblem;
        }

        public void SetServiceProblem(string ServiceProblem)
        {
            serviceProblem = FormatClientName(ServiceProblem);
        }

        // Public getters and setters for serviceCost
        public double GetServiceCost()
        {
            return serviceCost;
        }

        public void SetServiceCost(double ServiceCost)
        {
            serviceCost = ServiceCost;
        }

        // Public getters and setters for serviceTag
        public int GetServiceTag()
        {
            return serviceTag;
        }

        public void SetServiceTag(int ServiceTag)
        {
            serviceTag = ServiceTag;
        }

        // Display of data that has been dequeued from listview and moved into listbox
        public string Display()
        {
            return $"Client Name: {clientName}\nService Cost: {serviceCost:C}\nService Problem: {serviceProblem}";
        }

		private string FormatClientName(string name)
		{
			// Check if the provided name is not null or empty
			if (!string.IsNullOrEmpty(name))
			{
				// Split the name into an array of words using space as a delimiter
				string[] words = name.Split(' ');
				// Iterate through each word in the array
				for (int i = 0; i < words.Length; i++)
				{
					// Check if the word is not null or empty
					if (!string.IsNullOrEmpty(words[i]))
					{
						// Convert the first character of the word to uppercase and the rest to lowercase
						words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
					}
				}
				// Join the modified words back into a string separated by spaces and return it as Title Case
				return string.Join(" ", words);
			}
			// If the provided name is empty or null, return it as is
			return name;
		}

        public Drone() { }
    }
}

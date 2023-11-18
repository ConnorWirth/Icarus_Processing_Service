using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit;

namespace Icarus_Processing_Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		#region 6.2, 6.3, 6.4 - Global List and Global Queues
		// 6.2	Create a global List<T> of type Drone called “FinishedList”. 
		List<Drone> FinishedList = new List<Drone>();
        // 6.3	Create a global Queue<T> of type Drone called “RegularService”.
        Queue<Drone> RegularService = new Queue<Drone>();
        // 6.4	Create a global Queue<T> of type Drone called “ExpressService”.
        Queue<Drone> ExpressService = new Queue<Drone>();
		#endregion

		#region 6.5, 6.6 - AddNewItems Method and Express Cost Increase
		/*
        6.5	Create a button method called “AddNewItem” that will add a new service item to a Queue<> based on 
        the priority. Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost. Use a 
        numeric control for the Service Tag. The new service item will be added to the appropriate Queue based 
        on the Priority radio button.
        */
		private void AddNewItem(Queue<Drone> Queue, double cost)
        {
			// Create a new instance of Drone
			Drone drone = new Drone();
			// Set various properties of the drone object based on the input fields
			drone.SetDroneModel(textBoxDrone.Text);
			drone.SetClientName(textBoxClient.Text);
			drone.SetServiceProblem(textBoxProblem.Text);
			// Check if the Express radio button is checked
			if (radioButtonExpress.IsChecked == true)
			{
				// 6.6 Before a new service item is added to the Express Queue, the service cost must be increased by 15%.
				cost = cost * 1.15;
			}
			// Round the cost to two decimal places
			cost = Math.Round(cost, 2);
			// Set the service cost of the drone
			drone.SetServiceCost(cost);
			// Set the service tag for the drone using the value from the idTag TextBox
			drone.SetServiceTag(int.Parse(idTag.Text));
			// Increment the service tag
			IncrementServiceTag(idTag);
			// Enqueue the drone object into the specified Queue
			Queue.Enqueue(drone);
			// Update the display for both Regular and Express service
			DisplayRegularService();
			DisplayExpressService(); ;
        }
        
        // Button method that when clicked, adds a new item into a listview depending on the priority of the service
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
			// Check if any essential text fields are empty or if no service type is selected
			if (string.IsNullOrEmpty(textBoxDrone.Text) ||
				string.IsNullOrEmpty(textBoxClient.Text) ||
				string.IsNullOrEmpty(textBoxProblem.Text) ||
				textBoxCost.Text.Length == 0 ||
				(radioButtonRegular.IsChecked == false && radioButtonExpress.IsChecked == false))
			{
				// Clear the status strip and display a message indicating empty fields/buttons
				statusStrip1.Items.Clear();
				statusStrip1.Items.Add("Empty textbox/es or buttons");
			}
			else
			{
				// Ensure that the service priority is properly set
				GetServicePriority();
				// Check which service type is selected
				if (radioButtonRegular.IsChecked == true)
				{
					// Call the AddNewItem method for RegularService if Regular service is selected
					AddNewItem(RegularService, double.Parse(textBoxCost.Text));
				}
				else if (radioButtonExpress.IsChecked == true)
				{
					// Call the AddNewItem method for ExpressService if Express service is selected
					AddNewItem(ExpressService, double.Parse(textBoxCost.Text));
				}
				// Clear all textboxes after adding data
				ClearTextBoxes();
				// Clear the status strip and indicate successful addition of data
				statusStrip1.Items.Clear();
				statusStrip1.Items.Add("Data successfully added");
			}
		}
		#endregion

		#region 6.7 - GetServicePriority Method
		/*
        6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group. 
        This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        */
		private string GetServicePriority()
        {
			// Check which radio button is selected and return its value
			if (radioButtonRegular.IsChecked == true)
			{
				return "Regular"; // Return 'Regular' if Regular radio button is checked
			}
			else if (radioButtonExpress.IsChecked == true)
			{
				return "Express"; // Return 'Express' if Express radio button is checked
			}
			else
			{
				return string.Empty; // Return an empty string if no radio button is selected
			}
		}
		#endregion

		#region 6.8, 6.9 - DisplayRegularService and DisplayExpressService Methods
		/*
        6.8	Create a custom method that will display all the elements in the RegularService queue. The display must 
        use a List View and with appropriate column headers.
        */
		private void DisplayRegularService()
        {
			// Clear the items in the listViewRegular
			listViewRegular.Items.Clear();
			// Iterate through each 'Drone' object in the RegularService queue
			foreach (Drone regular in RegularService)
			{
				// Add a new item to the listViewRegular
				listViewRegular.Items.Add(new
				{
					// Define properties for the newly added item in the ListView
					regularModel = regular.GetDroneModel(),       // Set 'regularModel' property to Drone's model
					regularClient = regular.GetClientName(),      // Set 'regularClient' property to Drone's client name
					regularCost = "$" + regular.GetServiceCost(), // Set 'regularCost' property to Drone's service cost (prefixed with '$')
					regularProblem = regular.GetServiceProblem(), // Set 'regularProblem' property to Drone's service problem
					regularTag = regular.GetServiceTag(),         // Set 'regularTag' property to Drone's service tag
				});
			}
		}

        /*
        6.9	Create a custom method that will display all the elements in the ExpressService queue. The display must 
        use a List View and with appropriate column headers.
        */
        private void DisplayExpressService()
        {
			// Clear the items in the listViewExpress
			listViewExpress.Items.Clear();
			// Iterate through each 'Drone' object in the RegularService queue
			foreach (Drone express in ExpressService)
            {
				// Add a new item to the listViewExpress
				listViewExpress.Items.Add(new
                {
					// Define properties for the newly added item in the ListView
					expressModel = express.GetDroneModel(),       // Set 'expressModel' property to Drone's model
					expressClient = express.GetClientName(),      // Set 'expressClient' property to Drone's client name
					expressCost = "$" + express.GetServiceCost(), // Set 'expressCost' property to Drone's service cost (prefixed with '$')
					expressProblem = express.GetServiceProblem(), // Set 'expressProblem' property to Drone's service problem
					expressTag = express.GetServiceTag(),         // Set 'expressTag' property to Drone's service tag
				});
            }
        }
		#endregion

		#region 6.10 - Check Double Value in Service Cost Textbox
		// 6.10	Create a custom method to ensure the Service Cost textbox can only accept a double value with two decimal point.
		private void TextBoxServiceCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
			// Create a Regex object for validating input with a double value with two decimal point
			Regex regex = new Regex(@"^\d+(\.\d{0,2})?$");
			// Check if the input text matches the defined regex pattern and set e.Handled accordingly
			e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
		}
		#endregion

		#region 6.11 - Increment IntegerUpDown Control
		/*
        6.11 Create a custom method to increment the service tag control, this method must be called inside the “AddNewItem” 
        method before the new service item is added to a queue.
        */
		private void IncrementServiceTag(IntegerUpDown integerUpDown)
        {
			// Check if the integerUpDown control is not null
			if (integerUpDown != null)
			{
				// Get the current value of the integerUpDown control, or set it to 0 if it's null
				int currentValue = integerUpDown.Value ?? 0;
				// Increase the current value by 10 and set the updated value to the integerUpDown control
				integerUpDown.Value = currentValue + 10;
			}
		}
		#endregion

		#region 6.12, 6.13 - Selection Change Display in Regular and Express ListViews
		/*
        6.12 Create a mouse click method for the regular service ListView that will display the Client Name and Service Problem in 
        the related textboxes.
        */
		private void listViewRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			// Get the selected index from listViewRegular
			int index = listViewRegular.SelectedIndex;
			// Clear the selected items in listViewExpress
			listViewExpress.SelectedItems.Clear();
			// Call a method (displaySelection) passing RegularService and the retrieved index as parameters
			displaySelection(RegularService, index);
			// Clear the items in statusStrip1
			statusStrip1.Items.Clear();
		}

        /*
        6.13 Create a mouse click method for the express service ListView that will display the Client Name and Service Problem in 
        the related textboxes.
        */
        private void listViewExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			// Get the selected index from listViewExpress
			int index = listViewExpress.SelectedIndex;
			// Clear the selected items in listViewRegular
			listViewRegular.SelectedItems.Clear();
			// Call a method (displaySelection) passing ExpressService and the retrieved index as parameters
			displaySelection(ExpressService, index);
			// Clear the items in statusStrip1
			statusStrip1.Items.Clear();
        }

		// Method that displays list information into textboxes when row is clicked on in listviews
		private void displaySelection(Queue<Drone> Queue, int index)
        {
			// Check if the index retrieved from the ListView is valid (greater than or equal to 0)
			if (index >= 0)
			{
				// Convert the Queue to an array and then to a list
				List<Drone> list = Queue.ToArray().ToList();
				// Display specific details of the Drone object at the specified index in the TextBoxes
				textBoxClient.Text = list[index].GetClientName(); // Display client name
				textBoxDrone.Text = list[index].GetDroneModel(); // Display drone model
				textBoxCost.Text = list[index].GetServiceCost().ToString(); // Display service cost
				textBoxProblem.Text = list[index].GetServiceProblem(); // Display service problem
			}
			else
			{
				// If the index is less than 0 (ListView is empty or no selection), show a message in the status strip
				statusStrip1.Items.Clear();
				statusStrip1.Items.Add("Listview is empty");
			}
		}
		#endregion

		#region 6.14, 6.15 - Dequeue Item from Regular and Express ListView to ListBox
		/*
        6.14 Create a button click method that will remove a service item from the regular ListView and dequeue the regular service Queue<T> 
        data structure. The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        */
		private void RemoveRegularServiceItem_Click(object sender, RoutedEventArgs e)
        {
			// Check if the RegularService Queue has items
			if (RegularService.Count > 0)
			{
				// Dequeue (remove) an item from the RegularService Queue
				Drone removedItems = RegularService.Dequeue();
				// Add the dequeued item to the FinishedList
				FinishedList.Add(removedItems);
				// Add the display information of the dequeued item to a ListBox
				listBoxServices.Items.Add(removedItems.Display());
				// Refresh the display of RegularService items in listview
				DisplayRegularService();
				// Clear the status strip and add a message indicating successful addition to the listbox
				statusStrip1.Items.Clear();
				statusStrip1.Items.Add("Item added to listbox");
			}
		}

        /*
        6.15 Create a button click method that will remove a service item from the express ListView and dequeue the express service Queue<T> 
        data structure. The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        */
        private void RemoveExpressServiceItem_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
				// Dequeue (remove) an item from the ExpressService Queue
				Drone removedItem = ExpressService.Dequeue();
                // Add the dequeued item to the FinishedList
                FinishedList.Add(removedItem);
				// Add the display information of the dequeued item to a ListBox
				listBoxServices.Items.Add(removedItem.Display());
				// Refresh the display of ExpressService items in listview
				DisplayExpressService();
				// Clear the status strip and add a message indicating successful addition to the listbox
				statusStrip1.Items.Clear();
                statusStrip1.Items.Add("Item added to listbox");
            }
        }
		#endregion

		#region 6.16 - Double-Mouse Click Deletion in ListBox
		/*
        6.16 Create a double mouse click method that will delete a service item from the finished listbox and remove the same item 
        from the List<T>.
        */
		private void ListViewFinished_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			// Check if there's any selected item in listBoxServices
			if (listBoxServices.SelectedItems.Count > 0)
			{
				// Retrieve the index of the selected item in the listBoxServices
				int removedItem = listBoxServices.SelectedIndex;
				// Remove the item from the listBoxServices at the retrieved index
				listBoxServices.Items.RemoveAt(removedItem);
				// Remove the corresponding item from the FinishedList at the same index
				FinishedList.RemoveAt(removedItem);
				// Clear the status strip and add a message indicating successful removal from the listbox
				statusStrip1.Items.Clear();
				statusStrip1.Items.Add("Item removed from listbox");
			}
		}
		#endregion

		#region 6.17 - ClearTextBoxes Method
		// 6.17	Create a custom method that will clear all the textboxes after each service item has been added.
		private void ClearTextBoxes()
        {
            // Clear all textboxes on the top left side of the program
			textBoxClient.Clear();
            textBoxCost.Clear();
            textBoxDrone.Clear();
            textBoxProblem.Clear();
        }
		#endregion
	}
}

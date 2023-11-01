using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        // 6.2	Create a global List<T> of type Drone called “FinishedList”. 
        List<Drone> FinishedList = new List<Drone>();
        // 6.3	Create a global Queue<T> of type Drone called “RegularService”.
        Queue<Drone> RegularService = new Queue<Drone>();
        // 6.4	Create a global Queue<T> of type Drone called “ExpressService”.
        Queue<Drone> ExpressService = new Queue<Drone>();

        /*
        6.5	Create a button method called “AddNewItem” that will add a new service item to a Queue<> based on 
        the priority. Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost. Use a 
        numeric control for the Service Tag. The new service item will be added to the appropriate Queue based 
        on the Priority radio button.
        */
        private void AddNewItem(Queue<Drone> Queue, double cost)
        {
            Drone drone = new Drone();
            drone.SetDroneModel(textBoxDrone.Text);
            drone.SetClientName(textBoxClient.Text);
            drone.SetServiceProblem(textBoxProblem.Text);

            // 6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
            if (radioButtonExpress.IsChecked == true)
            {
                // Increase the cost by 15% for Express service
                cost = cost * 1.15;
            }
            cost = Math.Round(cost, 2);

            drone.SetServiceCost(cost);
            drone.SetServiceTag(int.Parse(idTag.Text));

            Queue.Enqueue(drone);
            DisplayRegularService();
            DisplayExpressService();
        }
        
        // Button method that when clicked, adds a new item into a listview depending on the priority of the service
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDrone.Text) ||
                string.IsNullOrEmpty(textBoxClient.Text) ||
                string.IsNullOrEmpty(textBoxProblem.Text) ||
                textBoxCost.Text.Length == 0 ||
                (radioButtonRegular.IsChecked == false && radioButtonExpress.IsChecked == false))
            {
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add("Empty textbox/es or buttons");
            }

            else
            {
                GetServicePriority(); // Ensure this method sets the priority properly

                if (radioButtonRegular.IsChecked == true)
                {
                    AddNewItem(RegularService, double.Parse(textBoxCost.Text));
                }
                else if (radioButtonExpress.IsChecked == true)
                {
                    AddNewItem(ExpressService, double.Parse(textBoxCost.Text));
                }

                ClearTextBoxes();

                statusStrip1.Items.Clear();
                statusStrip1.Items.Add("Data successfully added");
            }
        }

        /*
        6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group. 
        This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        */
        private string GetServicePriority()
        {
            // Check which radio button is selected and return its value
            if (radioButtonRegular.IsChecked == true)
            {
                return "Regular";
            }
            else if (radioButtonExpress.IsChecked == true)
            {
                return "Express";
            }
            else
            {
                return string.Empty; // No radio button is selected
            }
        }

        /*
        6.8	Create a custom method that will display all the elements in the RegularService queue. The display must 
        use a List View and with appropriate column headers.
        */
        private void DisplayRegularService()
        {
            if (radioButtonRegular.IsChecked == true)
            {
                listViewRegular.Items.Clear();

                foreach (Drone regular in RegularService)
                {
                    listViewRegular.Items.Add(new
                    {
                        regularModel = regular.GetDroneModel(),
                        regularClient = regular.GetClientName(),
                        regularCost = "$" + regular.GetServiceCost(),
                        regularProblem = regular.GetServiceProblem(),
                        regularTag = regular.GetServiceTag(),
                    });
                }
            }
        }

        /*
        6.9	Create a custom method that will display all the elements in the ExpressService queue. The display must 
        use a List View and with appropriate column headers.
        */
        private void DisplayExpressService()
        {
            if (radioButtonExpress.IsChecked == true)
            {
                listViewExpress.Items.Clear();

                foreach (Drone express in ExpressService)
                {
                    listViewExpress.Items.Add(new
                    {
                        expressModel = express.GetDroneModel(),
                        expressClient = express.GetClientName(),
                        expressCost = "$" + express.GetServiceCost(),
                        expressProblem = express.GetServiceProblem(),
                        expressTag = express.GetServiceTag(),
                    });
                }
            }
        }



        // 6.17	Create a custom method that will clear all the textboxes after each service item has been added.
        private void ClearTextBoxes()
        {
            textBoxClient.Clear();
            textBoxCost.Clear();
            textBoxDrone.Clear();
            textBoxProblem.Clear();
        }
    }
}

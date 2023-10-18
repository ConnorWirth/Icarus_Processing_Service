# Icarus_Processing_Service

# Scenario
As the Senior Programmer for CITE Managed Services, your task is to develop a fully functional Drone Service Application for Icarus Pty Ltd who operates a local service and repair company. The application will be used by front desk staff to log drones for service and repair.

# Background Information

The Icarus organisation has a long history for servicing all types of drones and unmanned aircraft, their reputation for quality and discretion has made them the leaders in the service industry. The company offers two categories of service, regular and express. This service is unique because once a service tag is issued the policy is to repair or replace, which ensures all services are completed on time. The staff at Icarus has made the following requests that will need to be included in any solution.

# Application Criteria

The Service Processing Application must use two Queue<T> data structures of a simple class which has the following five attributes: Client Name, Drone Model, Service Problem, Service Cost and Service Tag.

When a client delivers a drone to Icarus for attention the front desk staff will enter the details as required to populate the Drone class. The client will be able to select between a regular or express priority for the service of their drone. The express priority service will incur an additional 15% charge to the service cost. Once the priority has been selected the drone will be added to one of the two queues (regular or express). The client’s drone will be tagged and send to the service department for inspection and repair/service. Once the drone is repaired and returned to the front desk the Icarus staff will remove the details from the queue. This removal process will dequeue the appropriate data structure and add the details to the list of completed work. Finally, the client will be able to pay for the work and collect their drone; the staff on the front desk will then remove the item from the finished work list.

All user interactions must have full error trapping and feedback messaging which is displayed in a status strip at the bottom of the form. The need to use a message box for critical errors or general feedback is not necessary.

You should consult with the CITE representative (Your Lecturer) if you are unsure about any of the problems or questions. Your primary research should focus on the resources on the Blackboard website, additional information can be collected from the Internet, ensure all sources are referenced at the end of your report. You should write your answers in the standard templates provided in this assessment document. 

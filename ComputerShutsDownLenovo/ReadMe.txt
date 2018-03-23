Situation:
Lenovo Think Pad connected to a USB port replicator shuts down the monitors every 10 to 15 minutes of in-activity.  No matter what the power setting configured in the OS has been set to. 

Previously tried solutions: 
Created a local admin 
Verified setting for what happens when the laptop lid closes. 
Checked power config as administrator
checked BIOS setting to make sure that's not an issue. 
Updated BIOS for both the Lenovo Port replicator and laptop. 


End result that solved the problem: 
compmgmt.msc ==> Device Manger ==> USB ports 
Individually each of the 12 ports must remove the configuration property under power management that allows the computer to turn OFF the device to save computer power.  
~ After some pre-determined period of time these USB ports are powered off.  
~ Since the monitors, and network are run through the USB port. 
~ It was this setting that shuts it down. 


not done YET! 

Finally solution was to keep the laptop open.  
Closing the lid allows the usb ports to shut down. 

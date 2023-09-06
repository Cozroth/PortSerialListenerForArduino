# Port Serial Listener For Arduino
A simple software to  listen to the desired COM port defined in Program.cs 
```C#
SerialPort mySerialPort = new SerialPort("COM8");
```
Set it to the port your arduino is connected to


## Usage
Reading a light sensor value and printing it to file and every hour making a new line with the NEW HOUR text and the date time
Used to get data from a light sensor over time to be able to analyse and get the average values for usage in other projects
using System;
using System.IO.Ports;
using System.Windows;

namespace Serial.IButton
{
    class IButton
    {
        private SerialPort serport; //SerialPort-Object

        //Needed variables
        private string ids = string.Empty;                           
        
        public string read_IDs(string Com,int baut)
        {
            //Only for test if the data reading works fine
            try
            {
                serialinit(Com, baut);
                serport.Open(); //Opens the serial-connection

                ids = serport.ReadLine(); //Reads the data from the serial connection                
                serport.Close(); //Closing the serial-connection
                //if (ids.Length == 20) //Check if the data stored in IDs is a valid
                return ids; //Output of the valid IDs
                //else
                    //MessageBox.Show("IDs fehlerhaft!"); //Error if the data in "mac" is not a valid MAC-Address
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error"); //Shows an error if there is something wrong with the serial connection

                serport.Close(); //Closing the serial-connection
            }

            return "-";
        }
        
        private void serialinit(string Com, int baut)
        {
            serport = new SerialPort(Com); //New instance of the SerialPort-Class with the used COM-Interface as transfer parameter
            serport.BaudRate = baut; //Setting the BaudRate to 9600
            serport.Parity = Parity.None; //Setting Parity to None
            serport.DataBits = 8; //Used Bits for data is set to 8
            serport.StopBits = StopBits.One; //StopBit is set to 1
        }
    }

}


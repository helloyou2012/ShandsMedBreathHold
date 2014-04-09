using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace ShandsMedBreathHold
{
    public class DataReceiver
    {
        private Thread _thread;
        private bool _isRunning;
        private DataReceiverListener _listener;
        private SerialPort _serialPort;
        private Random _rand;

        public static string[] GetPortNames(){
            return SerialPort.GetPortNames();
        }

        public DataReceiver(DataReceiverListener lis, string portName)
        {
            Console.WriteLine(portName);
            this._listener = lis;
            this._thread = null;
            this._isRunning = false;
            this._rand = new Random();

            _serialPort = new SerialPort();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = 115200;
            _serialPort.ReadTimeout = 1000;
            _serialPort.WriteTimeout = 1000;
            try
            {
                _serialPort.Open();
                _listener.isOpened(true);
            }
            catch (Exception)
            {
                _listener.isOpened(false);
            }
        }

        public void close()
        {
            _isRunning = false;
            if (_thread!=null&&_thread.IsAlive == true)
            {
                _thread.Abort();
            }
            _serialPort.Close();
            _listener.isOpened(false);
        }

        public void toggleReceive()
        {
            if (_thread==null)
            {
                // start worker threads.
                ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoop);
                _thread = new Thread(addDataThreadStart);
                _thread.Start();
            }
            _isRunning = !_isRunning;
            _listener.isRunning(_isRunning);
        }

        private void AddDataThreadLoop()
        {
            while (true)
            {
                if (_isRunning)
                {
                    try
                    {
                        string line=_serialPort.ReadLine();
                        Console.WriteLine(line);
                        _listener.dataReceived(_rand.Next(10, 20));
                        //Thread.Sleep(20);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}

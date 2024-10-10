﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaxTimerIII
{
    public partial class MainForm : Form
    {

        // Constants
        private const int MAXITEMTXT = 256;
        private const string PaltalkClassName = "DlgGroupChat Window Class";
        private const string ListViewClassName = "SysHeader32";
        private const int LVM_GETITEMCOUNT = 0x1004;
        private const int LVM_GETITEM = 0x1005;

        // P/Invoke Structures and Delegates
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LVITEMW
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private int iMaxNicks = 0;

        // P/Invoke Functions
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
         int dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        // Process access rights
        private const uint PROCESS_VM_OPERATION = 0x0008;
        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_VM_WRITE = 0x0020;

        // Virtual memory constants
        private const uint MEM_COMMIT = 0x00001000;
        private const uint MEM_RELEASE = 0x00008000;
        private const uint PAGE_READWRITE = 0x04;

        // Global Variables
        private IntPtr ghMain = IntPtr.Zero;
        private IntPtr ghList = IntPtr.Zero;
        private IntPtr ghPtRoom = IntPtr.Zero;
        private IntPtr ghPtLv = IntPtr.Zero;
        public string gstrSavedNick = string.Empty;
        public string gstrCurrentNick = string.Empty;
        public int giMicTimerSeconds = 0;  
        private int iDrp = 0;
        private string strClock;

        public byte[] StructureToByteArray(object obj)
        {
            int num = Marshal.SizeOf(RuntimeHelpers.GetObjectValue(obj));
            byte[] array = new byte[checked(num - 1 + 1)];
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            Marshal.StructureToPtr(RuntimeHelpers.GetObjectValue(obj), intPtr, fDeleteOld: true);
            Marshal.Copy(intPtr, array, 0, num);
            Marshal.FreeHGlobal(intPtr);
            return array;
        }

        public object ByteArrayToStructure(byte[] arr)
        {
            int num = arr.Length;
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            Marshal.Copy(arr, 0, intPtr, num);
            object objectValue = RuntimeHelpers.GetObjectValue(Marshal.PtrToStructure(intPtr, typeof(LVITEMW)));
            Marshal.FreeHGlobal(intPtr);
            return objectValue;
        }

        public MainForm()
        {
            InitializeComponent();
            ghMain = this.Handle;
        }


      
        private void MainForm_Load(object sender, EventArgs e)
        {
           // MessageBox.Show("Main Form load");
        }

        private void BtnGetPt_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString());
            GetPaltalkWindows();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if( BtnStart.Text == "Start")
            {
                TimerMonitor.Start();
                BtnStart.Text = "Stop";
            }
            else if( BtnStart.Text == "Stop")
            {
                TimerMonitor.Stop();
                MicTimerReset();
                gstrSavedNick = string.Empty;
                BtnStart.Text = "Start";
            }
                           
        }

        // Exit the application
        private void BtnExit_Click(object sender, EventArgs e)
        {
            MainForm.ActiveForm.Close();
        }

        // Clear the list of history
        private void ButtonHistoryClear_Click(object sender, EventArgs e)
        {
            ListBoxHistory.Items.Clear();
        }      

        // Method to find Paltalk windows
        private void GetPaltalkWindows()
        {
            ghPtLv = IntPtr.Zero;
            ghPtRoom = IntPtr.Zero;

            // Find the Paltalk chat room window
            ghPtRoom = FindWindow(PaltalkClassName, null);

            if (ghPtRoom != IntPtr.Zero)
            {
                // Get window text
                StringBuilder sbWindowText = new StringBuilder(200);
                GetWindowText(ghPtRoom, sbWindowText, sbWindowText.Capacity);
                string windowText = sbWindowText.ToString();

                // Enumerate child windows to find the list view
                EnumChildWindows(ghPtRoom, EnumPaltalkWindowsCallback, IntPtr.Zero);

                // Update History List Box
                ListBoxHistory.Items.Add($"Timing: " + sbWindowText);
            }
            else
            {
                MessageBox.Show("No Paltalk Window Found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Callback method for EnumChildWindows
        private bool EnumPaltalkWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            StringBuilder sbClassName = new StringBuilder(256);
            GetClassName(hWnd, sbClassName, sbClassName.Capacity);
            string className = sbClassName.ToString();
            Debug.WriteLine(className);

            if (className == ListViewClassName)
            {
                ghPtLv = hWnd;
                Debug.WriteLine($"We got list view handle: {ghPtLv}");
              
                return false; // Stop enumeration, we got it
            }

            return true; // Continue enumeration
        }

        // Retrieve the list view items (nicknames)
        private string GetMicUser()
        {
            string strNickname = string.Empty;

            if (ghPtLv == IntPtr.Zero)
            {
                MessageBox.Show("No Paltalk Room Handle!", "GetMicUser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty; // No Paltalk ListView handle
            } 
                               
            checked
            {
                try
                {
                    IntPtr userListHdl = ghPtLv;

                    int iNuberOfNiks = (int)SendMessage(userListHdl, 4100, IntPtr.Zero, IntPtr.Zero);
                    TxtCurrentNicks.Text = iNuberOfNiks.ToString();
                    if(iNuberOfNiks > iMaxNicks) iMaxNicks = iNuberOfNiks;
                    TxtMaxNicks.Text = iMaxNicks.ToString();

                    GetWindowThreadProcessId(userListHdl, out uint lpdwProcessId);

                    IntPtr hPorc = OpenProcess(2035711, bInheritHandle: false, (uint)lpdwProcessId);
                    int iNumForIndex = iNuberOfNiks - 1;

                    for (int i = 0; i <= iNumForIndex; i++)
                    {
                        ListViewItem listViewItem = new ListViewItem();
                        int iSizeOfLvItemW = Marshal.SizeOf(typeof(LVITEMW));
                                                
                        IntPtr pRemoteMem = VirtualAllocEx(hPorc, IntPtr.Zero, iSizeOfLvItemW, 12288u, 4u);

                        LVITEMW lVITEMW = default;
                        lVITEMW.mask = 3;
                        lVITEMW.iSubItem = 0;
                        lVITEMW.iItem = i;
                        int iTxtSize = 256;
                        IntPtr pRemoteNick = (lVITEMW.pszText = VirtualAllocEx(hPorc, IntPtr.Zero, iTxtSize, 12288u, 4u));
                        lVITEMW.cchTextMax = iTxtSize;

                        byte[] byteArr2Write = StructureToByteArray(lVITEMW);
                        IntPtr lpNumberOfBytesWritten = (IntPtr)0;
                        WriteProcessMemory(hPorc, pRemoteMem, byteArr2Write, iSizeOfLvItemW, out lpNumberOfBytesWritten);
                        SendMessage(userListHdl, 4171, (IntPtr)i, pRemoteMem); // win32 API to get list view item
                        IntPtr lpNumberOfBytesRead = (IntPtr)0;
                        ReadProcessMemory(hPorc, pRemoteMem, byteArr2Write, iSizeOfLvItemW, out lpNumberOfBytesRead);
                        object obj = ByteArrayToStructure(byteArr2Write);
                        listViewItem.ImageIndex = ((obj != null) ? ((LVITEMW)obj) : default).iImage; // Get the image number indicating mic user
                        if (listViewItem.ImageIndex == 10)
                        {
                            SendMessage(userListHdl, 4141, (IntPtr)i, pRemoteMem); // Get the remote data bytes
                            byte[] array2 = new byte[iTxtSize - 1 + 1]; //make a buff for the nickname byte array 
                            lpNumberOfBytesRead = (IntPtr)0;
                            // Read the remote byte array containing the nickname 
                            ReadProcessMemory(hPorc, pRemoteNick, array2, iTxtSize, out lpNumberOfBytesRead);
                            // Turn the byte array into text 
                            listViewItem.Text = Encoding.Default.GetString(array2).TrimEnd(default(char));
                        }
                        VirtualFreeEx(hPorc, pRemoteNick, 0, 32768); // Free the remote memory of the Nick
                        VirtualFreeEx(hPorc, pRemoteMem, 0, 32768); // Free the remote memory of List View Item
                        if (listViewItem.ImageIndex == 10)
                        {
                            // we have the Nickname om mic, add it to the List Box
                            strNickname = listViewItem.Text.ToString();
                            
                            //  MessageBox.Show(strNickname, "Nickname Reader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    CloseHandle(hPorc);
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    MessageBox.Show("Exception: " + ex2.Message);
                    return string.Empty;
                }

            }

            return strNickname;
        }

        // Helper method to get process ID
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Override OnLoad to set up enumeration callback
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Assign the callback
            EnumWindowsProc callback = new EnumWindowsProc(EnumPaltalkWindowsCallback);
            // No action needed on load
        }

        //Timer Start
        private void MicTimerStart()
        {
            TxtClock.Text = "00:00";
            giMicTimerSeconds = 0;

            TimerMic.Start();

        }

        private void MicTimerReset()
        {
            TimerMic.Stop();
            TxtClock.Text = "00:00";
            giMicTimerSeconds = 0;
        }

        private void TimerMonitor_Tick(object sender, EventArgs e)
        {
           
            // Get Nick on mic
            gstrCurrentNick = GetMicUser();
            if (gstrSavedNick == string.Empty && gstrCurrentNick == string.Empty)
                return; // No User on mic do nothing
                        
            // Stopped talking or mic dropout 
            if(gstrCurrentNick == string.Empty && gstrSavedNick != String.Empty )
            {
                iDrp++;
                // This is to tolerate dropout 
                Debug.WriteLine($"Dropout count: {iDrp}");
                if(iDrp > 4)
                {
                    MicTimerReset();             
                    gstrSavedNick = string.Empty;
                    Debug.WriteLine($"5 Dropouts count: {iDrp} reset mic timer");
                    iDrp = 0;
                }
                                
            }
            // New Nick on mic -> start timer
            else if(gstrCurrentNick != gstrSavedNick)
            {
                gstrSavedNick = gstrCurrentNick;
                MicTimerReset();
                MicTimerStart();

                ListBoxHistory.Items.Add(" Started timing: " + gstrCurrentNick);

            }

           

        }

        // This fires every second (1000ms)  
        private void TimerMic_Tick(object sender, EventArgs e)
        {
            int iX = 60;
            int iMin;
            int iSec;   
            
            string strMin;
            string strSec;

            giMicTimerSeconds++;
            
            iMin = giMicTimerSeconds / iX;
            iSec = giMicTimerSeconds % iX;
            if (iSec < 10) strSec = $"0{iSec}";
            else strSec = iSec.ToString();
            if (iMin < 10) strMin = $"0{iMin}";
            else strMin = iMin.ToString();  
            strClock = strMin + ":" + strSec;
            Debug.WriteLine(strClock);
                       
            TxtClock.Text = strClock;
        }
    }
}
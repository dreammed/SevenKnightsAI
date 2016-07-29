using AutoIt;
using MinimizeCapture;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SevenKnightsAI.Classes
{
	internal class AutoSpy
	{
		#region Private Fields
		private const int GWL_EXSTYLE = -20;

		private const int LWA_ALPHA = 2;

		private const int LWA_COLORKEY = 1;

		private const uint SWP_NOMOVE = 2u;

		private const int WHEEL_DELTA = 120;

		private const uint WM_GETTEXT = 13u;

		private const uint WM_GETTEXTLENGTH = 14u;

		private const uint WM_KEYDOWN = 256u;

		private const uint WM_KEYUP = 257u;

		private const uint WM_LBUTTONDOWN = 513u;

		private const uint WM_MBUTTONDOWN = 519u;

		private const uint WM_MOUSEMOVE = 512u;

		private const uint WM_MOUSEWHEEL = 522u;

		private const uint WM_RBUTTONDOWN = 516u;

		private const uint WM_SHOWWINDOW = 64u;

		private const long WS_CAPTION = 12582912L;

		private const int WS_EX_LAYERED = 524288;

		private const long WS_MAXIMIZEBOX = 65536L;

		private const long WS_MINIMIZEBOX = 131072L;

		private const long WS_OVERLAPPED = 0L;

		private const long WS_SIZEBOX = 262144L;

		private const long WS_SYSMENU = 524288L;

		private Random random;
		#endregion Private Fields

		#region Public Properties

		public IntPtr ControlHandle
		{
			get;
			private set;
		}

		public Bitmap CurrentFrame
		{
			get;
			private set;
		}

		public IntPtr Handle
		{
			get;
			private set;
		}

		#endregion Public Properties

		#region Public Constructors

		public AutoSpy(IntPtr handle) : this(handle, handle)
		{ }

		public AutoSpy(IntPtr handle, IntPtr controlHandle)
		{
			WindowSnap.ForceMDICapturing = false;
			this.Handle = handle;
			this.ControlHandle = controlHandle;
			this.random = new Random();
			this.CaptureFrame(true, true);
		}

		#endregion Public Constructors

		#region Public Methods

		public static int ColorToInt(Color color)
		{
			string value = string.Format("{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
			return Convert.ToInt32(value, 16);
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr parentHWnd, IntPtr childAfterHWnd, string className, string windowTitle);

		public static IntPtr GetControlHandle(string title, string control)
		{
			IntPtr handle = AutoSpy.GetHandle(title, null);
			IntPtr result = AutoItX.ControlGetHandle(handle, control);
			if (result.ToInt32() == 0)
			{
				throw new Exception("ControlHandle not found");
			}
			return result;
		}

		public static IntPtr GetHandle(string title, string className = null)
		{
			IntPtr result = AutoSpy.FindWindow(className, title);
			if (result.ToInt32() == 0)
			{
				throw new Exception("Handle not found");
			}
			return result;
		}

		public static int GetPixel(WindowSnap snap, int x, int y)
		{
			return AutoSpy.ColorToInt(snap.Image.GetPixel(x, y));
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern void GetWindowText(IntPtr handle, StringBuilder resultWindowText, int maxTextCapacity);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr PostMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, StringBuilder text);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		public void BringToFront()
		{
			AutoSpy.SetForegroundWindow(this.Handle);
			Thread.Sleep(50);
		}

		public Bitmap CaptureFrame(bool backgroundMode = true, bool cache = true)
		{
			if (this.CurrentFrame != null && cache)
			{
				this.CurrentFrame.Dispose();
			}
			Bitmap bitmap;
			if (backgroundMode)
			{
				WindowSnap windowSnap = WindowSnap.GetWindowSnap(this.Handle, true);
				bitmap = windowSnap.Image;
			}
			else
			{
				bitmap = ForegroundCapture.CaptureWindow(this.Handle);
			}
			if (cache)
			{
				this.CurrentFrame = bitmap;
			}
			return bitmap;
		}

		public void Click(int x, int y, int numClicks = 1, int delay = 0, string button = "left")
		{
			uint num = this.MakeButtonMessage(button);
			uint lParam = this.MakeLong(x, y);
			for (int i = 0; i < numClicks; i++)
			{
				AutoSpy.PostMessage(this.ControlHandle, num, 0u, lParam);
				AutoSpy.PostMessage(this.ControlHandle, num + 1u, 0u, lParam);
				Thread.Sleep(delay);
			}
		}

		public void ClickDrag(int startX, int startY, int endX, int endY, int delay = 0, string button = "left")
		{
			uint num = this.MakeButtonMessage(button);
			uint wParam = this.MakeButtonPressedMessage(button);
			AutoSpy.PostMessage(this.ControlHandle, num, 0u, this.MakeLong(startX, startY));
			Thread.Sleep(delay / 2);
			AutoSpy.PostMessage(this.ControlHandle, 512u, wParam, this.MakeLong(endX, endY));
			Thread.Sleep(delay / 2);
			AutoSpy.PostMessage(this.ControlHandle, num + 1u, 0u, this.MakeLong(endX, endY));
		}

		public void ClickHold(int x, int y, int delay = 0, string button = "left")
		{
			uint num = this.MakeButtonMessage(button);
			AutoSpy.PostMessage(this.ControlHandle, num, 0u, this.MakeLong(x, y));
			Thread.Sleep(delay);
			AutoSpy.PostMessage(this.ControlHandle, num + 1u, 0u, this.MakeLong(x, y));
		}

		public void FocusWindow()
		{
			AutoSpy.SwitchToThisWindow(this.Handle, false);
			Thread.Sleep(50);
		}

		public Point GetMousePos()
		{
			Rectangle windowPos = AutoItX.WinGetPos(this.Handle);
			Point mousePos = AutoItX.MouseGetPos();
			return new Point(mousePos.X - windowPos.X, mousePos.Y - windowPos.Y);
		}

		public int GetPixel(int x, int y)
		{
			return AutoSpy.ColorToInt(this.CurrentFrame.GetPixel(x, y));
		}

		public string[] GetText()
		{
			return AutoItX.WinGetText(this.Handle, 65535).Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		}

		public void Hide()
		{
			int nCmdShow = 0;
			AutoSpy.ShowWindow(this.Handle, nCmdShow);
		}

		public void HoldKey(uint key, int times = 1, int delay = 0)
		{
			AutoSpy.PostMessage(this.ControlHandle, 256u, key, 0u);
			Thread.Sleep(delay);
			AutoSpy.PostMessage(this.ControlHandle, 257u, key, 0u);
		}

		public void Opacity(int value)
		{
			AutoSpy.SetWindowLong(this.Handle, -20, AutoSpy.GetWindowLong(this.Handle, -20) ^ 524288);
			AutoSpy.SetLayeredWindowAttributes(this.Handle, 0u, (byte)value, 2u);
		}

		public void PressKey(uint key, int times = 1, int delay = 0)
		{
			for (int i = 0; i < times; i++)
			{
				AutoSpy.PostMessage(this.ControlHandle, 256u, key, 0u);
				AutoSpy.PostMessage(this.ControlHandle, 257u, key, 0u);
				Thread.Sleep(delay);
			}
		}

		public void ResizeWindow(int width, int height, bool fixedSize = false)
		{
			if (fixedSize)
			{
				int nIndex = -16;
				long num = (long)AutoSpy.GetWindowLong(this.Handle, nIndex);
				num &= -13303809L;
				AutoSpy.SetWindowLong(this.Handle, nIndex, (int)num);
			}
			AutoSpy.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, width, height, 2u);
		}

		public void Scroll(int x, int y, int scrolls = -1, int wheelDelta = 120)
		{
			AutoSpy.PostMessage(this.ControlHandle, 522u, (uint)((uint)(wheelDelta * scrolls) << 16), this.MakeLong(x, y));
		}

		public void Show()
		{
			int nCmdShow = 5;
			AutoSpy.ShowWindow(this.Handle, nCmdShow);
		}

		public void WeightedClick(int x, int y, double scale = 1.0, double density = 1.0, int numClicks = 1, int delay = 0, string button = "left")
		{
			Point point = this.RandomWeightedCoords(x, y, scale, density);
			this.Click(point.X, point.Y, numClicks, delay, button);
		}

		#endregion Public Methods

		#region Private Methods

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern uint GetLastError();

		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetParent(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetWindowLongA", ExactSpelling = true, SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

		[DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "SetWindowLongA", ExactSpelling = true, SetLastError = true)]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint uFlags);

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

		private uint MakeButtonMessage(string button)
		{
			uint result = 0u;
			string a;
			if ((a = button.ToLower()) != null)
			{
				if (!(a == "left"))
				{
					if (!(a == "middle"))
					{
						if (a == "right")
						{
							result = 516u;
						}
					}
					else
					{
						result = 519u;
					}
				}
				else
				{
					result = 513u;
				}
			}
			return result;
		}

		private uint MakeButtonPressedMessage(string button)
		{
			uint result = 0u;
			string a;
			if ((a = button.ToLower()) != null)
			{
				if (!(a == "left"))
				{
					if (!(a == "middle"))
					{
						if (a == "right")
						{
							result = 10u;
						}
					}
					else
					{
						result = 2u;
					}
				}
				else
				{
					result = 1u;
				}
			}
			return result;
		}

		private uint MakeLong(int lowWord, int hiWord)
		{
			return (uint)(hiWord * 65536 | (lowWord & 65535));
		}

		private double RandomWeight()
		{
			return (double)((float)this.random.NextDouble());
		}

		private Point RandomWeightedCoords(int x, int y, double scale = 1.0, double density = 1.0)
		{
			double num = this.RandomWeight() * 2.0 * 3.1415926535897931;
			double num2 = this.RandomWeight();
			if (density == 0.0)
			{
				density = 1.0;
			}
			if (num2 == 0.0)
			{
				num2 = 1E-07;
			}
			double num3 = scale * (Math.Pow(num2, -1.0 / density) - 1.0);
			return new Point((int)((double)x + num3 * Math.Sin(num)), (int)((double)y + num3 * Math.Cos(num)));
		}

		#endregion Private Methods
	}
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.InteropServices;

namespace AzureMcp.Tests.Services.Azure.Authentication;

/// <summary>
/// Provides window handle information for native authentication dialogs.
/// </summary>
public static class WindowHandleProvider
{
    /// <summary>
    /// Get the handle of the foreground window for Windows
    /// </summary>
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    /// <summary>
    /// Get the handle of the console window for Linux
    /// </summary>
    [DllImport("libX11.so.6")]
    private static extern IntPtr XOpenDisplay(string display);

    [DllImport("libX11.so.6")]
    private static extern IntPtr XRootWindow(IntPtr display, int screen);

    [DllImport("libX11.so.6")]
    private static extern IntPtr XDefaultRootWindow(IntPtr display);

    /// <summary>
    /// Get window handle on xplat
    /// </summary>
    public static IntPtr GetWindowHandle()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetForegroundWindow();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            try
            {
                IntPtr display = XOpenDisplay(":1");
                if (display == IntPtr.Zero)
                {
                    Console.WriteLine("No X display available. Running in headless mode.");
                }
                else
                {
                    Console.WriteLine("X display is available.");
                }
                return display;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
            }
            return IntPtr.Zero;
        }
        else
        {
            throw new PlatformNotSupportedException("This platform is not supported.");
        }
    }
}
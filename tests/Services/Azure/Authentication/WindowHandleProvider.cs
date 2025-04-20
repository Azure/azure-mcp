// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.InteropServices;

namespace AzureMcp.Tests.Services.Azure.Authentication;

/// <summary>
/// Provides window handle information for native authentication dialogs.
/// </summary>
public static partial class WindowHandleProvider
{
    /// <summary>
    /// Get window handle across platforms
    /// </summary>
    public static IntPtr GetWindowHandle()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetForegroundWindow();
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            try
            {
                // Get a reference to the shared NSApplication instance
                IntPtr nsApp = NSApplication_sharedApplication();

                if (nsApp != IntPtr.Zero)
                {
                    // Get the main window of the application
                    IntPtr mainWindow = NSApp_mainWindow(nsApp);

                    if (mainWindow != IntPtr.Zero)
                    {
                        // Get the window number which can be used as a handle
                        return NSWindow_windowNumber(mainWindow);
                    }
                    else
                    {
                        Console.WriteLine("No main window available on macOS.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to get NSApplication shared instance.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"macOS window handle error: {ex}");
                Console.ResetColor();
            }
            return IntPtr.Zero;
        }

        throw new PlatformNotSupportedException("This platform is not supported.");
    }

    // Source-generated P/Invoke declarations for Windows
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr GetForegroundWindow();

    // Source-generated P/Invoke declarations for Linux
    [LibraryImport("libX11.so.6")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPUTF8Str)] string display);

    [LibraryImport("libX11.so.6")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr XRootWindow(IntPtr display, int screen);

    [LibraryImport("libX11.so.6")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr XDefaultRootWindow(IntPtr display);

    // Source-generated P/Invoke declarations for macOS
    [LibraryImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr NSApplication_sharedApplication();

    [LibraryImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr NSApp_mainWindow(IntPtr nsApp);

    [LibraryImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    [return: MarshalAs(UnmanagedType.SysInt)]
    private static partial IntPtr NSWindow_windowNumber(IntPtr window);
}
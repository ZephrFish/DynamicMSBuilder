using System;
using System.Collections;
using System.Net;
using System.Management;

class Program
{
    static void Main()
    {
        Console.WriteLine("Environment Variables:");
        Console.WriteLine(new string('-', 40));
        foreach (DictionaryEntry envVar in Environment.GetEnvironmentVariables())
        {
            Console.WriteLine($"{envVar.Key, -30}: {envVar.Value}");
        }
        Console.WriteLine(new string('-', 40));

        string hostName = Dns.GetHostName();
        Console.WriteLine($"Hostname: {hostName}");

        Console.WriteLine("IP Addresses:");
        Console.WriteLine(new string('-', 40));
        try
        {
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            foreach (IPAddress address in addresses)
            {
                Console.WriteLine(address);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving IP addresses: {ex.Message}");
        }
        Console.WriteLine(new string('-', 40));
        Console.WriteLine("DNS Suffix:");
        Console.WriteLine(new string('-', 40));
        try
        {
            string dnsSuffix = GetDnsSuffix();
            Console.WriteLine(dnsSuffix);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving DNS Suffix: {ex.Message}");
        }
        Console.WriteLine(new string('-', 40));
    }

    static string GetDnsSuffix()
    {
        string dnsSuffix = "";
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE");
        foreach (ManagementObject queryObj in searcher.Get())
        {
            dnsSuffix = queryObj["DNSDomain"] as string;
            if (!string.IsNullOrEmpty(dnsSuffix))
            {
                break;
            }
        }
        return dnsSuffix;
    }
}

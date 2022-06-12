using System;
using System.Management;
using Microsoft.VisualBasic.Devices;

namespace GetInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputerInfo computerInfo = new ComputerInfo();

            Console.WriteLine("========== Informações do Sistema ==========\n");
            Console.WriteLine("SO: " + computerInfo.OSFullName);
            Console.WriteLine("Memória RAM: " + Math.Round(computerInfo.TotalPhysicalMemory / Math.Pow(1024.0, 3.0)) + "GB");
            Console.WriteLine("\n============================================\n");

            ManagementClass mc_proc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc_proc = mc_proc.GetInstances();

            Console.WriteLine("========== Processador(es) ==========\n");

            foreach (ManagementObject mo in moc_proc)
            {
                Console.WriteLine("Processador: " + mo.Properties["name"].Value.ToString());
                String arch = mo.Properties["Architecture"].Value.ToString();
                if (arch.Equals("0"))
                {
                    Console.WriteLine("Arquitetura: x86");
                } else if (arch.Equals("1"))
                {
                    Console.WriteLine("Arquitetura: MIPS");
                } else if (arch.Equals("2"))
                {
                    Console.WriteLine("Arquitetura: Alpha");
                } else if (arch.Equals("3"))
                {
                    Console.WriteLine("Arquitetura: PowerPC");
                } else if (arch.Equals("5"))
                {
                    Console.WriteLine("Arquitetura: ARM");
                } else if (arch.Equals("6"))
                {
                    Console.WriteLine("Arquitetura: ia64");
                } else if (arch.Equals("9"))
                {
                    Console.WriteLine("Arquitetura: x64");
                } else if (arch.Equals("12"))
                {
                    Console.WriteLine("Arquitetura: ARM64");
                } else
                {
                    Console.WriteLine("Arquitetura: desconhecida");
                }
            }
            Console.WriteLine("\n=====================================\n");

            ManagementClass mc_disk = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection moc_disk = mc_disk.GetInstances();

            Console.WriteLine("========== Disco(s) ==========\n");

            foreach (ManagementObject mo in moc_disk)
            {
                Console.WriteLine("Letra da Unidade: " + mo.Properties["name"].Value.ToString());
                Console.WriteLine("Nome da Unidade: " + mo.Properties["VolumeName"].Value.ToString());
                Console.WriteLine("Capacidade: " + Math.Round(Int64.Parse(mo.Properties["size"].Value.ToString()) / Math.Pow(1024.0, 3.0)) + "GB");
                Console.WriteLine();
            }

            Console.WriteLine("==============================\n");

            ManagementClass mc_net = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc_net = mc_net.GetInstances();

            Console.WriteLine("========== Adaptador(es) de Rede ==========\n");

            foreach (ManagementObject mo in moc_net)
            {
                if (mo.Properties["IPEnabled"].Value.ToString().Equals("True"))
                {
                    Console.WriteLine("Nome do Adaptador de Rede: {0}", mo.Properties["Description"].Value.ToString());
                    Console.WriteLine("Endereço MAC: {0}", mo.Properties["MACAddress"].Value.ToString());
                    try
                    {
                        string[] ips = (string[])mo.Properties["IPAddress"].Value;
                        foreach (string ip in ips)
                        {
                            if (ip.Contains("."))
                            {
                                Console.WriteLine("Endereço de IPV4: " + ip);
                            } else
                            {
                                Console.WriteLine("Endereço de IPV6: " + ip);
                            }
                        }
                        Console.WriteLine();
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Não foi possível recuperar o endereço de IP");
                    }
                }
            }
            
            Console.WriteLine("===========================================");

            Console.ReadLine();
        }
    }
}

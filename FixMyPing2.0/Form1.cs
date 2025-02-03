using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;

namespace FixMyPing2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Populate DNS combo box with popular DNS servers
            dns_slection_combobox.Items.Add("Google DNS (8.8.8.8)");
            dns_slection_combobox.Items.Add("Cloudflare DNS (1.1.1.1)");
            dns_slection_combobox.Items.Add("OpenDNS (208.67.222.222)");
            dns_slection_combobox.Items.Add("Quad9 (9.9.9.9)");
            dns_slection_combobox.Items.Add("CleanBrowsing (185.228.168.168)");
            dns_slection_combobox.Items.Add("Comodo Secure DNS (8.26.56.26)");
            dns_slection_combobox.Items.Add("DNS.Watch (84.200.69.80)");
            dns_slection_combobox.Items.Add("Yandex DNS (77.88.8.8)");
            dns_slection_combobox.Items.Add("FreeDNS (37.235.1.174)");
            dns_slection_combobox.Items.Add("AdGuard DNS (94.140.14.14)");

            dns_slection_combobox.SelectedIndex = 0;  // Default to Google DNS

            // Button click events
            setDns_btn.Click += SetDns_btn_Click;
            exit.Click += (sender, e) => Application.Exit();

            // Start pinging Google
            Timer pingTimer = new Timer();
            pingTimer.Interval = 1000; // 1 second interval
            pingTimer.Tick += PingTimer_Tick;
            pingTimer.Start();
        }

        private void PingTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("8.8.8.8");  // Google's DNS IP
                if (reply.Status == IPStatus.Success)
                {
                    pingView_label.Text = $"Ping: {reply.RoundtripTime} ms";
                }
                else
                {
                    pingView_label.Text = "Ping failed";
                }
            }
            catch (Exception)
            {
                pingView_label.Text = "Ping failed";
            }
        }

        private void SetDns_btn_Click(object sender, EventArgs e)
        {
            string selectedDns = dns_slection_combobox.SelectedItem.ToString();
            string dnsIp = selectedDns.Split(' ')[1].Trim('(', ')');

            // Set DNS via registry (this will affect the system DNS for the current session)
            SetDnsForCurrentSession(dnsIp);
        }

        private void SetDnsForCurrentSession(string dnsIp)
        {
            try
            {
                // Attempt to set the DNS for the current session using netsh commands
                ProcessStartInfo processStartInfo = new ProcessStartInfo("netsh", $"interface ip set dns \"Wi-Fi\" static {dnsIp}");
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();

                MessageBox.Show($"DNS has been set to {dnsIp} for the current session.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set DNS. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace FixMyPing.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DnsPresetCombo.SelectionChanged += DnsPresetCombo_SelectionChanged;
        }

        private void DnsPresetCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = (System.Windows.Controls.ComboBoxItem)DnsPresetCombo.SelectedItem;
            if (selected?.Tag?.ToString() == "custom")
            {
                CustomDnsText.IsEnabled = true;
            }
            else
            {
                CustomDnsText.IsEnabled = false;
            }
        }

        private async void ApplyDnsButton_Click(object sender, RoutedEventArgs e)
        {
            var servers = GetSelectedServers();
            if (string.IsNullOrWhiteSpace(servers))
            {
                AppendOutput("No DNS servers specified.");
                return;
            }

            AppendOutput($"Applying DNS servers: {servers}");
            var cmd = $"Get-NetAdapter -Physical | Where-Object {{$_.Status -eq 'Up'}} | ForEach-Object {{ Set-DnsClientServerAddress -InterfaceIndex $_.ifIndex -ServerAddresses @({FormatForPowerShellArray(servers)}) }}";
            var result = await RunPowerShell(cmd);
            AppendOutput(result);
        }

        private async void RefreshDnsButton_Click(object sender, RoutedEventArgs e)
        {
            AppendOutput("Refreshing DNS cache...");
            var result = await RunPowerShell("Clear-DnsClientCache; ipconfig /flushdns");
            AppendOutput(result);
        }

        private async void AutoFixButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyDnsButton.IsEnabled = false;
            RefreshDnsButton.IsEnabled = false;
            AutoFixButton.IsEnabled = false;

            AppendOutput("Starting Apply & Refresh sequence...");
            await Task.Run(() => ApplyDnsButton_Click(null, null));
            await Task.Delay(800);
            await Task.Run(() => RefreshDnsButton_Click(null, null));

            AppendOutput("Done.");

            ApplyDnsButton.IsEnabled = true;
            RefreshDnsButton.IsEnabled = true;
            AutoFixButton.IsEnabled = true;
        }

        private string GetSelectedServers()
        {
            var selected = (System.Windows.Controls.ComboBoxItem)DnsPresetCombo.SelectedItem;
            if (selected == null) return string.Empty;
            var tag = selected.Tag?.ToString();
            if (tag == "custom") return CustomDnsText.Text?.Trim() ?? string.Empty;
            return tag ?? string.Empty;
        }

        private string FormatForPowerShellArray(string commaSeparated)
        {
            var parts = commaSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++) parts[i] = $"'{parts[i].Trim()}'";
            return string.Join(",", parts);
        }

        private void AppendOutput(string text)
        {
            Dispatcher.Invoke(() =>
            {
                OutputBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {text}\n");
                OutputBox.ScrollToEnd();
            });
        }

        private Task<string> RunPowerShell(string command)
        {
            return Task.Run(() =>
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "powershell",
                        Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var p = Process.Start(psi)!;
                    var outStr = p.StandardOutput.ReadToEnd();
                    var errStr = p.StandardError.ReadToEnd();
                    p.WaitForExit(60_000);
                    if (!string.IsNullOrWhiteSpace(errStr)) return $"ERROR: {errStr}\n{outStr}";
                    return outStr.Length > 0 ? outStr : "Completed successfully.";
                }
                catch (Exception ex)
                {
                    return $"Exception while running PowerShell: {ex.Message}";
                }
            });
        }
    }
}

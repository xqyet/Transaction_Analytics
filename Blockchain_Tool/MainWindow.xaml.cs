using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Blockchain_Tool
{
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "1FM6A7DWFEF9IQ9QCDJWYPIFBEQDYG5CFN";
        private readonly string baseUrl = "https://api.etherscan.io/api";

        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for the Fetch button click
        private async void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            string walletAddress = WalletAddressInput.Text;

            if (!string.IsNullOrWhiteSpace(walletAddress))
            {
                await FetchWalletTransactions(walletAddress);
            }
            else
            {
                MessageBox.Show("Please enter a valid wallet address.");
            }
        }

        // Method to fetch wallet transactions
        private async Task FetchWalletTransactions(string walletAddress)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"{baseUrl}?module=account&action=txlist&address={walletAddress}&startblock=0&endblock=99999999&sort=asc&apikey={apiKey}";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response correctly
                        var transactionData = JsonSerializer.Deserialize<EtherscanResponse>(jsonResponse);

                        if (transactionData != null && transactionData.Status == "1" && transactionData.Result.Count > 0)
                        {
                            // Clear previous transaction data
                            TransactionsGrid.ItemsSource = null;

                            // Convert the transaction data into a bindable format
                            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
                            foreach (var tx in transactionData.Result)
                            {
                                // Convert timestamp to DateTime
                                var dateTime = UnixTimeStampToDateTime(double.Parse(tx.TimeStamp));

                                // Determine if the transaction is incoming (IN) or outgoing (OUT)
                                string direction = tx.From.ToLower() == walletAddress.ToLower() ? "OUT" : "IN";

                                // Create a view model with formatted data
                                transactions.Add(new TransactionViewModel
                                {
                                    Hash = tx.Hash,
                                    From = tx.From,
                                    To = tx.To,
                                    Value = (double.Parse(tx.Value) / 1_000_000_000_000_000_000).ToString("0.000000", CultureInfo.InvariantCulture),  // Convert value from wei to ETH
                                    DateTime = dateTime.ToString("MM/dd/yyyy HH:mm"),
                                    Direction = direction  // IN or OUT
                                });
                            }

                            // Bind the data to the DataGrid
                            TransactionsGrid.ItemsSource = transactions;
                        }
                        else
                        {
                            MessageBox.Show("No transactions found or failed to retrieve transactions.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error fetching transactions.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching wallet transactions: {ex.Message}");
                }
            }
        }

        // Helper method to convert Unix timestamp to DateTime
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }

    // Define a class for deserializing the JSON response
    public class EtherscanResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;  // Maps to "status" in JSON

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;  // Maps to "message" in JSON

        [JsonPropertyName("result")]
        public List<Transaction> Result { get; set; } = new List<Transaction>();  // Maps to "result" in JSON
    }

    public class Transaction
    {
        [JsonPropertyName("blockNumber")]
        public string BlockNumber { get; set; } = string.Empty;  // Maps to "blockNumber"

        [JsonPropertyName("timeStamp")]
        public string TimeStamp { get; set; } = string.Empty;  // Maps to "timeStamp"

        [JsonPropertyName("hash")]
        public string Hash { get; set; } = string.Empty;  // Maps to "hash"

        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;  // Maps to "from"

        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;  // Maps to "to"

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;  // Maps to "value"

        [JsonPropertyName("gas")]
        public string Gas { get; set; } = string.Empty;  // Maps to "gas"

        [JsonPropertyName("gasPrice")]
        public string GasPrice { get; set; } = string.Empty;  // Maps to "gasPrice"

        [JsonPropertyName("isError")]
        public string IsError { get; set; } = string.Empty;  // Maps to "isError"
    }

    // ViewModel for displaying transaction data in the DataGrid
    public class TransactionViewModel
    {
        public string? Hash { get; set; }  // Made nullable to avoid warnings
        public string? From { get; set; }  // Made nullable to avoid warnings
        public string? To { get; set; }  // Made nullable to avoid warnings
        public string? Value { get; set; }  // Made nullable to avoid warnings
        public string? DateTime { get; set; }  // Made nullable to avoid warnings
        public string? Direction { get; set; }  // IN or OUT
    }
}

# Blockchain Analytics Model

A WPF application for analyzing wallet transactions using multiple API's. The application allows users to input a wallet address, fetch transaction history, and view detailed transaction data. Will be adding more analytics tools and automatic transaction mapping in the future. I'll also update the gui for a more modern/sleek look at some point.

**Current Status: In Development** 🚧

## Features

- Fetch Ethereum wallet transaction history via Etherscan API.
- View transaction details including transaction hash, from and to wallet addresses, transaction value (in ETH), date/time, and transaction direction (IN or OUT).
- Clickable transaction hash that redirects to Etherscan's transaction page for more details.
- Clickable wallet addresses to fetch transactions for selected wallets.
- Displays the latest transactions first by default.
- Visual representation of IN/OUT transactions with color-coded indicators (green for incoming, red for outgoing).
- Loading indicator when fetching transaction data.
- Semi-transparent design with a modern UI featuring a dark grey-to-pink gradient background.

## Demo

[![Watch the video](https://img.youtube.com/vi/HQLgd2tPahE/0.jpg)](https://www.youtube.com/watch?v=HQLgd2tPahE&ab_channel=xqyet)



## Installation

To run this project locally, follow the instructions below:

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/blockchain-analytics-tool.git
    cd blockchain-analytics-tool
    ```

2. Open the solution in Visual Studio.

3. Restore the necessary NuGet packages.

4. Build and run the application.

## What you need 

- **.NET Core / WPF:** User interface framework.
- **Etherscan API:** For fetching Ethereum wallet transaction data.
- **HttpClient:** For making API requests.
- **JSON Serialization:** Using `System.Text.Json` to deserialize API responses.
- **XAML:** For designing the UI.
  
## How It Works

- Input an Ethereum wallet address in the text box.
- Click the "Fetch Transactions" button to retrieve transaction history from Etherscan.
- Transactions will display in the DataGrid, including clickable wallet addresses and transaction hashes.
  - Clicking a wallet address will show the transaction history for that wallet.
  - Clicking a transaction hash will open the transaction details on Etherscan.

## Contributing

If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are welcome.

1. Fork the repo.
2. Create a new branch:
    ```bash
    git checkout -b feature-name
    ```
3. Make your changes and commit:
    ```bash
    git commit -m 'Add a meaningful message'
    ```
4. Push to the branch:
    ```bash
    git push origin feature-name
    ```
5. Submit a pull request!

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Author:** [xqyet](https://xqyet.dev/)  

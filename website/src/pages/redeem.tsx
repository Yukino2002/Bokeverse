import React from 'react';
import Html5QrcodePlugin from '../components/qrcode/Html5QrcodePlugin.jsx'
import ResultContainerPlugin from '../components/qrcode/ResultContainerPlugin.jsx'
import { ConnectWallet, Web3Button} from "@thirdweb-dev/react";
import { ethers } from 'ethers'
import { ThirdwebSDK } from "@thirdweb-dev/sdk";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      decodedResults: [],
      showButton: false,
      result: ''
    }

    // This binding is necessary to make `this` work in the callback.
    this.onNewScanResult = this.onNewScanResult.bind(this);
    this.redeemItem = this.redeemItem.bind(this);
  }
  render() {
    return (
      <div className="pt-24">
        <div>
          <ConnectWallet />
        </div>
        <div className="container mx-auto flex flex-wrap flex-col md:flex-row items-center">
          <section className="App-section w-1/2">
            <br />
            <br />
            <br />
            <Html5QrcodePlugin 
              fps={10}
              qrbox={1000}
              disableFlip={false}
              qrCodeSuccessCallback={this.onNewScanResult}/>
            <ResultContainerPlugin results={this.state.decodedResults} />
            {
              this.state.showButton &&
              <Web3Button
                contractAddress="0xA6565eA363C92430fB674bc056e618D34f1Bf61C"
                action={(contract) =>
                  contract.call("redeemItem", this.state.result)
                }
              >
                Redeem NFT
              </Web3Button>
            }
          </section>
        </div>
      </div>
    );
  }

  onNewScanResult(decodedText, decodedResult) {
    console.log(
      "App [result]", decodedResult['decodedText']);
    this.setState({
      showButton: true,
      result: decodedResult['decodedText']
    });
    this.setState((state, props) => {
      state.decodedResults.push(decodedResult);
      console.log( state.decodedResults);
      return state;
    });
  }
  
  async redeemItem(result) {
    // let ethProvider = new ethers.providers.Web3Provider(window.ethereum);
    // let signer = ethProvider.getSigner()
    // const sdk = ThirdwebSDK.fromSigner(signer);

    // const contract = await sdk.getContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
    // var result = await contract.call("redeemItem", result);
    console.log(result);
  }
}

export default App;

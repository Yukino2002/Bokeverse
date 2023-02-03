// import '../src/App.css';

import React from 'react';
import Html5QrcodePlugin from '../components/qrcode/Html5QrcodePlugin.jsx'
import ResultContainerPlugin from '../components/qrcode/ResultContainerPlugin.jsx'
import { ConnectWallet, useContract, useContractRead, useContractWrite} from "@thirdweb-dev/react";
import { ethers } from 'ethers'
import { ThirdwebSDK } from "@thirdweb-dev/sdk";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      decodedResults: []
    }

    // This binding is necessary to make `this` work in the callback.
    this.onNewScanResult = this.onNewScanResult.bind(this);
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
            
          </section>
        </div>
      </div>
    );
  }

  onNewScanResult(decodedText, decodedResult) {
    console.log(
      "App [result]", decodedResult['decodedText']);
    this.redeemItem(decodedResult['decodedText']);
    this.setState((state, props) => {
      state.decodedResults.push(decodedResult);
      console.log( state.decodedResults);
      return state;
    });
  }
  
  async redeemItem(result) {
    // const MetaMask = Wallet.MetaMask;
    // const wallet = new MetaMask({ appName: "Phaser-Platformer" });
    // const { address, chainId } = await wallet.connect(ChainId.Goerli);

    let ethProvider = new ethers.providers.Web3Provider(window.ethereum);
    // const signer = await wallet.getSigner(chainId);
    let signer = ethProvider.getSigner()
    const sdk = ThirdwebSDK.fromSigner(signer);

    const contract = await sdk.getContract("0xA6565eA363C92430fB674bc056e618D34f1Bf61C");
    // // get signer
    // const signer = await sdk.getSigner();
    var result = await contract.call("redeemItem", result);
    console.log(result);
  }
}

export default App;

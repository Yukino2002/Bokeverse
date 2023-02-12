import React from 'react';
import Html5QrcodePlugin from '../components/qrcode/Html5QrcodePlugin.jsx'
import ResultContainerPlugin from '../components/qrcode/ResultContainerPlugin.jsx'
import { ConnectWallet, Web3Button, useContract } from "@thirdweb-dev/react";
import { ethers } from 'ethers'
import { ThirdwebSDK } from "@thirdweb-dev/sdk";
import AuthProvider from './AuthProvider';

class App extends React.Component <any, any> {
  constructor(props: any) {
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
      <AuthProvider>
        <div className="flex items-center mx-auto mt-[80px] bg-white text-black">
          {/* <div>
            <ConnectWallet />
          </div> */}
          <div className="container mx-auto mb-[290px] flex flex-wrap flex-col p-32 items-center">
            <section className="App-section w-1/2">
              <div className='flex flex-col mx-auto qrcode'>
                <Html5QrcodePlugin
                  fps={10}
                  qrbox={1000}
                  disableFlip={false}
                  qrCodeSuccessCallback={this.onNewScanResult} />
                <ResultContainerPlugin results={this.state.decodedResults} />
                {
                  this.state.showButton &&
                  <Web3Button
                    contractAddress="0xfbFaAB92b0444c36770190F22ea0C116B0Dea1a2"
                    action={(contract) =>
                      contract.call("redeemItem", this.state.result)
                    }
                  >
                    Redeem NFT
                  </Web3Button>
                }
              </div>
            </section>
          </div>
        </div>
      </AuthProvider>
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
      console.log(state.decodedResults);
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

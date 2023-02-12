import React from 'react';
import AuthProvider from './AuthProvider';

class App extends React.Component {
  render() {
    return (
        <div className="mt-[80px] container-iframe">
          <iframe src="https://gateway.ipfscdn.io/ipfs/QmXoX5jmrt5kDTR8VQvCWY9viCe9k84JahsCodqhFiXBai/" className="responsive-iframe"></iframe>
        </div>
    );
  }
}

export default App;

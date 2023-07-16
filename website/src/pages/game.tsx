import React from 'react';
import AuthProvider from './AuthProvider';

class App extends React.Component {
  render() {
    return (
        <div className="mt-[80px] container-iframe">
          <iframe src="https://ipfs.io/ipfs/bafybeifabelk6r27wqfdl6vee6ybih7fmq5k5jl7ocvsgsog7yqkudvplu/" className="responsive-iframe"></iframe>
        </div>
    );
  }
}

export default App;

import React from 'react';
import AuthProvider from './AuthProvider';

class App extends React.Component {
  render() {
    return (
      <AuthProvider>
        <div className="mt-[80px]">
          <iframe src="https://gateway.ipfscdn.io/ipfs/QmXaFThYwd2ohGaD4J4SzCe47DEhQ4ZV3Kv9PLzKkVv3zG/" className="w-full h-screen"></iframe>
        </div>
      </AuthProvider>
    );
  }
}

export default App;

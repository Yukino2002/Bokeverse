import React from 'react';
import AuthProvider from './AuthProvider';

class App extends React.Component {
  render() {
    return (
        <div className="mt-[80px] container-iframe">
          <iframe src="https://gateway.ipfscdn.io/ipfs/QmR74z73Z2SMA4jVgAkwkgyFfUk16BWLxy7uyWd23BQ2JV/" className="responsive-iframe"></iframe>
        </div>
    );
  }
}

export default App;

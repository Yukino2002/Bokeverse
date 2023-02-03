import '@/styles/globals.css'
import Navbar from '../components/Layout/Navbar';
import Footer from '../components/Layout/Footer';
import { ThirdwebProvider, ChainId } from "@thirdweb-dev/react";
const desiredChainId = ChainId.Goerli;

function MyApp({ Component, pageProps }: AppProps) {
	return (
		<ThirdwebProvider desiredChainId={desiredChainId}>
			<div>
				<Navbar />
				<Component {...pageProps} />
				<Footer />
			</div>
		</ThirdwebProvider>
	);
}

export default MyApp;
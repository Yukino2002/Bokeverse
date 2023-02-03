import '@/styles/globals.css'
import Navbar from '../components/Layout/Navbar';
import Footer from '../components/Layout/Footer';
import { ThirdwebProvider, ChainId } from "@thirdweb-dev/react";
import AuthProvider from './AuthProvider';
const desiredChainId = ChainId.Goerli;

function MyApp({ Component, pageProps }: AppProps) {
	return (
		<ThirdwebProvider desiredChainId={desiredChainId}>
			<AuthProvider>
				<Navbar />
				<Component {...pageProps} />
				<Footer />
			</AuthProvider>
		</ThirdwebProvider>
	);
}

export default MyApp;
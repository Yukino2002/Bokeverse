import '@/styles/globals.css'
import type { AppProps } from 'next/app';
import Navbar from '../components/Layout/Navbar';
import Footer from '../components/Layout/Footer';
import { ThirdwebProvider, ChainId } from "@thirdweb-dev/react";
import AuthProvider from './AuthProvider';
const desiredChainId = ChainId.Fantom;

function MyApp({ Component, pageProps }: AppProps) {
	return (
		<ThirdwebProvider desiredChainId={desiredChainId}>
				<Navbar />
				<Component {...pageProps} />
				<Footer />
		</ThirdwebProvider>
	);
}

export default MyApp;
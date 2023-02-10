import Head from 'next/head'
import Image from 'next/image'
import { Inter } from '@next/font/google'
import styles from '@/styles/Home.module.css'
import type { NextPage } from "next";
import { useActiveListings, useContract } from "@thirdweb-dev/react";
import NFTCard from "../components/NFTCard/NFTCard";
import AuthProvider from './AuthProvider';
import Link from "next/link";
const inter = Inter({ subsets: ['latin'] })

const Home: NextPage = () => {
    const { contract } = useContract(
        "0x61067EbAe343f6047271704d63B3f493c0810742",
        "marketplace"
    );
    const { data: nfts, isLoading } = useActiveListings(contract);
    if (isLoading)
        return (
            <div className={"flex flex-col items-center w-screen justify-center font-concertOne italic text-5xl text-[#3A3771] bg-white h-[83vh]"}>Fetching all the listed NFTs for sale ...</div>
        );

    return (
        <AuthProvider>
            <div className="pt-24 bg-white pl-12">
                <div className={"space-y-4 h-[83vh]"}>
                    <div className={"text-4xl text-[#3A3771] font-semibold font-concertOne pb-2"}>Active Listings</div>
                    <div className={`nft-grid`}>
                        {nfts &&
                            nfts.map((nft) => {
                                return (
                                    <Link legacyBehavior
                                        href={`/assets/${nft.id}`}
                                        key={nft.assetContractAddress + nft.id} >
                                        <a>
                                            <NFTCard
                                                nft={{
                                                    name: nft.asset.name as string,
                                                    tokenUri: nft.asset.image as string,
                                                    price: nft.buyoutCurrencyValuePerToken?.displayValue,
                                                }}
                                            />
                                        </a>
                                    </Link>
                                );
                            })}
                    </div>
                </div>
            </div>
        </AuthProvider>

    );
};

export default Home;
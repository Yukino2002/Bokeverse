import Head from 'next/head'
import Image from 'next/image'
import { Inter } from '@next/font/google'
import styles from '@/styles/Home.module.css'
import type { NextPage } from "next";
import { useActiveListings, useContract } from "@thirdweb-dev/react";
import NFTCard from "../components/NFTCard";
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
       <div className={"mb-3 pt-24 flex w-screen justify-center"}>Loading ...</div>
      );

    return (

        <div className="pt-24">
            NFT Marketplace
             <div className={"space-y-4 p-2"}>
              <div className={"text-2xl font-semibold"}>Active Listings</div>
              <div className={`nft-grid`}>
               {nfts &&
                nfts.map((nft) => {
                 return (
                      <Link
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

    );
};

export default Home;
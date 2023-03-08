import Head from 'next/head'
import Image from 'next/image'
import { Inter } from '@next/font/google'
import styles from '@/styles/Home.module.css'

import MetaMaskLogo from '../../public/MetaMask.png';
import CoinbaseLogo from '../../public/Coinbase.png';
import QRCodeLogo from '../../public/QRCode.png';
import MarketplaceLogo from '../../public/Marketplace.png';
import UnityLogo from '../../public/Unity.png';
import ThirdwebLogo from '../../public/Thirdweb.png';
import PlayerImage from '../../public/PlayerLarge.png';
import StateMenuImage from '../../public/StartMenu.jpg';
import BanditsImage from '../../public/Bandits.jpg';
import PartyScreenImage from '../../public/PartyScreen.jpg';
import BattleImage from '../../public/Battle.jpg';
import TownSquareImage from '../../public/TownSquare.jpg';
import WildImage from '../../public/Wild.jpg';

import Feature from '@/components/feature';

const inter = Inter({ subsets: ['latin'] })

export default function Home() {
  return (
    <>
      <Head>
        <title>Bokeverse: Fantom</title>
        <meta name="description" content="Generated by create next app" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
        <link href="https://fonts.googleapis.com/css2?family=Concert+One&family=Press+Start+2P&display=swap" rel="stylesheet" />
      </Head>
      <main className='bg-white'>
        <div className='flex flex-col gap-20'>
          <section className="bg-white text-black mt-[80px]">
            <div className='flex flex-row gap-3 justify-between'>
              <div className='pt-20 flex flex-col font-boogaloo w-[50%] pl-12 justify-between'>
                <h2 className='text-2xl'>
                  Discover a world of adventure with our latest 2D RPG game that offers a unique and exciting gaming experience. Imagine a beautifully crafted and expansive map with turn-based player versus environment battles, where you can showcase your skills and strategy. And now, you can take it to the next level with our decentralized game that requires only your wallet to connect, giving you the freedom to play and explore at any time!
                </h2>

                <h3 className='text-2xl'>
                  With PvP mode on the horizon, the competition is about to heat up. Don't miss out on this opportunity to dive right in and be a part of this exciting new world. Get ready for an unforgettable adventure filled with challenges and rewards.
                </h3>

                <h1 className='text-[#3A3771] text-4xl font-base'>Start playing today!</h1>

                <section className="bg-white text-black pb-8 pt-4">
                  <h2 className="font-boogaloo text-5xl pb-10">
                    Made with
                  </h2>
                  <div className="flex flex-col gap-10">
                    <div className="flex flex-row gap-6">
                      <Image src={UnityLogo} alt="Unity" width={200} />
                      <Image src={ThirdwebLogo} alt="Unity" width={450} />
                    </div>
                  </div>
                </section>
              </div>
              <div className=''>
                <Image src={PlayerImage} alt="Player" width={800} />
              </div>
            </div>
          </section>

          <section className="bg-white px-[275px] text-black pb-6">
            <h2 className="font-boogaloo text-center text-6xl p-6 pb-12">
              Games have never been this fun and accessible at the same time!
            </h2>
            <div className="flex flex-col gap-14">
              <div className="flex flex-row gap-10">
                <Feature image={MetaMaskLogo} title="MetaMask" description="MetaMask's seamless integration allows for effortless and secure transactions within the game world. Players can easily manage their in-game assets, make purchases etc.." />
                <Feature image={QRCodeLogo} title="QR Code" description="We utilize QR codes as a means of connecting physical collectibles to decentralised gaming. This enhances the gaming experience and allows players to own and trade valuable assets." />
              </div>
              <div className="flex flex-row gap-10">
                <Feature image={CoinbaseLogo} title="Coinbase" description="Coinbase integration in our game has been nothing short of wonderful! Players can now easily buy, sell, and store their in-game assets with the security and reliability of Coinbase." />
                <Feature image={MarketplaceLogo} title="Marketplace" description="Our marketplace lets players to trade collectible items as NFTs. Players can now truly own their prized possessions, adding an extra layer of prestige to their in-game achievements!" />
              </div>
            </div>
          </section>

          <section className="bg-white px-[275px] text-black pb-16">
            <h2 className="font-boogaloo text-center text-6xl p-6 pb-12">
              Get a glimpse of the immersive world of Bokeverse!
            </h2>
            <div className="grid grid-cols-3 items-center text-center gap-5">
              <Image src={StateMenuImage} alt="StartMenu" className='border border-black' />
              <Image src={WildImage} alt="Wild" className='border border-black' />
              <Image src={BattleImage} alt="Wild" className='border border-black' />
              <Image src={TownSquareImage} alt="TownSquare" className='border border-black' />
              <Image src={PartyScreenImage} alt="PartyScreen" className='border border-black' />
              <Image src={BanditsImage} alt="Wild" className='border border-black' />
            </div>
          </section>
        </div>
      </main>
    </>
  )
}

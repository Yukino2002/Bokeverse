import Head from 'next/head';
import Link from 'next/link';
import { useEffect, useRef } from 'react';
import { useState } from 'react';

export default function Navbar() {
	const navItems = [
		{
			link: 'https://gateway.ipfscdn.io/ipfs/QmZ7MykGBg2dEneW6RjGV9AHsat27SrJFXhjCNkRYGmd3i/',
			name: 'Game',
			target: "_blank"
		},
		{
			link: '/redeem',
			name: 'Redeem',
			target: ""
		},
		{
			link: '/marketplace',
			name: 'Marketplace',
			target: ""
		},
	];
	const navClass = 'px-8 flex justify-between text-secondary h-full';

	return (
		<>
			<Head>
				<link rel="preconnect" href="https://fonts.googleapis.com" />
				<link rel="preconnect" href="https://fonts.gstatic.com" crossOrigin="anonymous" />
				<link href="https://fonts.googleapis.com/css2?family=Concert+One&display=swap" rel="stylesheet" />
				<link href="https://fonts.googleapis.com/css2?family=Boogaloo&family=Concert+One&display=swap" rel="stylesheet" />
			</Head>
			<div className="fixed top-0 left-0 w-full z-50 h-20 bg-[#fafafa]">
				<nav className={navClass} id="navbar" style={{ transition: '0.4s' }}>
					<Link href="/">
						<div className="flex flex-1 h-full items-center text-[#3A3771]">
							<h1 className="ml-3 text-4xl font-bold font-concertOne tracking-wide">BOKEVERSE</h1>
						</div>
					</Link>
					<div id="" className="flex flex-row items-stretch font-semibold font-bogaloo text-xl content-center align-middle text-[#3A3771] gap-5">
						{navItems.map((item, index) => (
							<Link href={item.link} key={index} target={item.target} className="flex mt-5 align-middle px-5 h-9 hover:opacity-90 hover:border-b-2 hover:border-[#3A3771] border-accent duration-100">
								<div className="my-auto">{item.name}</div>
							</Link>
						))}
					</div>
				</nav>
			</div>
		</>
	);
}
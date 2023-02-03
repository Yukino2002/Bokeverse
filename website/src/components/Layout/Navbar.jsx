import Link from 'next/link';
import { useEffect, useRef } from 'react';
import { useState } from 'react';

export default function Navbar() {
	const navItems = [
		{
			link: '/redeem',
			name: 'Game',
		},
		{
			link: '/redeem',
			name: 'Redeem',
		},
		{
			link: '/marketplace',
			name: 'Marketplace',
		},
	];
	const navClass = 'px-8 flex justify-between text-secondary h-full';

	return (
		<div className="fixed top-0 left-0 w-full z-50 h-20 bg-[#17181e]">
			<nav className={navClass} id="navbar" style={{ transition: '0.4s' }}>
				<Link href="/">
					<div className="flex flex-1 h-full items-center">
						<h1 className="ml-3 text-2xl">Bokeverse</h1>
					</div>
				</Link>
				<div id="" className="flex items-stretch content-center align-middle">
					{navItems.map((item, index) => (
						<Link href={item.link} key={index} className="flex py-auto px-4 hover:opacity-90 hover:border-b-2 border-accent duration-100">
							<div className="my-auto">{item.name}</div>
						</Link>
					))}
				</div>
			</nav>
		</div>
	);
}
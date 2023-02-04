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
		<div className="fixed top-0 left-0 w-full z-50 h-20 bg-[#FFFFFF]">
			<nav className={navClass} id="navbar" style={{ transition: '0.4s' }}>
				<Link href="/">
					<div className="flex flex-1 h-full items-center text-[#3A3771]">
						<h1 className="ml-3 text-2xl font-bold">BOKEVERSE</h1>
					</div>
				</Link>
				<div id="" className="flex flex-row items-stretch font-semibold text-lg content-center align-middle text-[#764ABC] gap-5">
					{navItems.map((item, index) => (
						<Link href={item.link} key={index} className="flex mt-4 align-middle px-5 h-10 hover:opacity-90 hover:border-b-2 hover:border-[#764ABC] border-accent duration-100">
							<div className="my-auto">{item.name}</div>
						</Link>
					))}
				</div>
			</nav>
		</div>
	);
}
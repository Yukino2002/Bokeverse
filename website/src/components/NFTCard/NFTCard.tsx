import Image from "next/image";
import { MediaRenderer } from "@thirdweb-dev/react";
import ethLogo from "./eth-logo.png";

export default function NFTCard({
   nft,
}: {
   nft: {
      tokenUri: string;
      name: string;
      price?: string;
   };
}) {
   return (
      <div
         className={`relative flex cursor-pointer
   flex-col overflow-hidden rounded-lg bg-white shadow-lg
   transition-all duration-300 hover:shadow-2xl dark:bg-[#333333] `}
      >
         <MediaRenderer
            src={nft.tokenUri}
            style={{
               objectFit: "cover",
            }}
            className={
               "rounded-lg transition duration-300 ease-in-out hover:scale-105 h-full p-5"
            }
         />
         <div className={`flex flex-col gap-y-3 p-3`}>
            <div className={`text-4xl font-boogaloo tracking-wide`}>{nft.name}</div>

            {nft.price && (
               <div>
                  <div className={`text-xl font-boogaloo`}>Price</div>
                  <div className={`flex items-center gap-x-1`}>
                     <Image src={ethLogo} height={16} width={16} alt="nft-image" />
                     <p className={`p-2 text-base font-boogaloo`}>{nft.price}</p>
                  </div>
               </div>
            )}
         </div>
      </div>
   );
}
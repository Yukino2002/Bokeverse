import { useRouter } from "next/router";
import { useContract, useListing } from "@thirdweb-dev/react";
import Image from "next/image";
import { AiOutlineClockCircle } from "react-icons/ai";
import { BigNumber } from "ethers";

export default function NFT() {
  const router = useRouter();
  const { listingId } = router.query;
  const { contract } = useContract(
    "0x61067EbAe343f6047271704d63B3f493c0810742",
    "marketplace"
  );

  const { data: nft, isLoading } = useListing(contract, listingId as string);

  const buyoutListing = async () => {
    try {
      await contract?.buyoutListing(BigNumber.from(listingId), 1);
    } catch (e) {
      alert(e);
    }
  };

  if (isLoading || !nft) {
    return (
      <div className="flex h-screen items-center justify-center">
        Loading ...
      </div>
    );
  }

  return (
    <div className="flex justify-center">
      <div className="flex max-w-[500px] flex-col justify-center gap-y-4 p-2">
        <div className="text-2xl font-semibold">{nft?.asset?.name}</div>
        <div className="flex flex-col rounded-lg border border-[#e8ebe5]">
          <div className="flex items-center justify-start p-3">
            <Image src="/matic-logo.png" height={20} width={20} />
          </div>
          <Image
            className="rounded-2xl"
            src={nft?.asset.image as string}
            width={500}
            height={500}
            objectFit="cover"
          />
        </div>
        <div className="flex space-x-1 text-sm">
          <div className="text-gray-500">Owned by</div>
          <div className="cursor-pointer text-blue-500">
            {nft?.sellerAddress}
          </div>
        </div>
        <div className="flex flex-col rounded-lg border border-[#e8ebe5]">
          <div className="border-b border-[#e8ebe5] p-3">
            <div
              className="flex items-center space-x-2 text-sm text-gray-700 md:text-base"
            >
              <AiOutlineClockCircle size={24} />
              <p>
                Sale ends November 26, 2022 at 7:39pm GMT+11
              </p>
            </div>
          </div>
          <div className="flex flex-col gap-y-2 bg-slate-50 p-3">
            <div className="text-sm text-gray-500">Current Price</div>
            <div className="flex items-center space-x-3">
              <Image src="/matic-logo.png"  height={20} width={20} />
                 <p className={`text-3xl font-semibold`}>
                   {nft?.buyoutCurrencyValuePerToken?.displayValue}
                 </p>
                </div>
     <div className={"flex justify-center"}>
      <button
       className={
        "bg-blue-500 text-white font-bold rounded-full p-2"
       }
       onClick={buyoutListing}
      >
       Buyout
      </button>
     </div>
    </div>
   </div>
  </div>
 </div>
);
}
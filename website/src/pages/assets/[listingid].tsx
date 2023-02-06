import { useRouter } from "next/router";
import { useContract, useListing, MediaRenderer } from "@thirdweb-dev/react";
import Image from "next/image";
import { AiOutlineClockCircle } from "react-icons/ai";
import { BigNumber } from "ethers";
export default function NFT() {
  const router = useRouter();
  const { listingid } = router.query;
  const { contract } = useContract(
    "0x61067EbAe343f6047271704d63B3f493c0810742",
    "marketplace"
  );

  const { data: nft, isLoading } = useListing(contract, listingid as string);
  console.log(listingid);
  const buyoutListing = async () => {
    try {
      await contract?.buyoutListing(BigNumber.from(listingid), 1);
    } catch (e) {
      alert(e);
    }
  };

  if (isLoading || !nft) {
    return (
      <div className="flex flex-col items-center w-screen justify-center font-concertOne italic text-5xl text-[#3A3771] bg-white h-[83vh]">
        Loading your NFT ...
      </div>
    );
  }

  return (
    <div className="flex justify-center bg-white mt-[78px] font-boogaloo pb-12">
      <div className="flex max-w-[500px] flex-col justify-center gap-y-4 p-2">
        <div className={"text-2xl font-semibold"}>{nft?.asset?.name}</div>

        <div className={"flex flex-col rounded-lg border border-[#e8ebe5]"}>
          <div className={`flex items-center justify-start p-3`}>
            <Image src={`https://ethereum.org/static/6f05d59dc633140e4b547cb92f22e781/40129/eth-diamond-purple-white.jpg`} height={20} width={20} />
          </div>
          <Image
            className={"rounded-2xl"}
            src={nft?.asset.image as string}
            width={500}
            height={500}
            objectFit={"cover"}
          />
        </div>

        <div className={"flex space-x-1 text-sm"}>
          <div className={"text-[#333333] tracking-wide"}>Owned by</div>
          <div className="cursor-pointer text-blue-500">
            {nft?.sellerAddress}
          </div>
        </div>

        {/*Bottom Section*/}
        <div className={"flex flex-col rounded-lg border border-[#e8ebe5]"}>
          <div className={"border-b border-[#e8ebe5] p-3"}>
            <div
              className={
                "flex items-center space-x-2 text-sm text-gray-700 md:text-base"
              }
            >
              <AiOutlineClockCircle size={24} />
              <p>Sale ends February 17, 2023 at 7:39pm GMT+11</p>
            </div>
          </div>
          <div className={"flex flex-col gap-y-2 bg-slate-50 p-3"}>
            <div className={"text-base text-[#333333]"}>Current Price</div>
            <div className={`flex items-center space-x-3`}>
              <Image src={`https://ethereum.org/static/6f05d59dc633140e4b547cb92f22e781/40129/eth-diamond-purple-white.jpg`} height={24} width={24} />
              <p className={`text-3xl text-black font-semibold`}>
                {nft?.buyoutCurrencyValuePerToken?.displayValue}
              </p>
            </div>
            <button
              type="button"
              className="tracking-wide rounded-lg bg-blue-700 px-5 py-4 text-2xl text-white hover:bg-blue-800 focus:outline-none focus:ring-4 focus:ring-blue-300 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
              onClick={buyoutListing}
            >
              Purchase
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
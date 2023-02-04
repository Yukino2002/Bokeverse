import { ThirdwebStorage } from "@thirdweb-dev/storage";
import dataMonster from '../../../contract/data.json' assert { type: "json" };
import fs from 'fs';
import { ThirdwebSDK } from "@thirdweb-dev/sdk";
import {} from 'dotenv/config' 


const GOERLI_PRIVATE_KEY = "a12e71ebe9a4bc009a99f6b0a99c8f24163ce13d39979100adb2a1a74c7519b7";
const sdk = ThirdwebSDK.fromPrivateKey(GOERLI_PRIVATE_KEY, "goerli");
// First, instantiate the SDK
const storage = new ThirdwebStorage();
const contract = await sdk.getContract("0x0D21a294d856190c393c52bb1d16C2E4AfDFE7Ad");

for (let i = 0; i < dataMonster.length; i++) {
    const filepath="../contract/images/"+dataMonster[i].name+".png";
    const metadata = {
        name: dataMonster[i].name,
        description: "A Bokeverse NFT",
        // Here we add a file into the image property of our metadata
        image: fs.readFileSync(filepath),
        properties: [
          {
            name: dataMonster[i].name,
            type: dataMonster[i].type,
            hp: dataMonster[i].hp,
            attack: dataMonster[i].attack,
            defense: dataMonster[i].defense,
            speed: dataMonster[i].speed,
          },
        ],
      };
    const uri = await storage.upload(metadata);
    // console.log("https://cloudflare-ipfs.com/ipfs/"+jsonCID);
    console.log("https://gateway.ipfscdn.io/ipfs/"+uri.slice(7));
    var result = await contract.call("mint", "0xB7E99669e9eDdD2975511FBF059d01969f43D409", uri, 1);
    console.log(result);
}

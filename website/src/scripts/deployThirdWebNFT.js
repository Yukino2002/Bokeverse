import { ThirdwebStorage } from "@thirdweb-dev/storage";
import dataMonster from '../../../contract/data.json' assert { type: "json" };
import fs from 'fs';
import { ThirdwebSDK } from "@thirdweb-dev/sdk";
import {} from 'dotenv/config' 


const GOERLI_PRIVATE_KEY = process.env.GOERLI_PRIVATE_KEY;
const sdk = ThirdwebSDK.fromPrivateKey(GOERLI_PRIVATE_KEY, "goerli");
// First, instantiate the SDK
const storage = new ThirdwebStorage();
const contract = await sdk.getContract("0xFaFE383B0c49B06d121D85D8d47cEE69324992Fe");

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
    var result = await contract.call("mint", "0xB7E99669e9eDdD2975511FBF059d01969f43D409", uri, "1");
}

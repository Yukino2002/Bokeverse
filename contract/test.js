import { ThirdwebSDK } from "@thirdweb-dev/sdk";
import web3GAME from './artifacts/contracts/web3GAME.sol/web3GAME.json' assert { type: "json" };
import dataMonster from './data.json' assert { type: "json" };
import {globSource, create} from 'ipfs-http-client';
import {} from 'dotenv/config' 
import fs from 'fs';
import { NFTStorage, File, Blob } from "nft.storage";

const GOERLI_PRIVATE_KEY = process.env.GOERLI_PRIVATE_KEY;
const sdk = ThirdwebSDK.fromPrivateKey(GOERLI_PRIVATE_KEY, "goerli");
const NFT_STORE_API_KEY = process.env.NFT_STORE_API_KEY;

const client = new NFTStorage({ token: NFT_STORE_API_KEY });

// const someData = new Blob([await fs.promises.readFile("images/Dewena.png")]);
// const cid = await client.storeBlob(someData);

// console.log(cid);

const contract = await sdk.getContractFromAbi("0xA6565eA363C92430fB674bc056e618D34f1Bf61C",web3GAME.abi);

for (let i = 0; i < dataMonster.length; i++) {
    const filepath="images/"+dataMonster[i].name+".png";
    const imageData = new Blob([await fs.promises.readFile(filepath)]);
    const cid = await client.storeBlob(imageData);
    dataMonster[i].imageCID = cid;
    const jsonData = JSON.stringify(dataMonster[i]);
    const jsonBlob = new Blob([jsonData]);
    const jsonCID = await client.storeBlob(jsonBlob);
    console.log("https://cloudflare-ipfs.com/ipfs/"+jsonCID);

    var result = await contract.call("mint", "0xB7E99669e9eDdD2975511FBF059d01969f43D409", jsonCID, "1");
    console.log(result);
}

// console.log(web3GAMEArray);

// var result = await contract.call("mint", "0xB7E99669e9eDdD2975511FBF059d01969f43D409", "test2", "1");
// console.log(result);

// const myData = await contract.call("getBokemon",1);

// console.log(myData[0],myData[1].toString());

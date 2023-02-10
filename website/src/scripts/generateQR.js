const qr = require("qrcode");
const { ThirdwebStorage } =  require("@thirdweb-dev/storage");
const dataMonster =  require('./QRcodeMonster/data.json');
const fs =  require('fs');
const { ThirdwebSDK } =  require("@thirdweb-dev/sdk");
const {} =  require('dotenv/config');
const moment = require('moment');
const randomString = require("randomstring");
const password = randomString.generate(8);
const GOERLI_PRIVATE_KEY = process.env.GOERLI_PRIVATE_KEY;
const sdk = ThirdwebSDK.fromPrivateKey(GOERLI_PRIVATE_KEY, "goerli");
const storage = new ThirdwebStorage();
const date = new Date();



start();

async function start() {
    const contract = await sdk.getContract("0xfbFaAB92b0444c36770190F22ea0C116B0Dea1a2");
    for (let i = 0; i < dataMonster.length; i++) {
        const filepath="./src/scripts/QRcodeMonster/"+dataMonster[i].name+".png";
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
         console.log(metadata);
        const uri = await storage.upload(metadata);
        // // console.log("https://cloudflare-ipfs.com/ipfs/"+jsonCID);
        console.log("https://gateway.ipfscdn.io/ipfs/"+uri.slice(7));
        var result = await contract.call("createRedeemableItem", password, uri, 1);
        console.log(result);
        // use moment to add time to end of file
        qr.toFile("./src/scripts/QRCodeResults/"+dataMonster[i].name+ ".png", password, function (err) {
            if (err) throw err;
            console.log("saved");
        });
    }
}

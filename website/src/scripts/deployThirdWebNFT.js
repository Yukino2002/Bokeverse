const { ThirdwebStorage } =  require("@thirdweb-dev/storage");
const dataMonster =  require('../../../contract/data.json');
const fs =  require('fs');
const { ThirdwebSDK } =  require("@thirdweb-dev/sdk");
const {} =  require('dotenv/config');


start();


async function start()
{
  const FANTOM_PRIVATE_KEY = process.env.FANTOM_PRIVATE_KEY;
  const sdk = ThirdwebSDK.fromPrivateKey(FANTOM_PRIVATE_KEY, "fantom");
  // First, instantiate the SDK
  const storage = new ThirdwebStorage();
  const contract = await sdk.getContract("0xFF999F6c675d400E7A12BB6E763056C19788da3C");
  
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
      console.log(uri)
      // console.log("https://gateway.ipfscdn.io/ipfs/"+uri.slice(7));
      // var result = await contract.call("mint", "0xA0A4CfffA2470E0d82fe307137Fc2880d6Efa3bf", uri, 1);
      // console.log(result);
  }

}
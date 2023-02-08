<a name="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Yukino2002/Bokeverse">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Bokeverse</h3>

  <p align="center">
    project_description
    <br />
    <a href="https://github.com/Yukino2002/Bokeverse"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Yukino2002/Bokeverse">View Demo</a>
    ·
    <a href="https://github.com/Yukino2002/Bokeverse/issues">Report Bug</a>
    ·
    <a href="https://github.com/Yukino2002/Bokeverse/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)

The game is a decentralized 2D open-world RPG with turn-based battles. It is set in a fantasy world, allowing players to explore, fight monsters and possess NFT characters and collectibles registered to their wallet address. We have also included a trading marketplace for NFTs and a QR code generation system to easily access and purchase these NFTs.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



## Technologies Used

* Unity
    * We used the Unity Engine version 2021.3.16f1 to create the game.

* Thirdweb: We used the Thirdweb web3 development framework to integrate web3 into our game. We used the following solutions and prodcuts:
    * GamingKit: We used the Thirdweb UnitySDK to
        * Connect players' wallets (MetaMask, Coinbase Wallet, etc.) within the game.
        * Read and write to the deployed smart contracts.
        * We also used the Thirdweb dashboard to interact with our deployed smart contracts.
    * ContractKit: We used the Thirdweb ContractKit to
        * Write our contracts for the game and marketplace.
        * We used the ERC1155 base contracts for the creation of NFTs etc.
    * UIKit: We used the Thirdweb UIKit in our website to 
        * Connect Wallet Button: Connect players' wallets (MetaMask, Coinbase Wallet, etc.) within the website.
        * NFT Metadata Renderer: Used this to render the metadata of the nft and list them on the marketplace.
    * ReactSDK: We used the react sdk in the development of our website, qr code redemption and marketplace. Some of the components and functions used are
      * Web3Button
      * useActiveListings
      * useContract
      * ConnectWallet
      * useAddress
      * ThirdwebProvider
      * ChainId
    * Storage:
      * for storing stuff

* Scenario.gg: We used Scenario.gg to create the assets for the game. Such as
  * playable bokemon characters
  * background for start menu
  * background during battle etc.

* Next.js: We used Next.js to create the website.


<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Getting Started

Before getting a local copy up, you must ensure that you have the necessary software required.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* npm
  ```sh
  npm install npm@latest -g
  ```

### Installation

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
   ```sh
   git clone https://github.com/Yukino2002/Bokeverse.git
   ```
3. Install NPM packages
   ```sh
   npm install
   ```
4. Enter your API in `config.js`
   ```js
   const API_KEY = 'ENTER YOUR API';
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Roadmap

- [ ] Feature 1
- [ ] Feature 2
- [ ] Feature 3
    - [ ] Nested Feature

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Your Name - [@twitter_handle](https://twitter.com/twitter_handle) - email@email_client.com

Project Link: [https://github.com/Yukino2002/Bokeverse](https://github.com/Yukino2002/Bokeverse)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* []()
* []()
* []()

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Yukino2002/Bokeverse.svg?style=for-the-badge
[contributors-url]: https://github.com/Yukino2002/Bokeverse/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Yukino2002/Bokeverse.svg?style=for-the-badge
[forks-url]: https://github.com/Yukino2002/Bokeverse/network/members
[stars-shield]: https://img.shields.io/github/stars/Yukino2002/Bokeverse.svg?style=for-the-badge
[stars-url]: https://github.com/Yukino2002/Bokeverse/stargazers
[issues-shield]: https://img.shields.io/github/issues/Yukino2002/Bokeverse.svg?style=for-the-badge
[issues-url]: https://github.com/Yukino2002/Bokeverse/issues
[license-shield]: https://img.shields.io/github/license/Yukino2002/Bokeverse.svg?style=for-the-badge
[license-url]: https://github.com/Yukino2002/Bokeverse/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: website/public/Wild.jpg
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Unity]: https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white
[Unity-url]: https://unity.com/
[Thirdweb]: https://img.shields.io/badge/Thirdweb-000000?style=for-the-badge&logo=thirdweb&logoColor=white

- [DoTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676) - An animation package for Unity.
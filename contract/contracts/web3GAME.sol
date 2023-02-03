// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "@openzeppelin/contracts/token/ERC1155/ERC1155.sol";
import"@openzeppelin/contracts/utils/Counters.sol";

contract web3GAME is ERC1155 {
    
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIds;

    constructor() ERC1155("") {

    }

    mapping (address => uint256[3]) public party; // mapping to store the party of bokemons for each address
    // mapping to store experience of each bokemon
    mapping (uint256 => uint256) public experience;

    function mint(address account, bytes memory uri, uint256 _experience) public returns (uint256) {
        require(uri.length > 0, "Metadata must be provided");
        require(_experience > 0, "Experience must be provided");
        require(account != address(0), "Account must be provided");
        // requrie size of metadata and experience to be the same
        _tokenIds.increment();
        uint256 newItemId = _tokenIds.current();
        _mint(account, newItemId, 1, uri);
        experience[newItemId] = _experience;
        return newItemId;
    }

    function getBokemon(uint256 _id) public view returns (string memory, uint256) {
        return (uri(_id), experience[_id]);
    }
    function getBokemonUri(uint256 _id) public view returns (string memory) {
        return uri(_id);
    }
    function getBokemonPerUser(address _user) public view returns (uint256[] memory) {
        // first count the number of bokemons the user has
        uint256 count = 0;
        for (uint i = 0; i < _tokenIds.current(); i++) {
            if (balanceOf(_user, i) == 1) {
                count++;
            }
        }
        // create an array of the size of the number of bokemons
        uint256[] memory bokemons = new uint256[](count);
        uint256 index = 0;
        for (uint i = 0; i < _tokenIds.current(); i++) {
            if (balanceOf(_user, i) == 1) {
                bokemons[index] = i;
                index++;
            }
        }
        return bokemons;
    }

    function increaseExperience(uint256 _id, uint256 _experience) public {
        experience[_id] += _experience;
    }

    // create redeemable bokemon if code is correct give the user a bokemon
    mapping (string => uint256) private redeemableItems;
    mapping (uint256 => bytes) private redeemableItemsUri;
    mapping (uint256 => uint256) private redeemableItemsExperience;
    mapping (uint256 => bool) private isRedeemed;
    Counters.Counter private _redeemableTokenIds;

    function createRedeemableItem(string memory _code, bytes memory _uri, uint256 _experience) public {
        require(bytes(_code).length > 0, "Code must be provided");
        require(_uri.length > 0, "Metadata must be provided");
        require(_experience > 0, "Experience must be provided");
        _redeemableTokenIds.increment();
        uint256 newItemId = _redeemableTokenIds.current();
        redeemableItems[_code] = newItemId;
        redeemableItemsUri[newItemId] = _uri;
        redeemableItemsExperience[newItemId] = _experience;
    }

    function redeemItem(string memory _code) public {
        require(bytes(_code).length > 0, "Code must be provided");
        require(redeemableItems[_code] != 0, "Code does not exist");
        require(isRedeemed[redeemableItems[_code]] == false, "Item already redeemed");
        isRedeemed[redeemableItems[_code]] = true;
        mint(msg.sender, redeemableItemsUri[redeemableItems[_code]], redeemableItemsExperience[redeemableItems[_code]]);
    }
}
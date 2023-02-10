// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "@thirdweb-dev/contracts/base/ERC1155Base.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "@thirdweb-dev/contracts/extension/interface/IMintableERC1155.sol";

contract web3GAME is ERC1155Base {
    
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIds;

      constructor(
        string memory _name,
        string memory _symbol,
        address _royaltyRecipient,
        uint128 _royaltyBps
    )
        ERC1155Base(
            _name,
            _symbol,
            _royaltyRecipient,
            _royaltyBps
        )
    {}

    // mapping to store experience of each bokemon
    mapping (uint256 => uint256) public experience;

    function mintTo(
        address _to,
        uint256 _tokenId,
        string memory _tokenURI,
        uint256 _amount
    ) public virtual override {
        
        uint256 tokenIdToMint;
        uint256 nextIdToMint = nextTokenIdToMint();

        if (_tokenId == type(uint256).max) {
            tokenIdToMint = nextIdToMint;
            nextTokenIdToMint_ += 1;
            _setTokenURI(nextIdToMint, _tokenURI);
        } else {
            require(_tokenId < nextIdToMint, "invalid id");
            tokenIdToMint = _tokenId;
        }

        _mint(_to, tokenIdToMint, _amount, "");
    }

    function mint(address account, string memory uri, uint256 _experience) public returns (uint256) {
        require(bytes(uri).length > 0, "Metadata must be provided");
        require(_experience > 0, "Experience must be provided");
        require(account != address(0), "Account must be provided");
        // requrie size of metadata and experience to be the same
        uint256 newItemId = _tokenIds.current();
        mintTo(account, type(uint256).max, uri, 1);
        experience[newItemId] = _experience;
        _tokenIds.increment();
        return newItemId;
    }

    function mintSimilar(address account, string memory uri, uint256 _experience, uint256 _id) public returns (uint256) {
        require(bytes(uri).length > 0, "Metadata must be provided");
        require(_experience > 0, "Experience must be provided");
        require(account != address(0), "Account must be provided");
        require(_id < _tokenIds.current(), "Id must be provided");
        // requrie size of metadata and experience to be the same
        uint256 newItemId = _tokenIds.current();
        mintTo(account, _id, uri, 1);
        experience[newItemId] = _experience;
        _tokenIds.increment();
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
            if (balanceOf[_user][i] == 1) {
                count++;
            }
        }
        // create an array of the size of the number of bokemons
        uint256[] memory bokemons = new uint256[](count);
        uint256 index = 0;
        for (uint i = 0; i < _tokenIds.current(); i++) {
            if (balanceOf[_user][i] == 1) {
                bokemons[index] = i;
                index++;
            }
        }
        return bokemons;
    }

    function increaseExperience(uint256 _id, uint256 _experience) public {
        experience[_id] += _experience;
    }

    function increaseExperienceBatch(uint256[] memory _ids, uint256[] memory _experience) public {
        for (uint i = 0; i < _ids.length; i++) {
            experience[_ids[i]] += _experience[i];
        }
    }

    // create redeemable bokemon if code is correct give the user a bokemon
    mapping (string => uint256) private redeemableItems;
    mapping (uint256 => string) private redeemableItemsUri;
    mapping (uint256 => uint256) private redeemableItemsExperience;
    mapping (uint256 => bool) private isRedeemed;
    Counters.Counter private _redeemableTokenIds;

    function createRedeemableItem(string memory _code, string memory _uri, uint256 _experience) public {
        require(bytes(_code).length > 0, "Code must be provided");
        require(bytes(_uri).length > 0, "Metadata must be provided");
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
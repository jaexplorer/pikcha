


# API Documentation


# Images

### Filter Images - random/ pikcha100/ artists100
- ENDPOINT : api/filter/images 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type = random/ pikcha100/ artists100 [string, Default=random], Start [int, Optional], Count [int, Optional]
- RESULTS : images[]
- ERROR_CODES : 200, 416, 500

Image
 - Id 
 - Title 
 - Location  
 - Caption  
 - Thumbnail  
 - Watermark  
 - Views 
 - Artist  
 - Performance 
 - TotSold 
 - AvgPrice 
 - Height 
 - ProductIds 
 

### Get an Image by product id
- ENDPOINT : api/product/{productId} 
- METHOD : get
- AUTHENTICATED : false
- RESULTS : image
- ERROR_CODES : 200, 404, 500

Image
 - Id <-- product id
 - ImageId
 - Title
 - Caption 
 - Location 
 - Type 
 - IsSale 
 - Price 
 - AvgPrice 
 - Watermark 
 - Views  
 - WatermarkedFile 
 - Performance 
 - TotSold  
 - Artist 



### Upload
- ENDPOINT : api/image/upload
- METHOD : post
- AUTHENTICATED : true
- PARAMS : Title [string], Caption [text], Location [string], ImageFile [file], Tags [list of strings]
- RESULTS : OK
- ERROR_CODES : 201, 500


### add view count
- ENDPOINT : api/image/views/increment/{imageId}
- METHOD : post
- AUTHENTICATED : false
- RESULTS : OK
- ERROR_CODES : 201, 500



# Artists

### Get an Artist
- ENDPOINT : api/profile/{userId} 
- METHOD : get
- AUTHENTICATED : false
- RESULTS : artist
- ERROR_CODES : 200, 404, 500


ARTIST
 - Id
 - FName 
 - LName
 - Email 
 - Phone 
 - Avatar 
 - Bio  
 - Links <- dictionary of strings
 - Location 
 - Views 
 - Performance 
 - TotSold 
 - AvgPrice 
 - Following <-- list of users
 - Followers <-- list of users
 - Roles  <-- list of string

### Get the loggedin user info 
- ENDPOINT : api/profile/myinfo/{userId} 
- METHOD : get
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


LOGGEDINUSERINFO
 - Id
 - FName 
 - LName
 - Email 
 - Phone 
 - Avatar
 - Bio 
 - Links 
 - Addr1 
 - Addr2 
 - City 
 - Postal 
 - State 
 - Country 
 - Following 
 - LUploadOn  <--- last uploaded on - to limit one image per day
 - Roles 

### Update user info
- ENDPOINT : api/profile/{userId} 
- METHOD : PUT
- AUTHENTICATED : true
- PARAMS : FirstName, LastName, BioInfo, Sociallinks, ShipAddress1 , ShipAddress2, ShipCity, ShipPostalCode, ShipCountry
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

### Promote a user to an artist 
- ENDPOINT : api/profile/artist/promote/{userId} 
- METHOD : post
- AUTHENTICATED : true
- PARAMS : signatureFile [file]
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

### Follow an artist 
- ENDPOINT : api/profile/artist/follow/{artistId}/{userId} 
- METHOD : post
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Unfollow an artist 
- ENDPOINT : api/profile/artist/unfollow/{artistId}/{userId}  
- METHOD : post
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Upload user avatar 
- ENDPOINT : api/profile/avatar/{userId}
- METHOD : post
- AUTHENTICATED : true
- PARAMS : imageFile [file]
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

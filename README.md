


# API Documentation


# Images

### Filter Images - random/ pikcha100/ artists100/ artistId
- ENDPOINT : api/filter/images 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type = random/ pikcha100/ artists100 [string, Default=random], Start [int, Optional], Count [int, Optional], ArtistId [string, Optional - required for artistId-Type ]
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
 - Artist [FName, LName, Location, Avatar, AggrImViews ]  
 - Performance 
 - TotSold 
 - AvgPrice 
 - MinPrice <-- min price among the products ready to sell (artist's + resellers)
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
- PARAMS : Title [string], Caption [text], Location [string], ImageFile [file], Tags [list of strings], Signature [string], Price [Number]
- RESULTS : OK
- ERROR_CODES : 201, 500


### add view count
- ENDPOINT : api/image/{imageId}/view
- METHOD : post
- AUTHENTICATED : false
- RESULTS : OK
- ERROR_CODES : 201, 500



# Artists

### Get an Artist
- ENDPOINT : api/profile/artist/{artistId}
- METHOD : get
- AUTHENTICATED : false
- RESULTS : artist
- ERROR_CODES : 200, 404, 500
- {artistId}  <-- {userId}

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
 - Sig <- signature file

### Get the loggedin user info 
- ENDPOINT : api/profile/{userId}/myinfo 
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
- PARAMS : FName, LName, Bio, Links, Addr1 , Addr2, City, Postal,State, Country, PhoneNumber 
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

### Promote a user to an artist 
- ENDPOINT : api/profile/{userId}/promote 
- METHOD : post
- AUTHENTICATED : true
- PARAMS : signatureContent [Base64String]
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

### Follow an artist 
- ENDPOINT : api/profile/{userId}/artist/{artistId}/follow 
- METHOD : post
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Unfollow an artist 
- ENDPOINT : api/profile/{userId}/artist/{artistId}/unfollow 
- METHOD : post
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Upload user avatar 
- ENDPOINT : api/profile/{userId}/avatar
- METHOD : post
- AUTHENTICATED : true
- PARAMS : avatarContent [Base64String]
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Upload artist cover photo 
- ENDPOINT : api/profile/artist/{artistId}/cover
- METHOD : post
- AUTHENTICATED : true
- PARAMS : coverContent [Base64String]
- RESULTS : artistProfileInfo
- ERROR_CODES : 200, 404, 500

### add social links 
- ENDPOINT : api/profile/artist/{artistId}/links
- METHOD : PUT
- AUTHENTICATED : true
- PARAMS : Type [string], Url[string] --> [FORMBODY]
- RESULTS : artistProfileInfo
- ERROR_CODES : 200, 404, 500


# Printers
### Get printing templates 
- ENDPOINT : api/product/{printerCode}/templates
- METHOD : get 
- AUTHENTICATED : no <-- should it be true?
- PARAMS : NILL
- RESULTS : List<ProductTemplate>
- ERROR_CODES : 200, 404, 500

{printerCode} == JONDO

ProductTemplate
 - Code
 - Width
 - Height
 - Material
 - Frame
 - Border
 - Finish



### Get printer quote 
- ENDPOINT : api/product/{printerCode}/quote
- METHOD : POST 
- AUTHENTICATED : no <-- should it be true?
- PARAMS : QuoteRequest --> [Body]
- RESULTS : QuoteResult
- ERROR_CODES : 200, 404, 500

 {printerCode} == JONDO

QuoteRequest
 - Addr1
 - Addr2
 - City
 - Postal
 - State
 - Country
 - List<QuoteItem>  <-- we can request quotes for multiple templates at a time. but for us, one code is enough.


 QuoteItem
  - Code <-- from the selected template
  - Qty


QuoteResult
- ShippingClass
- QuoteId
- RefNumber
- City
- State
- Country
- List<QuoteResultItem>


QuoteResultItem
 - Carrier
 - Name
 - Type
 - EstDeliveryOn
 - BaseFreight
 - Tax
 - TotFreight 


 ### create a printing order request 
- ENDPOINT : api/product/{printerCode}/order
- METHOD : POST 
- AUTHENTICATED : true
- PARAMS : OrderRequest --> [Body]
- RESULTS : OK()
- ERROR_CODES : 200, 404, 500


OrderRequest
 - FirstName
 - LastName
 - Addr1
 - Addr2
 - City
 - Postal
 - State
 - Country
 - List<OrderItem> 

 OrderItem
  - Code
  - Qty